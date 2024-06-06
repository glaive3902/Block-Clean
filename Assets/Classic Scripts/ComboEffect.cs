using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboEffect : MonoBehaviour
{
	public shakeEffect _shakeEffect;
	public void Disable()
	{
		gameObject.SetActive(false);
	}

	public void Shake()
	{
		_shakeEffect.TriggerShake();
	}
}
