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
    
    public GameObject lightsaberOnBack;

    public GameObject lightsaberInHand;

    public GameObject animatingLightsaber;

    //how long it takes for the lightsaber to be transferred from the robot's back to its hand
    public float lightsaberTransferDuration;

    //prevents the player from being damage multiple times for a single attack
    public float delayBetweenPlayerDamage;

    private bool movingLightsaberFromBackToHand = false, movingLightsaberFromHandToBack = false;

    private bool moved = false;


    private float sum = 0;

    private bool canDamagePlayer = false;

    private float damageDelay;

    // Start is called before the first frame update
    void Start()
    {
        damageDelay = delayBetweenPlayerDamage;

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

        if (target == null)
        {
            target = Camera.main.transform;
        }
       // agent.destination = target.position;
    }

    bool IsClosestRobot()
    {
        GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");
        float thisRobotDist = Vector3.Distance(transform.position, target.position);

        foreach(GameObject robot in robots)
        {
            float otherRobotDist = Vector3.Distance(robot.transform.position, target.position);
            if (otherRobotDist < thisRobotDist)
                return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {


        if (movingLightsaberFromBackToHand)
        {
            sum += Time.deltaTime;
            Vector3 p = Vector3.Lerp(lightsaberOnBack.transform.position, lightsaberInHand.transform.position, sum / lightsaberTransferDuration);
            animatingLightsaber.transform.position = p;
            if (sum >= lightsaberTransferDuration)
            {
                lightsaberInHand.SetActive(true);

                animatingLightsaber.transform.parent = lightsaberInHand.transform.parent;

                animatingLightsaber.SetActive(false);
                movingLightsaberFromBackToHand = false;
                sum = 0;

                anim.speed = 1.0f;
            }
        }

        if (movingLightsaberFromHandToBack)
        {
            sum += Time.deltaTime;
            Vector3 p = Vector3.Lerp(lightsaberInHand.transform.position, lightsaberOnBack.transform.position, sum / lightsaberTransferDuration);
            animatingLightsaber.transform.position = p;
            if (sum >= lightsaberTransferDuration)
            {
                lightsaberOnBack.SetActive(true);

                animatingLightsaber.transform.parent = lightsaberOnBack.transform.parent;

                animatingLightsaber.SetActive(false);
                movingLightsaberFromHandToBack = false;
                sum = 0;

                anim.speed = 1.0f;
            }
        }
    
        if (!IsClosestRobot())
        {
            agent.isStopped = true;
            anim.SetBool("HoldingLightsaber", false);
            anim.SetBool("Moving", false);
            return;
        }
        agent.isStopped = false;
        agent.SetDestination(target.position);

        if (damageDelay > 0)
            damageDelay -= Time.deltaTime;

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

        // Debug.Log("velocity.magnitude > 0.5f is "+ (velocity.magnitude > 0.5f)+ " , agent.remainingDistance > agent.radius is "+(agent.remainingDistance > agent.radius));

        // Update animation parameters
        anim.SetBool("Moving", shouldMove);
        anim.SetFloat("Direction", velocity.y);
        //anim.SetFloat("vely", velocity.y);
        if (shouldMove)
        {

            lookAtTargetPosition = agent.steeringTarget + transform.forward;

            if (worldDeltaPosition.magnitude > agent.radius)
            {
                agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
                // transform.LookAt(lookAtTargetPosition);

                moved = true;
            }
        }
        else
        {
            //if attacking makes robot look at player but makes it not look weird
            var lookPos = target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            transform.rotation = rotation;
        }

        //forces the robot to move a little before attacked
        if (!moved)
            return;


        anim.SetBool("HoldingLightsaber", !shouldMove);

      
    }



    void UnSheathLightsaber()
    {
        movingLightsaberFromBackToHand = true;

        animatingLightsaber.SetActive(true);
        lightsaberOnBack.SetActive(false);

        anim.speed = 0.1f;
    }

    void SheathLightsaber()
    {
        movingLightsaberFromHandToBack = true;

        animatingLightsaber.SetActive(true);
        lightsaberInHand.SetActive(false);

        anim.speed = 0.1f;
    }

    void OnAnimatorMove()
    {
        if (anim == null)
            return;
        // Update position based on animation movement using navigation surface height
        Vector3 position = anim.rootPosition;
        position.y = agent.nextPosition.y;
        transform.position = position;

       // Debug.Log("OnAnimatorMove");
    }

    void SwordBecomesDangerous()
    {
        canDamagePlayer = true;
    }

    void SwordBecomesHarmless()
    {
        canDamagePlayer = false;
    }

    bool IsSwordDangerous()
    {
        return canDamagePlayer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (damageDelay > 0)
            return;

        if (!canDamagePlayer)
            return;

        if (!LightsaberHit(collision))
            return;

        Debug.Log("player damaged");

        damageDelay = delayBetweenPlayerDamage;
    }

    //checks if the robot's lightsaber hit the player
    private bool LightsaberHit(Collision collision)
    {
        foreach(ContactPoint contactPoint in collision.contacts)
        {
            if(contactPoint.thisCollider.CompareTag("EnemyLightsaber") && contactPoint.otherCollider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

}
