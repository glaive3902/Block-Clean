using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShapeSquare : MonoBehaviour
{
	private Shadow shadow;
	private void Start()
	{
		shadow = GetComponent<Shadow>();
	}

	public void DeactivateShape()
	{
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.SetActive(false);
	}

	public void ActivateShape()
	{
		gameObject.GetComponent<BoxCollider2D>().enabled=true;
		gameObject.SetActive(true);
	}

}
