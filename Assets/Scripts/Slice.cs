using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Slice : MonoBehaviour
{
    public Material capMaterial;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle")
            || collision.collider.CompareTag("Bomb")
            || collision.collider.CompareTag("ObstacleSliced"))
		{
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

            // Update score
            if (collision.collider.CompareTag("ObstacleSliced"))
            {
                Score.score += 5;
            }

            else if (collision.collider.CompareTag("Obstacle"))
            {
                Score.score += 20;
                victim.tag = "ObstacleSliced";
            }

            Destroy(pieces[1], 1);
		}
    }
}
