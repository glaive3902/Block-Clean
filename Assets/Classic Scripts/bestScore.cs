using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class bestScore : MonoBehaviour
{
    public Image icon;
    public TMP_Text bestScoreText;
    private int _currentBestScore;

    private void Start()
	{
		
	}

	void Update()
    {
        _currentBestScore = PlayerPrefs.GetInt("bestScore");
        DisplayBestScore();
        notice();
	}

    private void DisplayBestScore()
    {
        bestScoreText.text = _currentBestScore.ToString();
    }

	public void ResetBestScore()
	{
        PlayerPrefs.DeleteKey("bestScore");
	}

    private void notice()
    {
        if (GameEvent.bestScoreReached)
            icon.gameObject.SetActive(true);
        else
        {
            icon.gameObject.SetActive(false);
        }
            
    }
}
