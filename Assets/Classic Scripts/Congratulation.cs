using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congratulation : MonoBehaviour
{
    public List<GameObject> Effects = new List<GameObject>();
    void Start()
    {
        GameEvent.ShowEffects += ShowEffects;

	}

	private void OnDisable()
	{
		GameEvent.ShowEffects -= ShowEffects;
	}

    private void ShowEffects()
    {
        var index = UnityEngine.Random.Range(0, Effects.Count);
        Effects[index].SetActive(true);
    }
}
