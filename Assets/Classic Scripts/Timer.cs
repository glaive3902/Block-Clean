using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
	public GameObject Win;
	public GameObject Lose;
	public Slider ReviveSlider;
	public Grid grid;
	//public rewardADS rewardads;
	public float currentTime;
	public float OriginalCountdown = 5;

	private void Start()
	{
		currentTime = OriginalCountdown;
		ReviveSlider.maxValue = OriginalCountdown;
	}
	//AudioManager audioManager;

	private void Update()
	{
		Countdown();
		ReviveSlider.value = currentTime;
		if(ReviveSlider.value == 0)
		{
			GameEvent.GameOver();
			grid.StartCoroutine(grid.GameOver());
			ReviveSlider.gameObject.SetActive(false);
			
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
		}
		
	}
	IEnumerator CountdownRoute()
	{
		ReviveSlider.gameObject.SetActive(true);
		currentTime = OriginalCountdown;
		yield return new WaitForSeconds(1);
		GameEvent.OnCountDown = true;
	}

	private void WinOrLose()
	{
		if(GameEvent.bestScoreReached)
		{
			Win.SetActive(true);
			Lose.SetActive(false);
		}
		else
		{
			Win.SetActive(false);
			Lose.SetActive(true);
		}
		
	}
	public void resetRevive()
	{
		currentTime = OriginalCountdown;
	}
}
