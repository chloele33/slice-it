using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
	public GameObject[] ignorePrefabs;

    void Awake()
	{
		//foreach(GameObject go in ignorePrefabs)
		//{
		//	Physics.IgnoreCollision(go.transform.GetComponent<Collider>(), GetComponent<Collider>());
		//}
		Physics.IgnoreLayerCollision(8, 9);
	}
}
