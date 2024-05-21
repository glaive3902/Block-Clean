using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bestScore : MonoBehaviour
{
    public TMP_Text bestScoreText;
    private int _currentBestScore;

    private void Start()
	{
		
	}

	void Update()
    {
        _currentBestScore = PlayerPrefs.GetInt("bestScore");
        DisplayBestScore();
    }

    private void DisplayBestScore()
    {
        bestScoreText.text = _currentBestScore.ToString();
    }

	public void ResetBestScore()
	{
        PlayerPrefs.DeleteKey("bestScore");
	}
}
