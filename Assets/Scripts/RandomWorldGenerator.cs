using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomWorldGenerator : MonoBehaviour {

	public GameObject [] objectsToCreate;
	public int minNumberOfObjectsTocreate;
	public int maxNumberOfObjectsTocreate;
	public GameObject floorPos;
	public int maxRotation, minRotation;
    public MusicSelector musicSelector;

	private void Start()
	{
		create();
	}

	// Use this for initialization
	public void create () {

		int rnd = Random.Range(minNumberOfObjectsTocreate, maxNumberOfObjectsTocreate);

        for (int i = 0; i < rnd; i++)
		{
			int randomObject = Random.Range(0, objectsToCreate.Length);

            float posX, posZ;
			random(out posX, out posZ);

			int rotationX = Random.Range(minRotation, maxRotation);
			int rotationY = Random.Range(minRotation, maxRotation);
			int rotationZ = Random.Range(minRotation, maxRotation);

			GameObject c = Instantiate(objectsToCreate[randomObject], new Vector3(posX, floorPos.transform.position.y + floorPos.transform.localScale.y / 2, posZ), Quaternion.identity);
			

			c.transform.rotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));
		}

		FindObjectOfType<NavMeshSurface>().BuildNavMesh();
        musicSelector.setSong();

    }

	void random(out float x, out float z)
	{

		int factor1 = 1;
		int rnd = Random.Range(0, 2);
		if (rnd == 0)
			factor1 = -1;
		z = Random.Range(floorPos.transform.position.z, floorPos.transform.position.z + factor1 * floorPos.transform.localScale.z / 2);


		int factor2 = 1;
		int rnd2 = Random.Range(0, 2);
		if (rnd2 == 0)
			factor2 = -1;
		x = Random.Range(floorPos.transform.position.x, floorPos.transform.position.x + factor2 * floorPos.transform.localScale.x / 2);
	}


}
