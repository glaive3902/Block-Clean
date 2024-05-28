using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;
using UnityEngine.UI;
public class GameOverPopUps : MonoBehaviour
{
	public Image NoMoreSpace;
	public GameObject Win;
	public GameObject Lose;

	public TMP_Text bestScore;
	public TMP_Text FinalScore;

	private int _finalScore;
	private int _bestscore;
    void Start()
    {
		
    }

	private void FixedUpdate()
	{
		UpdateBS();
		UpdateSC();
	}
	private void OnDisable()
	{
		GameEvent.GameOver -= OnGameOver;
	}

	private void OnEnable()
	{
		GameEvent.GameOver += OnGameOver;
	}

	public void OnGameOver()
	{
		StartCoroutine(EndGame());
	}

	public void UpdateBS()
	{
		_bestscore = PlayerPrefs.GetInt("bestScore");
		bestScore.text = _bestscore.ToString();
	}

	public void UpdateSC()
	{
		_finalScore = Score.currentScore;
		FinalScore.text = _finalScore.ToString();
	}

	public void WinOrLose()
	{
		if (GameEvent.bestScoreReached)
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

	private IEnumerator EndGame()
	{
		NoMoreSpace.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		NoMoreSpace.gameObject.SetActive(false);
		WinOrLose();
	}
}
