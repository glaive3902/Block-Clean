using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public Image ComboEffect;
    public static int currentScore;

    private int _bestScore = 0;
    private Coroutine _ScoreCoroutine;
    void Start()
    {
        currentScore = 0;
        _bestScore = PlayerPrefs.GetInt("bestScore");
        UpdateScoreText();
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
        if (GameEvent.Combo)
        {
            ComboEffect.gameObject.SetActive(true);
        }
        
    }
	private void Addscore(int score)
    {
        Debug.Log("them "+score+" diem");
        int targetscore = currentScore + score;
		if (_ScoreCoroutine != null)
		{
			StopCoroutine(_ScoreCoroutine);
		}

		_ScoreCoroutine = StartCoroutine(AnimateScore(currentScore, targetscore));


        //UpdateScoreText();
        if (targetscore >= _bestScore)
        {
            GameEvent.bestScoreReached = true;
            _bestScore = targetscore;
            PlayerPrefs.SetInt("bestScore", _bestScore);
        }
    }
    private IEnumerator AnimateScore(int startScore, int endScore)
    {
        float duration = 0.5f; // thoi gian chay hieu ung diem so
	    float elapsed = 0.0f;
        while (elapsed < duration)
        {

			
	        elapsed += Time.deltaTime;
            currentScore = (int)Mathf.Lerp(startScore, endScore, elapsed/duration);
            UpdateScoreText();
            yield return null;
        }
        currentScore = endScore;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void SetNewGameScore()
    {
        currentScore = 0;
        UpdateScoreText();
		ComboEffect.gameObject.SetActive(false);
	}
}
