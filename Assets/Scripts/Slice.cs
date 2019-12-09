using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Slice : MonoBehaviour
{
    public Material capMaterial;
	public GameObject Health;
	public GameObject Spawner;

    private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Bomb") || collision.collider.CompareTag("shield") || collision.collider.CompareTag("Slow"))
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
				Health.GetComponent<HealthIndicator>().shieldOn = true;
				StartCoroutine(DisableShield());
			}

			if (collision.collider.CompareTag("Slow"))
			{
				Spawner.GetComponent<Spawner>().slowDown = true;
				StartCoroutine(SlowDown());
			}
		}
    }

	IEnumerator DisableShield()
	{
		yield return new WaitForSecondsRealtime(10);
		Health.GetComponent<HealthIndicator>().shieldOn = false;
	}

	IEnumerator SlowDown()
	{
		yield return new WaitForSecondsRealtime(10);
		Spawner.GetComponent<Spawner>().slowDown = false;
	}
}
