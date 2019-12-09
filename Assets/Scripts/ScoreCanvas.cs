using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCanvas : MonoBehaviour

{

    public Transform head;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move with head position
        gameObject.transform.position = new Vector3(
            head.position.x - 4,
            gameObject.transform.position.y,
            gameObject.transform.position.z);
    }
}
