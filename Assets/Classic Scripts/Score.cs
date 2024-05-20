using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    private int _currentScore;
    private int _bestScore = 0;
    void Start()
    {
        _currentScore = 0;
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
        
    }

    private void Addscore(int score)
    {
        _currentScore += score;
        UpdateScoreText();
        if (_currentScore >= _bestScore)
        {
            GameEvent.bestScoreReached = true;
            _bestScore = _currentScore;
            PlayerPrefs.SetInt("bestScore", _bestScore);
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = _currentScore.ToString();
    }

    public void SetNewGameScore()
    {
        _currentScore = 0;
        UpdateScoreText();
	}
}
