using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikesSpawner : MonoBehaviour
{
	public GameObject[] obstaclePrefabs;
	public int[] probIntervals;


	//the number of seconds until an obstacle spawns
	public float spawnInterval = 1.4f;

	//the number of seconds until the number of "types" of the obstacles that can spawn increases
	public float obstacleChangeInterval = 17;

	//public float timeUntilMinSpawn = 60*5;
	//public float minSpawnInterval = 1;


	public float minSpawnZ = -1.5f;
	public float maxSpawnZ = 1.5f;

	public float minSpawnY = 0;
	public float maxSpawnY = 1.5f;

	public float minSpawnWhiteY = 1.5f;
	public float maxSpawnWhiteY = 1.6f;

	public float minSpawnWhiteZ = -1f;
	public float maxSpawnWhiteZ = 1f;


	private int indexLimit = 0;
	private float timeLeftTillSpawn;
	private float timeTillObstacleChange;

	// Start is called before the first frame update
	void Start()
    {
		timeLeftTillSpawn = spawnInterval;
		timeTillObstacleChange = obstacleChangeInterval;
	}

    // Update is called once per frame
    void Update()
    {
		spawnInterval = Mathf.Max(spawnInterval - Time.deltaTime*0.005f, 1);

		timeLeftTillSpawn -= Time.deltaTime;
		timeTillObstacleChange -= Time.deltaTime;

		if (timeTillObstacleChange < 0)
		{
			//increase the types of obstacles that can spawn
			indexLimit = Mathf.Min(indexLimit + 1, obstaclePrefabs.Length-1);

			timeTillObstacleChange = obstacleChangeInterval;
		}

		if (timeLeftTillSpawn < 0)
		{
			float prob = Random.Range(0, 100);

			Debug.Log(probIntervals[indexLimit]);
			int index = 0;
			for(int i = 1; i< obstaclePrefabs.Length; i++)
			{
				if (probIntervals[i - 1] < prob && probIntervals[i] >= prob)
					index = i;

			}
			index = Mathf.Min(index, indexLimit);
			

			float randomZ = Random.Range(minSpawnZ, maxSpawnZ);
			


			GameObject objectToSpawn = obstaclePrefabs[index];

			Vector3 spawnLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z);


			if (!objectToSpawn.CompareTag("unbreakableVertical") && !objectToSpawn.CompareTag("UnbreakableCube"))
			{
				spawnLocation.y += Random.Range(minSpawnY, maxSpawnY);
				spawnLocation.z += Random.Range(minSpawnZ, maxSpawnZ);
			}
			else if (objectToSpawn.CompareTag("UnbreakableCube"))
			{
				spawnLocation.y += Random.Range(minSpawnWhiteY, maxSpawnWhiteY);
			}
			else
			{
				spawnLocation.z += Random.Range(minSpawnWhiteZ, maxSpawnWhiteZ);
			}

			Instantiate(objectToSpawn, spawnLocation, objectToSpawn.transform.rotation);


			//resets the spawn time
			timeLeftTillSpawn = spawnInterval;
		}

	}
}
