using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent (typeof(Rigidbody))]
public class Slice : MonoBehaviour
{
	public float batSpeed = 200f;

    public Material capMaterial;
	public GameObject Health;
	public GameObject CamRig;
	public GameObject Spawner;
	public GameObject ShieldSphere;

	Rigidbody simulator;

	public SteamVR_TrackedObject rightHand;


	//public SteamVR_Action_Vibration g;

	private void Start()
	{
		simulator = new GameObject().AddComponent<Rigidbody>();
		simulator.name = "Simulator1";
		simulator.transform.parent = transform.parent;
	}

	private void Update()
	{
		simulator.velocity = (transform.position - simulator.position) * batSpeed;
	}

	private void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.CompareTag("SmallUnbreakable"))
		{
			SteamVR_Controller.Input((int)rightHand.index).TriggerHapticPulse(500);

			Handheld.Vibrate();

			GameObject smallUnbreakableCube = collision.collider.gameObject;
			Rigidbody rb = smallUnbreakableCube.GetComponent<Rigidbody>();

			smallUnbreakableCube.GetComponent<MoveTowardPlayer>().enabled = false;
			rb.isKinematic = false;
			rb.freezeRotation = false;
			rb.velocity = simulator.velocity;
		}

		if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("ObstacleSliced") || collision.collider.CompareTag("shield") || collision.collider.CompareTag("Slow") || collision.collider.CompareTag("Bomb"))
		{
			SteamVR_Controller.Input((int)rightHand.index).TriggerHapticPulse(500);

			Handheld.Vibrate();

			if (collision.collider.CompareTag("shield"))
			{
				collision.collider.gameObject.GetComponent<MoveTowardPlayer>().enabled = false;
			}


			GetComponent<AudioSource>().Play();

			GameObject victim = collision.collider.gameObject;
			victim.transform.GetChild(0).gameObject.SetActive(true);
			victim.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();

			if (collision.collider.CompareTag("Bomb"))
			{
				victim.transform.GetChild(1).gameObject.SetActive(true);
				victim.GetComponent<MeshRenderer>().enabled = false;
				StartCoroutine(Explosion());
			}

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
				ShieldSphere.SetActive(true);
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

	IEnumerator Explosion()
	{
		yield return new WaitForSeconds(0.3f);

		SceneManager.LoadScene("GameOverScene");
	}

	IEnumerator DisableShield()
	{
		yield return new WaitForSecondsRealtime(10);
		CamRig.GetComponent<PlayerHealth>().shieldOn = false;

		Debug.Log("Shield Off");
		Health.GetComponent<HealthIndicator>().shieldHealth.color = Color.green;
		ShieldSphere.SetActive(false);
	}

	IEnumerator SlowDown()
	{
		yield return new WaitForSecondsRealtime(10);
		Spawner.GetComponent<Spawner>().slowDown = false;
	}
}
