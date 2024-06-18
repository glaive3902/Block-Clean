using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class GridSquare : MonoBehaviour
{
    public Image normalImage;
    public Image activeImage;
    public Image hoverImage;
    public Image PlaceEffectImage;
    public List<Sprite> normalImages;
    public Animator animator;
    AudioManager audioManager;
    private Config.SquareColor _currentSquareColor = Config.SquareColor.NotSet;

    public Config.SquareColor GetCurrentColor()
    {
        return _currentSquareColor;
    }
    public bool Selected { get;set;}
    public int SquareIndex { get; set; }

    public bool SquareOccupied { get; set; }

    void Start()
    {
        Selected = false;
        SquareOccupied = false;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

	private void Update()
	{
        checkPlaying();
	}
	public bool CanWeUseThisSquare()
    {
        return hoverImage.gameObject.activeSelf;
    }

    public void PlaceShapeOnTheBoard(Config.SquareColor color)
    {
        
        _currentSquareColor = color;
        ActivateSquare();
        //audioManager.PlaySFX(audioManager.putDown);
    }
    public void ActivateSquare()
    {
        hoverImage.gameObject.SetActive(false);
        activeImage.gameObject.SetActive(true);
		PlaceEffectImage.gameObject.SetActive(true);
        Selected = true;
        SquareOccupied = true;
    }

    public void Deactivate()
    { 
        _currentSquareColor = Config.SquareColor.NotSet;
        animator.enabled = true;
		

	}

    public void ClearOccupied()
    {
		_currentSquareColor = Config.SquareColor.NotSet;
		Selected = false;
        SquareOccupied =false;
    }
    public void SetImage(bool setFirstImage)
    {
        normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (SquareOccupied == false)
        {
            Selected =true;
		    hoverImage.gameObject.SetActive(true);

		}
        //else if (collision.GetComponent<ShapeSquare>() != null)
        //{
        //    collision.GetComponent<ShapeSquare>().SetOccupied();
        //}

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        Selected = true;
		if (SquareOccupied == false)
		{
			
			hoverImage.gameObject.SetActive(true);

		}
		//else if (collision.GetComponent<ShapeSquare>() != null)
		//{
		//	collision.GetComponent<ShapeSquare>().SetOccupied();
		//}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (SquareOccupied == false)
		{
			Selected = false;
            hoverImage.gameObject.SetActive(false);
		}
		//else if (collision.GetComponent<ShapeSquare>() != null)
		//{
		//	collision.GetComponent<ShapeSquare>().UnSetOccupied();
		//}

	}

    private void checkPlaying()
    {
        if (GameEvent.isPlaying == true)
        {
			GetComponent<BoxCollider2D>().enabled = true;
		}
        else
        {
			GetComponent<BoxCollider2D>().enabled = false;
		}
    }
	public void Disable()
	{
		activeImage.gameObject.SetActive(false);
	}

	public void DisableAnim()
	{
		GetComponent<Animator>().enabled = false;
	}
}

