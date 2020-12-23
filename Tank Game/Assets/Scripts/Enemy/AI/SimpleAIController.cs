using UnityEngine;
using System.Collections;

public class SimpleAIController : MonoBehaviour
{
    public Transform[] waypoints;
    private int waypointIndex;
    public int speed;
    private int speedholder;
    public int moveSpeed;
    private float dist;
    //private bool ISEEU;
    //private bool canSee;
    public Transform enemyTF;
    public Rigidbody enemyRB;
    public int avoidanceStage = 0;
    public Transform player;
    //private float exitTime = 4.0f;
    //private int avoidanceTime = 3;


    private void Start()
    {
        speedholder = speed;
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    private void Update()
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

    /*
    void Avoid()
    {
        if (avoidanceStage == 1)
        {
            // Rotate left
            Quaternion wantedRotation = enemyTF.rotation * Quaternion.Euler(Vector3.up * moveSpeed); ;
            enemyRB.MoveRotation(wantedRotation);
            Debug.Log("made it past the move script");
            // If I can now move forward, move to stage 2!
            if (CanMove(moveSpeed))
            {
                avoidanceStage = 2;
                Debug.Log("went into stage 2");
                // Set the number of seconds we will stay in Stage2
                exitTime = avoidanceTime;
            }// Otherwise, we'll do this again next turn!            
        }
        else if (avoidanceStage == 2)
        {
            // if we can move forward, do so
            if (CanMove(moveSpeed))
            {
                // Subtract from our timer and move
                exitTime -= Time.deltaTime;
                transform.Translate(Vector3.forward * moveSpeed);

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

    public bool CanMove(float moveSpeed)
    {
        RaycastHit hit;
        if (Physics.Raycast (enemyTF.position, enemyTF.forward * 50, out hit, moveSpeed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                Debug.Log("RAYCAST HIT " + hit.collider.gameObject.name);

                return false;
            }
            Debug.Log("Something else.." + hit.collider.gameObject.name);
        }
        return true;
    }*/

    void Patrol()
    {
       
         transform.Translate(Vector3.forward * speed * Time.deltaTime);
  
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length)
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