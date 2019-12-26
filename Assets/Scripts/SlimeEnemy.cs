using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemy : MonoBehaviour {

    public NavMeshAgent agent_;
    public int damage_ = 1;
    GameObject player_;
    GameObject target_;


    // Use this for initialization
    void Start ()
    {
        target_ = GameObject.FindGameObjectWithTag("Player");

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player_ = other.gameObject;
            player_.GetComponent<HealthScore>().ReduceHealth(damage_);
        }
    }

    void FixedUpdate()
    {
        agent_.SetDestination(target_.transform.position);
    }

    void Update () {
		
	}
}
