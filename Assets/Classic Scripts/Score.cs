using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public static int currentScore;
    private int _bestScore = 0;
    void Start()
    {
        currentScore = 0;
        _bestScore = PlayerPrefs.GetInt("bestScore");
    }

	private void OnEnable()
	{
        GameEvent.Addscore += Addscore;
        
	}

	private void OnDisable()
	{
        GameEvent.Addscore -= Addscore;
        
	}

	void Update()
    {
        _bestScore = PlayerPrefs.GetInt("bestScore");
    }

    private void Addscore(int score)
    {
        currentScore += score;

        UpdateScoreText();
        if (currentScore >= _bestScore)
        {
            GameEvent.bestScoreReached = true;
            _bestScore = currentScore;
            PlayerPrefs.SetInt("bestScore", _bestScore);
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void SetNewGameScore()
    {
        currentScore = 0;
        UpdateScoreText();
	}
}
