﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoidance : MonoBehaviour
{
    public Transform target;
    public TankMotor motor;
    private Transform tf;
    private int avoidanceStage = 0;
    public float avoidanceTime = 2.0f;
    private float exitTime;
    public static float enemyForwardSpeed;
    public static float enemyTurnSpeed;
    public enum AttackMode {Chase};
    public AttackMode attackMode;

    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackMode == AttackMode.Chase)
        {
            if (avoidanceStage != 0)
            {
                Avoid();
            }
            else
            {
                Follow();
            }
        }

    }

    public bool CanMove(float speed)
    {
        // Fire the laser in the direction we want
        // If the raycast hit
        RaycastHit hit;
        if (Physics.Raycast(tf.position, tf.forward, out hit, speed))
        {
            return false;
        }

        // otherwise, true
        return true;
    }

    public void Avoid()
    {
        if (avoidanceStage == 1)
        {
            Debug.Log("Trying to avoid something");
            // Tank Rotates Left
            motor.Rotate(-1 * enemyTurnSpeed);

            // Tank can move -> Stage 2
            if (CanMove(enemyForwardSpeed))
            {
                avoidanceStage = 2;

                // How long in Stage 2
                exitTime = avoidanceTime;
            }

            // Otherwise, we'll do this again next turn!
        }
        else if (avoidanceStage == 2)
        {
            // Move forward if you can
            if (CanMove(enemyForwardSpeed))
            {
                // Subtract from our timer and move
                exitTime -= Time.deltaTime;
                motor.Move(enemyTurnSpeed);

                // If we have moved long enough, return to chase mode
                if (exitTime <= 0)
                {
                    avoidanceStage = 0;
                }
            }
            else
            {
                // Otherwise, we can't move forward, so back to stage 1
                avoidanceStage = 1;
            }
        }
    }

    public void Follow()
    {
        Debug.Log("Following!");
        motor.RotateTowards(target.position, enemyTurnSpeed);
        if (CanMove(enemyForwardSpeed))
        {
            Debug.Log("I can follow something!");
            motor.Move(enemyForwardSpeed);
        }
        else
        {
            // Enter obstacle avoidance stage 1
            avoidanceStage = 1;
        }
    }
}
