using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLightsaberController : MonoBehaviour
{
    private RobotController robotController;
    // Start is called before the first frame update
    void Start()
    {
        robotController = GetComponentInParent<RobotController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("testigngg");
    }
}
