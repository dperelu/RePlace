using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { FLYING, SLIME, TURRET };

public class Target : MonoBehaviour {

    public float health = 100.0f;
    public GameObject prize;
    public EnemyType tipo = EnemyType.FLYING;

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0.0f)
        {
            Instantiate(prize, transform.position + new Vector3(0, 1, 0), transform.rotation);
            Transform o = transform.parent;
            if (o != null) Destroy(o.gameObject);
            else Destroy(gameObject);

			FindObjectOfType<DoorBehaviour>().eliminateEnemy();

        }
    }
}
