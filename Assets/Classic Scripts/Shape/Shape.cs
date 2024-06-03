using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Shape : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public GameObject squareShapeImage;
    public Vector3 shapeSelectedScale;
    public Vector2 offset = new Vector2(0f, 500f);
    public SquareTextureData squareTextureData;
    public ShapeData CurrentShapeData;
    public float transformSpeed = 1f;

	private List<GameObject> _currentShape = new List<GameObject>();

    public int totalSquareNumber { get; set; }
    
    
    private Vector3 _shapeStartScale;
    private RectTransform _transform;
    private bool _shapeDraggable = true;
    private Canvas _canvas;
    private bool _shapeActive = true;
    private Vector3 _StartPos;
	public void Awake()
	{
		_shapeStartScale = this.GetComponent<RectTransform>().localScale;
        _transform = this.GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _shapeDraggable = true;
        _shapeActive = true;
        _StartPos = _transform.localPosition;
        
	}

    public bool IsOnStartPos()
    {
        return _transform.localPosition == _StartPos;
    }

    public bool isAnyOfShapeSquareActive()
    {
        foreach (var square in _currentShape)
        {
            if(square.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
	private void OnEnable()
	{
        GameEvent.MoveShapeToStartPosition += MoveShapeToStartPosition;
        GameEvent.SetShapeInactive += SetShapeInactive;
	}

	private void OnDisable()
	{
		GameEvent.MoveShapeToStartPosition -= MoveShapeToStartPosition;
		GameEvent.SetShapeInactive -= SetShapeInactive;

	}

	public void DeactiveShape()
    {
        if (_shapeActive)
        {
            foreach(var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().DeactivateShape();
            }
        }
        _shapeActive=false;
    }

    public void SetShapeInactive()
    {
        if(IsOnStartPos()==false && isAnyOfShapeSquareActive())
        {
            foreach (var square in _currentShape)
            {
                square.gameObject.SetActive(false);
            }
        }
    }
    public void ActivateShape()
    {
        if (!_shapeActive)
        {
            foreach(var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().ActivateShape();
            }
            _shapeActive=true;
        }
    }
	public void RequestNewShape(ShapeData shapeData)
    {
		_transform.localPosition = _StartPos;
        squareTextureData.RandomColor();
        CreateShape(shapeData);
    }

    public void CreateShape(ShapeData shapeData)
    {
        
        CurrentShapeData = shapeData;
        totalSquareNumber = GetNumberOfSquares(shapeData);
        while (_currentShape.Count <= totalSquareNumber) 
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }
        GameEvent.UpdateSquareColor(squareTextureData.currentColor);

        foreach (var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, squareRect.rect.height * squareRect.localScale.y);

        int currentIndexInList = 0;

        //tao form cho shape

        for(var row = 0; row < shapeData.rows; row++)
        {
            for (var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance),
                            GetYPositionForShapeSquare(shapeData, row, moveDistance));
                    currentIndexInList++;
                }
            }
        }
    }	
    
	public float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
	{
		float squareShiftY = 0;
		int middleElement = (shapeData.rows / 2);
		squareShiftY = moveDistance.y * (middleElement - row);
		return squareShiftY;
	}
	public float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
	{
		float squareShiftX = 0;
		int middleElement = (shapeData.columns / 2);
		squareShiftX = moveDistance.x * (middleElement - column) * -1;
		return squareShiftX;
	}
	
	private int GetNumberOfSquares(ShapeData shapeData)
    {
        int number = 0;
        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.column)
            {
                if(active)
                    number++;
            }
        }
        return number;
    }

	public void OnPointerClick(PointerEventData eventData)
    {
		
	}
    public void OnPointerDown(PointerEventData eventData)
    {
        GameEvent.isPlaying = true;
        onDrag(eventData);
	}
	public void OnPointerUp(PointerEventData eventData)
    {
		//MoveShapeToStartPosition();
		this.GetComponent<RectTransform>().localScale = _shapeStartScale;
		GameEvent.CheckIfShapeCanbePlaced();
		GameEvent.isPlaying = false;
	}
	public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
	public void OnDrag(PointerEventData eventData)
    {
		
		GameEvent.isPlaying = true;
        onDrag(eventData);
	}
	public void OnEndDrag(PointerEventData eventData)
    {
        //MoveShapeToStartPosition();
		this.GetComponent<RectTransform>().localScale = _shapeStartScale;
        GameEvent.CheckIfShapeCanbePlaced();
        GameEvent.isPlaying = false;
		
	}
	

    private void MoveShapeToStartPosition()
    {
        _transform.transform.localPosition = _StartPos;
    }

    private void onDrag(PointerEventData eventData)
    {
			this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
			_transform.anchorMin = new Vector2(0, 0);
		    _transform.anchorMax = new Vector2(0, 0);
		    _transform.pivot = new Vector2(0, 0);

		    Vector2 pos;
		    RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, eventData.position, Camera.main, out pos);
		    _transform.localPosition = pos + offset;
        
    }

}
