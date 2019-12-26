using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RobotEnemyController : MonoBehaviour {


    public float radius;
    public int damage_ = 1;
    bool shooting;
    GameObject player;
	bool crashingOnShield = false;
    public AudioSource audiosSource;

	RaycastHit hit;
	Ray ray;

	public Transform rayoriPos;

	LineRenderer line;

	public GameObject shieldCrash;

	// Use this for initialization
	void Start () {
        radius = GetComponent<SphereCollider>().radius;
		player = GameObject.FindGameObjectWithTag("Player");
		line = GetComponentInChildren<LineRenderer>();
		//line.transform.position = rayoriPos.position;
		line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (shooting) shoot();

		Vector3 t = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
		gameObject.transform.LookAt(t);

		ray = new Ray(rayoriPos.transform.position, player.transform.position - rayoriPos.transform.position);

		if (Physics.Raycast(ray, out hit) && shooting)
		{

			if (hit.rigidbody != null)
			{ 
				if (hit.transform.CompareTag("Shield"))
				{
					
					crashingOnShield = true;
					Instantiate(shieldCrash, hit.point, Quaternion.identity);
					GetComponent<Target>().TakeDamage(damage_);
				}

				else
				{
					crashingOnShield = false;

				}//hit.rigidbody.AddForceAtPosition(ray.direction * pokeForce, hit.point);
			}
		}

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shooting = true;
			line.enabled = true;
            audiosSource.Play();

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shooting = false;
			line.enabled = false;
            audiosSource.Stop();

        }
    }

    void shoot()
    {
		if (crashingOnShield) { 
			line.SetPosition(0, rayoriPos.position);
			line.SetPosition(1, hit.point);
		}
		else
		{
			line.SetPosition(0, rayoriPos.position);
			line.SetPosition(1, player.transform.position);
		}
		


		if (!player.GetComponent<HealthScore>().shield_)
        {
            player.GetComponent<HealthScore>().ReduceHealth(damage_);
        }
    }
}
