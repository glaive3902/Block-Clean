using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public GameObject bannderAD;
	public GameObject initAD;
	public GameObject rewardAD;
	private void Awake()
	{
		//if(Application.isEditor == false)
			//Debug.unityLogger.logEnabled = false;
		DontDestroyOnLoad (bannderAD);
		DontDestroyOnLoad (initAD);
		DontDestroyOnLoad (rewardAD);

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
