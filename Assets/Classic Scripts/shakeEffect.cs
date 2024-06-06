using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeEffect : MonoBehaviour
{
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 1f;

    private Vector3 initialPosition;
    private Coroutine shakeCoroutine;

	private void Start()
	{
		initialPosition = transform.localPosition;
	}

	public void TriggerShake()
	{
		if (shakeCoroutine != null)
		{
			StopCoroutine(shakeCoroutine);
			transform.localPosition = initialPosition;
		}
		shakeCoroutine = StartCoroutine(Shake());
	}

	private IEnumerator Shake()
	{
		float elapsed = 0.0f;

		while (elapsed < shakeDuration)
		{
			float x = Random.Range(-1f, 1f) * shakeMagnitude;
			float y = Random.Range(-1f, 1f) * shakeMagnitude;

			transform.localPosition = new Vector3(initialPosition.x + x, initialPosition.y + y, initialPosition.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = initialPosition;
	}
}
