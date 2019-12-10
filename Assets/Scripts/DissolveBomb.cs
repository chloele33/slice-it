using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveBomb : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Bomb"))
		{
			other.gameObject.GetComponent<SpawnEffect>().enabled = true;
		}
	}
}
