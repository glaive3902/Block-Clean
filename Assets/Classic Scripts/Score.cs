using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    private int _currentScore;
    void Start()
    {
        _currentScore = 0;
        
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
