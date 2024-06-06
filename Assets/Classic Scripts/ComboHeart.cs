using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHeart : MonoBehaviour
{
    private Animator _animator;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	public void Disable()
	{
		gameObject.SetActive(false);
	}
	private void Update()
	{
		if (GameEvent.Combo)
			_animator.SetBool("Combo", true);
		if (!GameEvent.Combo)
		{
			_animator.SetBool("Combo", false);
		}
	}
}
