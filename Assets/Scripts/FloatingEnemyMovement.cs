using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class FloatingEnemyMovement : MonoBehaviour {

	GameObject target = null;
	public float distance = 50f;
	public float time = 2;
	public NavMeshAgent agent;
    public Transform gun;
    public GameObject bulletPrefab;
    public float bulletSpeed = 50.0f;
    public float fireRate = 1.0f;
    private float nextFire;
    public AudioSource audioSource;

    bool canMove = false;

	private void Start()
	{
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").gameObject;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			canMove = true;
		}
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time > nextFire)
        {
            audioSource.Play();
            nextFire = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
            Rigidbody rb = bullet.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = (other.gameObject.transform.position - gun.position) * bulletSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canMove = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.1f / distance * Mathf.Sin(Time.time * time)
		 , this.transform.position.z);

		if (canMove)
		{
			agent.SetDestination(target.transform.position);

		}
	}
}
