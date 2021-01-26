using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public Transform target;
    public TankMotor motor;
    private Transform tf;
    private int avoidanceStage = 0;
    public float avoidanceTime = 2.0f;
    private float exitTime;
    public static float enemyForwardSpeed;
    public static float enemyTurnSpeed;
    
    public enum AttackMode { Chase };
    public AttackMode attackMode;
    public enum AIState { Chase, ChaseAndFire, CheckForFlee, Flee, Rest };
    public AIState aiState = AIState.Chase;
    public float stateEnterTime;
    public float aiSenseRadius;
    public float restingHealRate; // in hp/second 

    void Start()
    {
        tf = gameObject.GetComponent<Transform>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( aiState == AIState.Chase ) {
       // Perform Behaviors
        if (avoidanceStage != 0) {
            Avoid();
        } else {
            Follow();
        }

           // Check for Transitions
           if (data.health < data.maxHealth * 0.5f) {
              ChangeState(AIState.CheckForFlee);
           } else if (Vector3.Distance (target.position, tf.position) <= aiSenseRadius) {
              ChangeState(AIState.ChaseAndFire);
           }
        } else if ( aiState == AIState.ChaseAndFire ) {
           // Perform Behaviors
           if (avoidanceStage != 0) {
              Avoid();
           } else {
              Follow();

              // Limit our firing rate, so we can only shoot if enough time has passed
              if (Time.time > lastShootTime + data.fireRate) {
                 shooter.Shoot(); // Note: this assumes we have a "shooter" component with a Shoot() function.
                 lastShootTime = Time.time;
              }
           }
           // Check for Transitions
           if (data.health < data.maxHealth * 0.5f) {
              ChangeState(AIState.CheckForFlee);
           } else if (Vector3.Distance (target.position, tf.position) > aiSenseRadius) {
              ChangeState(AIState.Chase);
           }
        } else if ( aiState == AIState.Flee ) {
           // Perform Behaviors
           if (avoidanceStage != 0) {
              Avoid();
           } else {
              DoFlee();
           }

           // Check for Transitions
           if (Time.time >= stateEnterTime + 30) {
              ChangeState(AIState.CheckForFlee);
           }
        } else if ( aiState == AIState.CheckForFlee ) {
           // Perform Behaviors
           CheckForFlee();

           // Check for Transitions
           if (Vector3.Distance (target.position, tf.position) <= aiSenseRadius) {
              ChangeState(AIState.Flee);
           } else {
              ChangeState(AIState.Rest);
           }
        } else if ( aiState == AIState.Rest ) {
           // Perform Behaviors
           DoRest();

           // Check for Transitions
           if (Vector3.Distance (target.position, tf.position) <= aiSenseRadius) {
              ChangeState(AIState.Flee);
           } else if (data.health >= data.maxHealth) {
              ChangeState(AIState.Chase);
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


    public void DoRest()
    {

        // Increase our health. Remember that our increase is "per second"!
        data.health += restingHealRate * Time.deltaTime;

        // But never go over our max health
        data.health = Mathf.Min(data.health, data.maxHealth);
    }

    public void ChangeState(AIState newState)
    {

        // Change our state
        aiState = newState;

        // save the time we changed states
        stateEnterTime = Time.time;
    }
}
