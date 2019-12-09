using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Slice : MonoBehaviour
{
    public Material capMaterial;
	public GameObject Health;
	public GameObject CamRig;
	public GameObject Spawner;

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("ObstacleSliced") || collision.collider.CompareTag("Bomb") || collision.collider.CompareTag("shield") || collision.collider.CompareTag("Slow"))
		{
			GetComponent<AudioSource>().Play();

			GameObject victim = collision.collider.gameObject;
			victim.transform.GetChild(0).gameObject.SetActive(true);
			victim.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();

			GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

			if (!pieces[1].GetComponent<Rigidbody>())
			{
				pieces[1].AddComponent<Rigidbody>();
				MeshCollider temp = pieces[1].AddComponent<MeshCollider>();
				temp.convex = true;
			}

			Destroy(pieces[1], 1);

			if (collision.collider.CompareTag("shield"))
			{
				CamRig.GetComponent<PlayerHealth>().shieldOn = true;
				//Health.GetComponent<HealthIndicator>().shieldHealth.enabled = true;
				Health.GetComponent<HealthIndicator>().shieldHealth.color = new Color(0, 212, 255);
				Debug.Log("Shield On");
				StartCoroutine(DisableShield());
			}

			if (collision.collider.CompareTag("Slow"))
			{
				Spawner.GetComponent<Spawner>().slowDown = true;
				StartCoroutine(SlowDown());
			}

			if (collision.collider.CompareTag("ObstacleSliced"))
			{
				Score.score += 5;
			}

			else if (collision.collider.CompareTag("Obstacle"))
			{
				Score.score += 20;
				victim.tag = "ObstacleSliced";
			}
		}
    }

	IEnumerator DisableShield()
	{
		yield return new WaitForSecondsRealtime(10);
		CamRig.GetComponent<PlayerHealth>().shieldOn = false;

		Debug.Log("Shield Off");
		Health.GetComponent<HealthIndicator>().shieldHealth.color = Color.green;
	}

	IEnumerator SlowDown()
	{
		yield return new WaitForSecondsRealtime(10);
		Spawner.GetComponent<Spawner>().slowDown = false;
	}
}
