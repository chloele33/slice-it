using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetForNextLevel : MonoBehaviour
{
	public Transform[] objectsWhosePositionMustBeReset;
	private Vector3 [] originalPositions;

	

	
    // Start is called before the first frame update
    void Start()
    {
		originalPositions = new Vector3[objectsWhosePositionMustBeReset.Length];

		for (int i=0;i<objectsWhosePositionMustBeReset.Length;i++)
		{
			originalPositions[i] = objectsWhosePositionMustBeReset[i].position;
		}
		

		gameObject.SetActive(false);

		//Debug.Log("turning off");

	}

	private void OnTriggerEnter(Collider other)
	{
		//still need to remove all spawned gameobjects
		if(other.CompareTag("Player"))
		{
			for (int i = 0; i < objectsWhosePositionMustBeReset.Length; i++)
			{
				objectsWhosePositionMustBeReset[i].position = originalPositions[i];
			}

			Debug.Log("next level. all hail penguins");

			Start();
		}
	}
}
