using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorBehaviour : MonoBehaviour {

	int numberOfEnemies = 0;

	public TextMeshProUGUI text;

	
	public void setEnemies(int enem)
	{
		gameObject.SetActive(true);

		numberOfEnemies = enem;

		text.text = "Enemies left: " + numberOfEnemies.ToString();
	}

	public void eliminateEnemy()
	{
		numberOfEnemies--;
		text.text = "Enemies left: " + numberOfEnemies.ToString();

		if (numberOfEnemies <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
