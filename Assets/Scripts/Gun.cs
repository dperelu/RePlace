using UnityEngine;
using UnityEngine.Audio;

public class Gun : MonoBehaviour {

    public float damage = 10.0f;
    public float range = 100.0f;
    public Camera fpsCam;
    public GameObject impactEffect;
    public AudioSource shootSFX;
    public float fireRate = 0.1f;
    private float nextFire;
    bool leech = false;
    public EnemyType gunpower = EnemyType.FLYING;

    void Start()
    {
        shootSFX = GetComponent<AudioSource>();
    }

     void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetMouseButtonUp(0))
        {
            stopShooting();
        }
	}

    public void setPower(int power)
    {
        GameObject g = gameObject.transform.Find("Pila").gameObject;
        leech = false;

        if (power == 1)
        {
            gunpower = EnemyType.SLIME;
            g.GetComponent<Renderer>().material.color = new Color32(142, 21, 161, 255);
        }
        else if (power == 2)
        {
            gunpower = EnemyType.TURRET;
            g.GetComponent<Renderer>().material.color = new Color32(11, 51, 127, 255);
        }
        else if (power == 3)
        {
            leech = true;
            gunpower = EnemyType.FLYING;
            g.GetComponent<Renderer>().material.color = new Color32(255, 113, 0, 255);
        }
            
    }

    void Shoot()
    {
        shootSFX.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
			FindObjectOfType<ShootingAnimation>().shoot();
            Target target = hit.transform.GetComponent<Target>();
            if (target != null && gunpower == target.tipo)
            {
                if (gunpower == EnemyType.FLYING) GameObject.FindGameObjectWithTag("Player").GetComponent<HealthScore>().ReduceHealth(-(int)damage);
                target.TakeDamage(damage);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2.0f);
        }
    }

	void stopShooting()
	{
		FindObjectOfType<ShootingAnimation>().Noshoot();
	}
}
