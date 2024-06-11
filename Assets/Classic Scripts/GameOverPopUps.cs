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
	public TMP_Text bestScore;
	public TMP_Text FinalScore;
	public TMP_Text GameOverBestScore;

	private int _finalScore;
	private int _bestscore;
    void Start()
    {
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
	}

	private void FixedUpdate()
	{
		UpdateSC();
		ShowCurrentBS();
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
		int currentBestScore = PlayerPrefs.GetInt("bestScore");
		StartCoroutine(AnimateScore(currentBestScore));
	}
	public void ShowCurrentBS()
	{
		_bestscore = PlayerPrefs.GetInt("bestScore");
		GameOverBestScore.text = _bestscore.ToString();
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
			UpdateBS();
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

	private IEnumerator AnimateScore(int BestScore)
	{
		
		float duration = 3f; // thoi gian chay hieu ung diem so
		float elapsed = 0.0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			_bestscore = (int)Mathf.Lerp(0, BestScore, elapsed / duration);
			bestScore.text = _bestscore.ToString();
			yield return null;
		}
		_bestscore = BestScore;
		bestScore.text = _bestscore.ToString();
	}

}
