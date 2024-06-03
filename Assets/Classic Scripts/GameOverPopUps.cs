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

	AudioManager audioManager;
	public List<TMP_Text> bestScore;
	public TMP_Text FinalScore;

	private int _finalScore;
	private int _bestscore;
    void Start()
    {
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
		foreach (var text in bestScore)
		{_bestscore = PlayerPrefs.GetInt("bestScore");
			text.text = _bestscore.ToString();
		}
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
			audioManager.PlaySFX(audioManager.win);
			Lose.SetActive(false);
		}
		else
		{
			Win.SetActive(false);
			audioManager.PlaySFX(audioManager.lose);
			Lose.SetActive(true);
		}
	}

	private IEnumerator EndGame()
	{
		NoMoreSpace.gameObject.SetActive(true);
		yield return new WaitForSeconds(4);
		WinOrLose();
		NoMoreSpace.gameObject.SetActive(false);
	}
}
