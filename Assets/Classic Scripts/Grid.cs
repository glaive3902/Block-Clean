﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Grid : MonoBehaviour
{
    
    public ShapeStorage shapeStorage;
    public int columns = 0;
    public int rows = 0;
    public float squaresGap = 0.1f;
    public GameObject gridSquare;
    public Vector2 startPosition = new Vector2 (0.0f , 0.0f);
    public float squareScale = 0.5f;
    public float everySquareOffset = 0.0f;
    public SquareTextureData squareTextureData;

    private int _comboLimit = 3;
    private int comboCountLeft;
    AudioManager audioManager;
    private LineIndicator _lineIndicator;
    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GameObject> _gridSquares = new List<GameObject>();
    private Config.SquareColor _currentActiveSquareColor = Config.SquareColor.NotSet;

	private void OnEnable()
	{
		GameEvent.CheckIfShapeCanbePlaced += CheckIfShapeCanBePlaced;
        GameEvent.UpdateSquareColor += OnUpdateSquareColor;
	}

	private void OnDisable()
	{
		GameEvent.CheckIfShapeCanbePlaced -= CheckIfShapeCanBePlaced;
		GameEvent.UpdateSquareColor -= OnUpdateSquareColor;

	}



	void Start()
    {
		 _lineIndicator = GetComponent<LineIndicator>();
        _currentActiveSquareColor = squareTextureData.activeSquareTextures[0].squareColor;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        NewGame();
	}

    private void OnUpdateSquareColor(Config.SquareColor color)
    {
        _currentActiveSquareColor = color;
    }
    public void ReplayButton()
    {
        NewGame();
    }
    private void CreateGrid()
    {
        SpawnGridSquare();
        SetGridSquarePosition();
    }

    public void NewGame()
    {
        foreach (GameObject square in _gridSquares) 
            {
                if(square != null)
                {
                    Destroy(square);
                }
                
            } 
        _gridSquares.Clear();
        GameEvent.NewGame = true;
        CreateGrid();
        
        //squareTextureData.RandomColor();
        GameEvent.RequestNewShapes();
        GameEvent.NewGame = false;
        GameEvent.bestScoreReached = false;
        GameEvent.OnCountDown = false;
        GameEvent.isPlaying = false;
        audioManager.PlaySFX(audioManager.NewGame);

	}

    public void GameOverBoard()
    {
        squareTextureData.GameOverColor();
        
		foreach (GameObject square in _gridSquares)
		{
			if (square.GetComponent<GridSquare>().Selected == false)
			{
                square.GetComponent<GridSquare>().ActivateSquare();
			}

		}
	}
    private void SpawnGridSquare()
    {
        //0, 1, 2, 3, 4, 5, 6, 7, 8,
        //9, 10, 11, 12, 13, 14, 15, 16,
        
        int square_index = 0;
        for (var row = 0;  row< rows; ++row) 
        {
            for (var column = 0; column< columns; ++column)
            {
                _gridSquares.Add(Instantiate(gridSquare) as GameObject);

                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SquareIndex = square_index;
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(this.transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3 (squareScale, squareScale, squareScale);
                _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SetImage(square_index % 2 == 0);
                square_index++;
            }
        }
    }

    private void SetGridSquarePosition()
    {
        int column_number = 0;
        int row_number = 0;
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;
        var square_rect = _gridSquares[0].GetComponent<RectTransform>();

        _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffset;
        _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffset;

        foreach (GameObject square in _gridSquares)
        {
            if( column_number + 1 > columns)
            {
                square_gap_number.x = 0;
                column_number = 0;
                row_number++;
                row_moved = false;
            }
            var pos_x_offset = _offset.x * column_number + (square_gap_number.x * squaresGap);
            var pos_y_offset = _offset.y * row_number +(square_gap_number.y * squaresGap);

            if (column_number > 0 && column_number % 2 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += squaresGap;
            }

            if (row_number >0 && row_number % 2 == 0)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += squaresGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset);
            square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset, startPosition.y - pos_y_offset, 0.0f);
            column_number++;
        }
    }

    private void ResetComboCount()
    {
        comboCountLeft = _comboLimit;
    }

    private void CheckIfShapeCanBePlaced()
    {
        var squareIndexes = new List<int>();
        foreach (var square in _gridSquares)
        {
            var gridSquare = square.GetComponent<GridSquare>();
            if(gridSquare.Selected && !gridSquare.SquareOccupied)
            {
                squareIndexes.Add(gridSquare.SquareIndex);
                gridSquare.Selected = false;
                //gridSquare.ActivateSquare();
            }
        }

        int singleScore = 1;
        var currentSelectedShape = shapeStorage.GetCurrentSelectedShape();
        if (currentSelectedShape == null) return; 

        if(currentSelectedShape.totalSquareNumber == squareIndexes.Count)
        {
            foreach (var squareIndex in squareIndexes)
            {
                _gridSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnTheBoard(_currentActiveSquareColor);
                
                
                GameEvent.Addscore(singleScore);
            }
            
            
            var shapeleft = 0;
            foreach (var shape in shapeStorage.shapeList)
            {
                if(shape.IsOnStartPos() && shape.isAnyOfShapeSquareActive())
                {
                    shapeleft++;
                }
            }

            if (shapeleft == 0)
            {
                GameEvent.RequestNewShapes();
            }
            else
            {
                GameEvent.SetShapeInactive();
            }
			CheckIfAnyLineCompleted();
        }
        else
        {
            //GameEvent.isPlaying = false;
            audioManager.PlaySFX(audioManager.invalid);
            GameEvent.MoveShapeToStartPosition();

		}
    }

    public void CheckIfAnyLineCompleted()
    {
        int BP = 0;
        List<int[]> lines = new List<int[]>();
        //cột
        foreach(var column in _lineIndicator.columnIndexes)
        {
            lines.Add(_lineIndicator.GetVerticalLine(column));   
        }

        //hàng

        for(var row = 0; row < 8; row++)
        {
            List<int> data = new List<int>(8);
            for(var index = 0; index <8; index++)
            {
                data.Add(_lineIndicator.line_data[row,index]);
            }
            lines.Add(data.ToArray());

        }

        var completedLines = CheckIfSquareAreCompleted(lines);
        if (completedLines == 1)
            audioManager.PlaySFX(audioManager.singleLine);
        if(completedLines >= 2)
        {
            GameEvent.ShowEffects();
            BP = 20 * completedLines * 2;
            GameEvent.AddPlus(BP);
            //bonus
            if (completedLines == 2)
                audioManager.PlaySFX(audioManager.doubleLines);
            else if (completedLines == 3)
                { 
                audioManager.PlaySFX(audioManager.tripleLines);
                
                }

			else
            {
                audioManager.PlaySFX(audioManager.MoreThanFour);
            }
		}

        // + điểm
        var totalScore = 20 * completedLines + BP; // + bonus
        GameEvent.Addscore(totalScore);
        CheckIfPlayerLost();
        
	}

    private int CheckIfSquareAreCompleted(List<int[]> data)
    {
        List<int[]> completedLines = new List<int[]>();

        var linesCompleted = 0;

        foreach(var line in data)
        {
            var lineCompleted = true;
            foreach (var squareIndex in line)
            {
                var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                if (comp.SquareOccupied == false)
                {
                    lineCompleted = false;
                }
            }

            if (lineCompleted)
            {
                
                completedLines.Add(line);
            }
        }
        foreach (var line in completedLines)
        {
            var completed = false;
            foreach (var squareIndex in line)
            {
                var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                comp.Deactivate();
                completed = true;
            }
            foreach (var squareIndex in line)
            {
				var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                comp.ClearOccupied();
			}
            if (completed)
            {
                ResetComboCount();
                linesCompleted++;
            }
		}
        return linesCompleted;
    }

    public void CheckIfPlayerLost()
    {
        var validShapes = 0;
        for (var index = 0; index < shapeStorage.shapeList.Count; index++)
        {
            var isShapeActive = shapeStorage.shapeList[index].isAnyOfShapeSquareActive();
            if (CheckIfShapeCanBePlacedOnGrid(shapeStorage.shapeList[index]) && isShapeActive)
            {
                shapeStorage.shapeList[index]?.ActivateShape();
                validShapes++;
				
			}
        }

        if(validShapes == 0)
        {
            GameEvent.GameOver();
            StartCoroutine(GameOver());
            
        }
        Debug.Log(validShapes);
    }

	private bool CheckIfShapeCanBePlacedOnGrid(Shape currentShape)
	{
		var currentShapeData = currentShape.CurrentShapeData;
		var shapeColumns = currentShapeData.columns;
		var shapeRows = currentShapeData.rows;

		List<int> originalShapeFilledUpSquares = new List<int>();
		var squareIndex = 0;
		for (var rowIndex = 0; rowIndex < shapeRows; rowIndex++)
		{
			for (var columnIndex = 0; columnIndex < shapeColumns; columnIndex++)
			{
				if (currentShapeData.board[rowIndex].column[columnIndex])
				{
					originalShapeFilledUpSquares.Add(squareIndex);
				}
				squareIndex++;
			}
		}

		if (currentShape.totalSquareNumber != originalShapeFilledUpSquares.Count)
			Debug.Log("Co van de voi bo dem");

		var squareList = GetAllSquareCombination(shapeColumns, shapeRows);

		foreach (var number in squareList)
		{
			bool shapeCanBePlacedOnTheBoard = true;
			foreach (var squareIndexToCheck in originalShapeFilledUpSquares)
			{
				var comp = _gridSquares[number[squareIndexToCheck]].GetComponent<GridSquare>();
				if (comp.SquareOccupied)
				{
					shapeCanBePlacedOnTheBoard = false;
					break; // Break early if any square is occupied
				}
			}

			if (shapeCanBePlacedOnTheBoard)
			{
				return true; // Can place the shape
			}
		}

		return false; // Cannot place the shape anywhere
	}

	private List<int[]> GetAllSquareCombination(int columns, int rows)
	{
		var squareList = new List<int[]>();
		int gridSize = 8; // Assuming a fixed grid size of 8x8

		for (int startRow = 0; startRow <= gridSize - rows; startRow++)
		{
			for (int startCol = 0; startCol <= gridSize - columns; startCol++)
			{
				var combination = new List<int>();
				for (int row = startRow; row < startRow + rows; row++)
				{
					for (int col = startCol; col < startCol + columns; col++)
					{
						combination.Add(_lineIndicator.line_data[row, col]);
					}
				}
				squareList.Add(combination.ToArray());
			}
		}

		return squareList;
	}

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1);
        GameOverBoard();
    }
    
}
