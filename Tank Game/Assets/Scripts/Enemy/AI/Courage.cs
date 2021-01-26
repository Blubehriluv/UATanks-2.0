using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courage : MonoBehaviour
{
    public enum AttackMode {Chase, Flee};
    public AttackMode attackMode;
    public float fleeDistance = 1.0f;
    public Transform targetTf;
    public static float enemyForwardSpeed;
    public static float enemyTurnSpeed;
    public TankMotor motor;
    public Transform tf; 
    // Start is called before the first frame update
    void Start()
    { 
        tf = gameObject.GetComponent<Transform>();
        targetTf = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (attackMode == AttackMode.Chase)
        {
            motor.RotateTowards(targetTf.position, enemyForwardSpeed);
            motor.Move(enemyForwardSpeed);
        }
        if (attackMode == AttackMode.Flee)
        {
            Vector3 vectorAwayFromTarget = (targetTf.position - tf.position) * -1;

            vectorAwayFromTarget.Normalize();
            vectorAwayFromTarget *= fleeDistance;

            Vector3 fleePosition = vectorAwayFromTarget + tf.position;
            motor.RotateTowards(fleePosition, enemyTurnSpeed);
            motor.Move(enemyForwardSpeed);
        }
    }
}
