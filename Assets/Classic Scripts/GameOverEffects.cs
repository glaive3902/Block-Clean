using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEffects : MonoBehaviour
{
	AudioManager audioManager;
	// Start is called before the first frame update
	void Start()
    {
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
		//audioManager.PlaySFX(audioManager.lose);
	}

    public void PlayGameOverSound()
	{
		audioManager.PlaySFX(audioManager.lose);
	}
}
