using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchUnbreakableCube : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("SmallUnbreakable"))
		{
			Score.score += 10;
			other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
			other.gameObject.GetComponent<MeshRenderer>().enabled = false;
			GetComponent<AudioSource>().Play();
			StartCoroutine(DestroyCube(other.gameObject));
		}
	}

	IEnumerator DestroyCube(GameObject go)
	{
		yield return new WaitForSeconds(3f);

		Destroy(go);
	}
}
