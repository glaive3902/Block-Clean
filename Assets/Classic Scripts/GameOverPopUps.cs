using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using TMPro;
using UnityEngine.UI;
public class GameOverPopUps : MonoBehaviour
{
	public GameObject Win;
	public GameObject Lose;

	public TMP_Text bestScore;
	public TMP_Text FinalScore;

	private int _finalScore;
	private int _bestscore;
    void Start()
    {
		
    }

	private void Update()
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
		if (GameEvent.bestScoreReached)
		{
			Win.gameObject.SetActive(true);
			Lose.gameObject.SetActive(false);
		}
		else if(!GameEvent.bestScoreReached)
		{
			Win.gameObject.SetActive(false);
			Lose.gameObject.SetActive(true);
		}

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
}
