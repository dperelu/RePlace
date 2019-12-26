using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	bool setAvaliable = false;

	bool created = false;

	private void Start()
	{
		//gameObject.SetActive(true);
		setAvaliable = true;
	}


	public void set(bool s)
	{
		setAvaliable = s;

		if (s)
		{
			gameObject.SetActive(true);
			created = false;
		}

		else gameObject.SetActive(false);

	}
}
