using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAI : MonoBehaviour
{
    public Transform[] waypoints;
    public enum PatrolScheme {stop, pingPong, loop}
    public PatrolScheme patrolScheme;
    public float closeEnough = 1.0f;
    public TankMotor motor;
    public static float enemyForwardSpeed;
    public static float enemyTurnSpeed;
    private int currentWaypoint = 0;
    private Transform tf;
    private bool isPatrolForward = true;

    // Start is called before the first frame update
    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (motor.RotateTowards(waypoints[currentWaypoint].position, enemyTurnSpeed))
        {
            // Do nothing!
        }
        else
        {
            // Move forward
            motor.Move(enemyForwardSpeed);
        }

        // If we are close to the waypoint,
        if (Vector3.SqrMagnitude(waypoints[currentWaypoint].position - tf.position) < (closeEnough * closeEnough))
        {

            if (patrolScheme == PatrolScheme.stop)
            {
                // Advance to the next waypoint, if we are still in range
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }
            }
            else if (patrolScheme == PatrolScheme.loop)
            {
                // Advance to the next waypoint, if we are still in range
                if (currentWaypoint < waypoints.Length - 1)
                {
                    currentWaypoint++;
                }
                else
                {
                    currentWaypoint = 0;
                }
            }
            else if (patrolScheme == PatrolScheme.pingPong)
            {
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
            }
        }
    }
}
