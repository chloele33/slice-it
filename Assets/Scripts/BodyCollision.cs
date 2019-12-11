using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BodyCollision : MonoBehaviour
{
    public Transform head;

    public GameObject cam;

    bool detectedBefore = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(head.position.x, head.position.y, head.position.z);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "UnbreakableCube" || collision.gameObject.tag == "unbreakableVertical" || collision.gameObject.tag == "SmallUnbreakable")
        {
            if (detectedBefore)
            {
                return;
            }

			collision.collider.gameObject.GetComponent<AudioSource>().Play();

            ObjectHit obj = collision.gameObject.GetComponent<ObjectHit>();

            if (obj.detectedBefore == false)
            {
                Debug.Log("Collided with obstacle");
                //gameObject.GetComponent<PlayerHealth>().ModifyHealth(1);
                //FindObjectOfType<PlayerHealth>().ModifyHealth(1);
                cam.GetComponent<PlayerHealth>().ModifyHealth(1);
                obj.detectedBefore = true; ;
                detectedBefore = true;
            }            
        }

		if (collision.collider.CompareTag("Bomb"))
		{
			collision.collider.gameObject.transform.GetChild(1).gameObject.SetActive(true);
			collision.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;

			StartCoroutine(Explosion());
		}
    }

	IEnumerator Explosion()
	{
		yield return new WaitForSeconds(0.3f);

		SceneManager.LoadScene("GameOverScene");
	}

	void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "UnbreakableCube" || collision.gameObject.tag == "unbreakableVertical" || collision.gameObject.tag == "SmallUnbreakable")
        {
            detectedBefore = false;
        }
    }
}
