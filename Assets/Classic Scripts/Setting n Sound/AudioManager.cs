using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Header("Audio Sources")]
    [SerializeField] AudioSource musicSources;
	[SerializeField] AudioSource SFXSources;

	[Header("Audio clips")]
    public AudioClip backGround;
	public AudioClip NewGame;
	public AudioClip singleLine;
	public AudioClip doubleLines;
	public AudioClip MoreThanFour;
	public AudioClip invalid;
	public AudioClip Click;
	public AudioClip lose;
	public AudioClip win;
	public AudioClip pickUp1;
	public AudioClip pickUp2;
	public AudioClip tripleLines;
	public AudioClip putDown;

	private void Start()
	{
		musicSources.clip = backGround;
		musicSources.Play();
	}

	public void PlaySFX(AudioClip clip)
	{
		SFXSources.PlayOneShot(clip);
	}

	public void ButtonClick()
	{
		SFXSources.PlayOneShot(Click);
	}

	public void WinOrLoseClick()
	{
		if (GameEvent.bestScoreReached)
			SFXSources.PlayOneShot(win);

		else
		{
			SFXSources.PlayOneShot(lose);
		}
	}
}
