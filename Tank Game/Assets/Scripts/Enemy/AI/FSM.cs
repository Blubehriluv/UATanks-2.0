using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    private TankData data;                                                   
    private TankMotor motor;                                                 
    private Transform tf;                                                    
    public enum AIState                                                      
    { Chase, ChaseAndFire, CheckForFlee, Flee, Rest, Wander };

    [SerializeField]
    private AIState aiState = AIState.Chase;
    private float stateEnterTime;
    public float aiSenseRadius;
    public int restingHealRate;
    public float visionDistance;
    public float visionAngle;
    public float hearingRadius;
    public float lastSeenLength;
    private float lastSeen;
    public bool showDebug;
    const float DEBUG_ANGLE_DISTANCE = 2.0f;
    const float DEGREES_TO_RADIANS = Mathf.PI / 180.0f;

    //Chase and Flee
    public Transform target;                                                 
    private int avoidanceStage = 0;                                          
    public float avoidanceTime = 2.0f;
    private float exitTime;                                                  
    public enum HuntMethod {Waypoint, Guard};
    public HuntMethod movementType;
    public float fleeDistance = 1.0f;
    public float percentHealthToFlee;
    public float fleeTime;

    //Waypoints
    [Header("Waypoint Settings")]
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    public float closeEnough = 1.0f;
    public enum LoopType {Stop, Loop, PingPong};
    public LoopType loopType;
    private bool isPatrolForward = true;

    public static float enemyHealth;
    public static float enemyFireRate;
    public static float enemyTurnSpeed;
    public static float enemyForwardSpeed;
    public TorpedoSpawn Shooter;
    public float enemyMaxHealth;

    private float distanceFromTarget;
    private Transform target1;
    private Transform target2;

    // Use this for initialization
    void Start()
    {
        //Get Components
        data = GetComponent<TankData>();
        motor = GetComponent<TankMotor>();
        tf = GetComponent<Transform>();

        //Set Target
        target = GameObject.Find("Player").GetComponent<Transform>();

        //Set Time
        lastSeen = Time.time - 1;

        //Set Waypoints
        waypoints = tf.parent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Checks to see how many players are present in the game.
        if (target1 == null && GameManager.instance.player != null) { target1 = GameManager.instance.player.transform; }
        if (target2 == null && GameManager.instance.player2 != null) { target2 = GameManager.instance.player2.transform; }
        if (target1 != null && target2 != null)
        {
            if ((target2.position - tf.position).magnitude < (target1.position - tf.position).magnitude) { target = target2; }
            else { target = target1; }
        }
        else if (target1 != null)
        {
            target = target1;
        }
        else if (target2 != null)
        {
            target = target2;
        }*/

        if (target != null) { distanceFromTarget = (target.position - tf.position).magnitude; }


        //Switch case statements to handle the states.
        switch (aiState)
        {
            case AIState.Chase:
                {

                    //If we can't see the player, wander around until we do
                    if (!CanSee(target.gameObject) && lastSeen < Time.time)
                    {
                        ChangeState(AIState.Wander);
                        break;
                    }

                    // Perform Behaviors
                    if (avoidanceStage != 0)
                    {
                        DoAvoidance();
                    }
                    else
                    {
                        DoChase();
                    }

                    // Check for Transitions
                    if (data.hp < data.maxHp * percentHealthToFlee)
                    {
                        ChangeState(AIState.CheckForFlee);
                    }
                    else if (distanceFromTarget <= aiSenseRadius)
                    {
                        ChangeState(AIState.ChaseAndFire);
                    }
                    break;
                }
            case AIState.ChaseAndFire:
                {
                    // Perform Behaviors
                    if (avoidanceStage != 0)
                    {
                        DoAvoidance();
                    }
                    else
                    {
                        DoChase();
                        Shooter.canShoot = true;
                    }
                    // Check for Transitions
                    if (data.hp < data.maxHp * percentHealthToFlee)
                    {
                        ChangeState(AIState.CheckForFlee);
                    }
                    else if (distanceFromTarget > aiSenseRadius)
                    {
                        ChangeState(AIState.Chase);
                    }
                    break;
                }
            case AIState.Flee:
                {
                    // Perform Behaviors
                    if (avoidanceStage != 0)
                    {
                        DoAvoidance();
                    }
                    else
                    {
                        DoFlee();
                    }

                    // Check for Transitions
                    if (Time.time >= stateEnterTime + fleeTime)
                    {
                        ChangeState(AIState.CheckForFlee);
                    }
                    break;
                }
            case AIState.CheckForFlee:
                {
                    // Check for Transitions
                    if (distanceFromTarget <= aiSenseRadius)
                    {
                        ChangeState(AIState.Flee);
                    }
                    else
                    {
                        ChangeState(AIState.Rest);
                    }
                    break;
                }
            case AIState.Rest:
                {
                    // Perform Behaviors
                    DoRest();

                    // Check for Transitions
                    if (distanceFromTarget <= aiSenseRadius)
                    {
                        ChangeState(AIState.Flee);
                    }
                    else if (enemyHealth >= enemyMaxHealth)
                    {
                        ChangeState(AIState.Chase);
                    }
                    break;
                }
            case AIState.Wander:
                {
                    switch (movementType)
                    {
                        case HuntMethod.Guard:
                            {
                                if (CanSee(target.gameObject))
                                {
                                    ChangeState(AIState.Chase);
                                }
                                else if (CanHear(target.gameObject))
                                {
                                    motor.RotateTowards(target.position, data.turnSpeed);
                                }
                                break;
                            }
                        case HuntMethod.Waypoint:
                            {
                                //If we can see them chase them
                                if (CanSee(target.gameObject))
                                {
                                    ChangeState(AIState.Chase);

                                }
                                //If we can hear them, turn towards them
                                else if (CanHear(target.gameObject))
                                {
                                    motor.RotateTowards(target.position, data.turnSpeed);
                                }
                                //If not, patrol waypoints
                                else
                                {
                                    if (!(motor.RotateTowards(waypoints[currentWaypoint].position, data.turnSpeed)))
                                    {
                                        //Move forward
                                        motor.Move(data.turnSpeed);
                                    }

                                    // If we are close to the waypoint,
                                    if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
                                    {
                                        //Switch Different Input Types
                                        switch (loopType)
                                        {
                                            case LoopType.Stop:

                                                // Advance to the next waypoint, if we are still in range
                                                if (currentWaypoint < waypoints.Length - 1)
                                                {
                                                    currentWaypoint++;
                                                }
                                                break;

                                            case LoopType.Loop:

                                                // Advance to the next waypoint, if we are still in range
                                                if (currentWaypoint < waypoints.Length - 1)
                                                {
                                                    currentWaypoint++;
                                                }
                                                else
                                                {
                                                    currentWaypoint = 0;
                                                }
                                                break;

                                            case LoopType.PingPong:

                                                if (isPatrolForward)
                                                {
                                                    // Advance to the next waypoint, if we are still in range
                                                    if (currentWaypoint < waypoints.Length - 1)
                                                    {
                                                        currentWaypoint++;
                                                    }
                                                    else
                                                    {
                                                        //Otherwise reverse direction and decrement our current waypoint
                                                        isPatrolForward = false;
                                                        currentWaypoint--;
                                                    }
                                                }
                                                else
                                                {
                                                    // Advance to the next waypoint, if we are still in range
                                                    if (currentWaypoint > 0)
                                                    {
                                                        currentWaypoint--;
                                                    }
                                                    else
                                                    {
                                                        //Otherwise reverse direction and increment our current waypoint
                                                        isPatrolForward = true;
                                                        currentWaypoint++;
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                            }
                    }
                    break;
                }
        }
    }

    //FSM
    public void ChangeState(AIState newState)
    {
        //Change our state
        aiState = newState;

        //Set the time we changed states
        stateEnterTime = Time.time;
    }

    //Chase
    void DoChase()
    {
        //Face target
        motor.RotateTowards(target.position, data.turnSpeed);
        //Check if we can move
        if (CanMove(data.forwardSpeed))
        {
            motor.Move(enemyForwardSpeed);
        }
        else
        {
            //Enter obstacle avoidance stage 1
            avoidanceStage = 1;
        }
    }

    //Flee
    void DoFlee()
    {
        //Variables
        Vector3 vectorAwayFromTarget = -1 * (target.position - tf.position);    //The vector away from our target
        vectorAwayFromTarget.Normalize();                                       //Normalized vector

        //Rotate away from target
        motor.RotateTowards(vectorAwayFromTarget * fleeDistance + tf.position, data.turnSpeed);

        //Check if we can move
        if (CanMove(data.forwardSpeed))
        {
            motor.Move(data.forwardSpeed);
        }
        else
        {
            //Enter obstacle avoidance stage 1
            avoidanceStage = 1;
        }
    }

    //Obstacle Avoidance
    void DoAvoidance()
    {
        if (avoidanceStage == 1)
        {
            //Rotate left
            motor.Rotate(-1 * data.turnSpeed);

            //If I can now move forward, move to stage 2
            if (CanMove(data.forwardSpeed))
            {
                avoidanceStage = 2;

                //Set the number of seconds we will stay in stage 2
                exitTime = avoidanceTime;
            }

            //If not, try again
        }
        else if (avoidanceStage == 2)
        {
            //If we can move forward, do so
            if (CanMove(data.forwardSpeed))
            {
                //Subtract from our timer and move
                exitTime -= Time.deltaTime;
                motor.Move(data.forwardSpeed);

                //If we have moved long enough, return to chase mode
                if (exitTime <= 0)
                {
                    avoidanceStage = 0;
                }
            }
            else
            {
                //If not, repeat avoidance stages
                avoidanceStage = 1;
            }
        }
    }

    //Check for Obastacles
    bool CanMove(float speed)
    {
        //If our raycast hit something
        RaycastHit hit;
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            //If it isn't the player
            if (!hit.collider.CompareTag("Player"))
            {
                //There's an obstacle
                return false;
            }
        }
        //If not, we can move
        return true;
    }

    //Rest
    public void DoRest()
    {
        //Increase our health per second, but don't over heal
        enemyHealth += Mathf.Clamp(restingHealRate * Time.deltaTime, 0, data.maxHp);
    }

    public bool CanHear(GameObject target)
    {
        /*
        //Check for a noise maker
        Noisemaker targetNoiseMaker = target.GetComponent<Noisemaker>();
        if (targetNoiseMaker == null)
        {
            return false;
        }

        //Check if we can hear them
        Transform targetTf = target.GetComponent<Transform>();
        if (Vector3.Distance(targetTf.position, tf.position) <= targetNoiseMaker.volume + hearingRadius)
        {
            return true;
        }

        //If we can't, return false*/
        return false;
    }

    public bool CanSee(GameObject target)
    {
        //Check for a collider
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider == null)
        {
            return false;
        }

        //Find vector to target
        Vector3 vectorToTarget = target.GetComponent<Transform>().position - tf.position;
        vectorToTarget.Normalize();

        //Debug
        DrawDebugAngle(showDebug);

        //If they're outside our field of view, we can't see them
        if (Vector3.Angle(vectorToTarget, tf.forward) >= visionAngle)
        {
            return false;
        }

        // Make sure nothing is blocking our view
        Ray ray = new Ray(tf.position, vectorToTarget);
        RaycastHit hitInfo;

        //If we hit nothing, return false
        if (!Physics.Raycast(ray, out hitInfo, visionDistance))
        {
            return false;
        }

        if (showDebug)
        {
            Debug.DrawLine(tf.position, tf.position + vectorToTarget * visionDistance, Color.red);
            Debug.Log("Hit: " + hitInfo.collider + "\nTarget: " + targetCollider);
        }

        // if our raycast hits nothing, we can't see them
        if (!Physics.Raycast(tf.position, vectorToTarget, visionDistance))
        {
            return false;
        }

        // If our raycast hits them first, then we can see them
        if (hitInfo.collider == targetCollider)
        {
            lastSeen = Time.time + lastSeenLength;
            return true;
        }

        // otherwise, we hit something else
        return false;
    }

    public void DrawDebugAngle(bool show)
    {
        if (show)
        {
            Vector3 perpendicularDirection = new Vector3(-tf.forward.y, tf.forward.x);
            float oppositeSideLength = Mathf.Tan(visionAngle * 0.5f * DEGREES_TO_RADIANS) * DEBUG_ANGLE_DISTANCE;
            Debug.DrawLine(tf.position, tf.position + DEBUG_ANGLE_DISTANCE * tf.forward + perpendicularDirection * oppositeSideLength, Color.green);
            Debug.DrawLine(tf.position, tf.position + DEBUG_ANGLE_DISTANCE * tf.forward - perpendicularDirection * oppositeSideLength, Color.green);
        }
    }
}