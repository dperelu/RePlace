using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("Dest", 0.5f);
	}
	
	void Dest()
	{
		Destroy(gameObject);
	}
}
