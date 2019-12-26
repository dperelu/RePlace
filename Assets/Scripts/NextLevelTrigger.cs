using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelTrigger : MonoBehaviour {

	public GameObject floor1, floor2;

	public Image img;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			floor1.SetActive(false);
			floor2.SetActive(false);

			StartCoroutine(FadeImage());
		}		
	}

	IEnumerator FadeImage()
	{
		for (float i = 0; i <= 1; i += Time.deltaTime)
		{

			Color c = new Color();

			c.r = img.color.r;
			c.g = img.color.g;
			c.b = img.color.b;

			c.a = i;

			img.color = c;

			yield return null;
		}

		FindObjectOfType<MovePlayerScript>().startNotFade();
	}

}
