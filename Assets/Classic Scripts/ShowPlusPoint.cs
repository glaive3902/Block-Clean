using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowPlusPoint : MonoBehaviour
{
    public TMP_Text PlusPoint;
    private int BonusPoint;
    void Start()
    {
        GameEvent.AddPlus += AddPlus;

	}

	private void Update()
	{
        updatePS();
	}
	private void OnDisable()
	{
        GameEvent.AddPlus -= AddPlus;
	}


    private void AddPlus(int score)
    {
        BonusPoint = score;
        PlusPoint.gameObject.SetActive(true);
    }

    private void updatePS()
    {
        PlusPoint.text = "+" + BonusPoint.ToString();
    }

    //private void DisplayPS()
    //{
		
	
}
