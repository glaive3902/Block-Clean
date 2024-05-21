using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
	public GameObject EndGameButton;
    public float currentTime;
	public float OriginalCountdown = 5;

	private void Start()
	{
		
	}

	private void Update()
	{
		if (GameEvent.OnCountDown)
		{
			Countdown();
		}
		else if(!GameEvent.OnCountDown)
		{
			StartCoroutine(CountdownRoute());
		}
	}

	public void Countdown()
	{
		
		if (currentTime > 0)
		{
			
			currentTime -= Time.deltaTime;
		}
		else
		{
			currentTime = 0;
			EndGameButton.SetActive(true);
			
		}
	}
	IEnumerator CountdownRoute()
	{
		EndGameButton.SetActive(false);
		currentTime = OriginalCountdown;
		yield return new WaitForSeconds(1);
		GameEvent.OnCountDown = true;
	}
}
