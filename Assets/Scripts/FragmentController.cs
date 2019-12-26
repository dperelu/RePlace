using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentController : MonoBehaviour {

    public float speed = 10.0f;
    public int pointsEarned_ = 10;
    Transform playerTransform;
    GameManagerComp gameManager;
	// Use this for initialization
	void Start () {
        gameManager = GameObject.FindObjectOfType<GameManagerComp>();
	}
	
	// Update is called once per frame
	void Update () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // SUMAR PUNTOS
            gameManager.IncreaseScore(pointsEarned_);
            Destroy(gameObject);
        }
    }
}
