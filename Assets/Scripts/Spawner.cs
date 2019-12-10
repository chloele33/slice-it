using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float waitTime;
	public bool slowDown = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while(true)
        {
			float prob = Random.Range(0, 100);
			GameObject go;

			// blue
			if (prob <= 70)
			{
				go = Instantiate(obstaclePrefabs[0]);
			}
			else if (prob <= 80) // red
			{
				go = Instantiate(obstaclePrefabs[1]);
			}
			else if (prob <= 85) // purple
			{
				go = Instantiate(obstaclePrefabs[2]);
			}
			else if (prob <= 95) // unbreakable vertical
			{
				go = Instantiate(obstaclePrefabs[4]);
			}
			else
			{
				go = Instantiate(obstaclePrefabs[3]);
			}

            //GameObject go = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);

			if (slowDown)
			{
				--go.GetComponent<MoveTowardPlayer>().speed;
			}

            if (go.tag != "UnbreakableCube" && go.tag != "unbreakableVertical")
            {
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(5f, 0f, 0f);
                //rb.useGravity = true;

                Vector3 pos = transform.position;
                pos.y += Random.Range(0f, 1.5f);
                pos.z += Random.Range(-2f, 1.5f);
                go.transform.position = pos;
            }
            else if (go.tag == "UnbreakableCube")
            {
                Vector3 pos = transform.position;
                pos.y += Random.Range(1.5f, 1.6f);
                go.transform.position = pos;
            }
			else
			{
				Vector3 pos = transform.position;
				//pos.y += Random.Range(0f, 0f);
				pos.z += Random.Range(-1f, 1f);
				go.transform.position = pos;
			}

            

            yield return new WaitForSeconds(waitTime);
        }
    }
}
