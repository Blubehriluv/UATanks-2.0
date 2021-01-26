using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMotor : MonoBehaviour
{
    private CharacterController characterController;
    public Transform tf;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float speed)
    {
        Vector3 hold = tf.forward.normalized;
        Vector3 speedVector = hold * speed * Time.deltaTime;
        characterController.Move(speedVector);
    }

    public void Rotate(float speed)
    {
        Vector3 rotateVector = Vector3.up * (speed * Time.deltaTime);
        tf.Rotate(rotateVector, Space.Self);
    }

    public bool RotateTowards(Vector3 target, float speed)
    {
        Vector3 vectorToTarget = target - tf.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget);


        if (targetRotation == tf.rotation)
        {
            return false;
        }

        tf.rotation = Quaternion.RotateTowards(tf.rotation, targetRotation, speed * Time.deltaTime);

        // We rotated, so return true
        return true;
    }
}
