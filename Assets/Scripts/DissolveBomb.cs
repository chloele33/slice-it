using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveBomb : MonoBehaviour
{
	public Material unbreakableDissolveMat = null;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Bomb"))
		{
			Debug.Log("BHit");
			other.gameObject.GetComponent<SpawnEffect>().enabled = true;
			other.gameObject.GetComponent<SphereCollider>().enabled = false;
			StartCoroutine(DestroyBomb(other.gameObject));
		}		
	}

	private void OnCollisionEnter(Collision collision)
	{		
		if (collision.collider.CompareTag("Bomb"))
		{
			Debug.Log("BHit");
			collision.collider.gameObject.GetComponent<SpawnEffect>().enabled = true;
			collision.collider.gameObject.GetComponent<SphereCollider>().enabled = false;
			StartCoroutine(DestroyBomb(collision.collider.gameObject));

			if (this.gameObject.tag == "SmallUnbreakable" ||
			this.gameObject.tag == "UnbreakableCube" ||
			this.gameObject.tag == "unbreakableVertical")
			{
				Debug.Log("Hit");
				GetComponent<MeshRenderer>().material = unbreakableDissolveMat;
				GetComponent<SpawnEffect>().enabled = true;
				GetComponent<BoxCollider>().enabled = false;
				StartCoroutine(DestroyBomb(this.gameObject));
			}
		}
	}

	IEnumerator DestroyBomb(GameObject go)
	{
		yield return new WaitForSeconds(3f);

		Destroy(go);
	}
}
