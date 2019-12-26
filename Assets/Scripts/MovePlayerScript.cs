using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayerScript : MonoBehaviour
{

	public Transform dropSP;

	public GameObject floor1, floor2;

	public Image img;

	public void startNotFade()
	{

		GeneratedObject[] go = FindObjectsOfType<GeneratedObject>();

		foreach (GeneratedObject g in go)
		{
			Destroy(g.gameObject);
		}


		floor1.SetActive(true);
		floor2.SetActive(true);

		FindObjectOfType<RandomWorldGenerator>().create();

        Camera.main.GetComponent<GlitchEffect>().enabled = true;
        StartCoroutine(showImg());
	}

	IEnumerator showImg()
	{
		for (float i = 1; i >= 0.1; i -= Time.deltaTime)
		{

			Color c = new Color();

			c.r = img.color.r;
			c.g = img.color.g;
			c.b = img.color.b;

			c.a = i;

			img.color = c;

			GameObject.FindGameObjectWithTag("Player").transform.position = dropSP.transform.position;

			yield return null;
		}


        FindObjectOfType<GameManagerComp>().Setlevelinitialized(false);
        FindObjectOfType<GameManagerComp>().Init();

	}
}
		
