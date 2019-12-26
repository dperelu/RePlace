using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingAnimation : MonoBehaviour
{

	public GameObject anim;

	public void shoot()
	{
		anim.SetActive(true);
	}

	public void Noshoot()
	{
		anim.SetActive(false);
	}
}
