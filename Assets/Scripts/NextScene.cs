using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextScene : MonoBehaviour {

	public string SceneName;

	public void onClick()
	{
		SceneManager.LoadScene(SceneName);
	}
}
