using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class RobotController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private Vector2 smoothDeltaPosition = Vector2.zero;
    private Vector2 velocity = Vector2.zero;

    public Transform head;
    private Vector3 lookAtTargetPosition;
    private Vector3 lookAtPosition;

    public Transform target;
    // public bool looking = true;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        // Don’t update position automatically
        agent.updatePosition = false;
       // agent.updateRotation = false;

        if (!head)
        {
            Debug.LogError("No head transform - LookAt disabled");
            enabled = false;
            return;
        }
        
        lookAtTargetPosition = head.position + transform.forward;
        lookAtPosition = lookAtTargetPosition;

        if (target)
        {
            agent.destination = target.position;
        }
        else
        {
            agent.destination = Camera.main.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

       
        // Update animation parameters
        anim.SetBool("Moving", shouldMove);
        anim.SetFloat("Direction", velocity.y);
        //anim.SetFloat("vely", velocity.y);
        if (!shouldMove)
            return;
        lookAtTargetPosition = agent.steeringTarget + transform.forward;

        if (worldDeltaPosition.magnitude > agent.radius)
        {
            agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
           // transform.LookAt(lookAtTargetPosition);
        }
        //OnAnimatorIK();
    }
    
    
    
    void OnAnimatorMove()
    {
        // Update position based on animation movement using navigation surface height
        Vector3 position = anim.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;

       // Debug.Log("OnAnimatorMove");
    }
}
