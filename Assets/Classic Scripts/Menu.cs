using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	private void Awake()
	{
		if(Application.isEditor == false)
			Debug.unityLogger.logEnabled = false;
	}

	private void Start()
	{
		Application.targetFrameRate = -1;
		Application.targetFrameRate = 60;
	}
	public void LoadScene(string name)
	{
		SceneManager.LoadScene(name);
	}


}
