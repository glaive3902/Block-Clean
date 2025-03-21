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
	public AudioClip combo1;
	public AudioClip combo2;
	public AudioClip combo3;
	public AudioClip combo4;
	public AudioClip combo5;
	public AudioClip combo6;
	public AudioClip combo7;
	public AudioClip combo8;
	public AudioClip combo9;
	public AudioClip combo10;
	public AudioClip combo11;
	public AudioClip combo12;
	public AudioClip combo13;
	public AudioClip combo14;
	public AudioClip combo15;
	public AudioClip invalid;
	public AudioClip Click;
	public AudioClip lose;
	public AudioClip win;
	public AudioClip pickUp1;
	public AudioClip pickUp2;
	public AudioClip tripleLines;
	public AudioClip putDown;
	public AudioClip Revive;


	public GameObject SFXOn;
	public GameObject SFXOff;
	public GameObject MusicOn;
	public GameObject MusicOff;

	public int SFX;
	public int Music;
	private void Start()
	{
		SFX = PlayerPrefs.GetInt("SFX");
		Music = PlayerPrefs.GetInt("Music");
		musicSources.clip = backGround;
		musicSources.Play();
		SFXSettingCheck();
		MusicSettingCheck();
	}

	public void Update()
	{
		SFX = PlayerPrefs.GetInt("SFX");
		Music = PlayerPrefs.GetInt("Music");
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

	public void TurnOffSFX()
	{
		PlayerPrefs.SetInt("SFX", 0);
	}

	public void TurnOnSFX()
	{
		PlayerPrefs.SetInt("SFX", 1);
	}

	public void TurnOffMusic()
	{
		PlayerPrefs.SetInt("Music", 0);
	}

	public void TurnOnMusic()
	{
		PlayerPrefs.SetInt("Music", 1);
	}
	public void SFXSettingCheck()
	{
		if(SFX == 1)
		{
			SFXOn.gameObject.SetActive(true);
			SFXOff.gameObject.SetActive(false);
			SFXSources.mute = false;
		}
		else if(SFX == 0)
		{
			SFXOn.gameObject.SetActive(false);
			SFXOff.gameObject.SetActive(true);
			SFXSources.mute = true;
		}
	}

	public void MusicSettingCheck()
	{
		if (Music == 1)
		{
			MusicOn.gameObject.SetActive(true);
			MusicOff.gameObject.SetActive(false);
			musicSources.mute = false;
		}
		else if (Music == 0)
		{
			MusicOn.gameObject.SetActive(false);
			MusicOff.gameObject.SetActive(true);
			musicSources.mute = true;
		}
	}
}
