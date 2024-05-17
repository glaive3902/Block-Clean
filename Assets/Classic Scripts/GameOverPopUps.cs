using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameOverPopUps : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

	private void OnDisable()
	{
		GameEvent.GameOver += OnGameOver;
	}

	private void OnEnable()
	{
		GameEvent.GameOver -= OnGameOver;
	}

	public void OnGameOver()
	{

	}

}
