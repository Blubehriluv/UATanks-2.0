using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAI : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointIndex;
    public int speed;
    private int speedholder;
    public int moveSpeed;
    private float dist;
    public Transform enemyTF;
    public Rigidbody enemyRB;
    public int avoidanceStage = 0;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        speedholder = speed;
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = enemyTF.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(enemyTF.transform.position, fwd * 50, Color.green);

        //Do avoidance
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 3f)
        {
            IncreaseIndex();
        }
        Patrol();
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            speed = 0;
            transform.LookAt(player.position);
            Debug.Log("i see the player");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            speed = speedholder;
            IncreaseIndex();
        }
    }
}
