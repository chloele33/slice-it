using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BombDeath : MonoBehaviour
{
	private void Update()
	{
		//Debug.Log("Before If");
		if ((transform.position.x >= 15f && transform.position.x <= 17f)
			&& (transform.position.z >= 22f && transform.position.z <= 24f)
			&& (transform.position.y >= 0f && transform.position.y <= 2f))
		{
			//Debug.Log("Explode");
			transform.GetChild(1).gameObject.SetActive(true);
			GetComponent<MeshRenderer>().enabled = false;
			StartCoroutine(Explosion());
		}
	}

	IEnumerator Explosion()
	{
		yield return new WaitForSeconds(0.3f);

		SceneManager.LoadScene("GameOverScene");
	}
}
