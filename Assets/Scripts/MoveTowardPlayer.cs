using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    public float bounceSpeed;

    public bool rotate, bouncing;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate)
            rb.velocity = Vector3.right * speed;
        else
            transform.position += Vector3.right * speed*Time.deltaTime;

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(bouncing)
        {
            Debug.Log(collision.gameObject.name);
            if(collision.transform.CompareTag("Floor"))
            {
                rb.velocity =  new Vector3(rb.velocity.x, bounceSpeed, 0);
            }
        }
    }
}
