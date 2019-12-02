using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while(true)
        {
            GameObject go = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);

            if (go.tag != "UnbreakableCube")
            {
                Rigidbody rb = go.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(5f, 0f, 0f);
                //rb.useGravity = true;

                Vector3 pos = transform.position;
                pos.y += Random.Range(-1f, 2f);
                pos.z += Random.Range(-2f, 2f);
                go.transform.position = pos;
            }
            else
            {
                Vector3 pos = transform.position;
                pos.y += Random.Range(0.5f, 2f);
                go.transform.position = pos;
            }

            

            yield return new WaitForSeconds(waitTime);
        }
    }
}
