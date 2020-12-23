using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerTF;
    public Rigidbody playerRB;
    public static float playerForwardSpeed;
    public static float playerBackSpeed;
    public static float playerTurnSpeed;
    


    // Start is called before the first frame update
    void Start()
    {
        playerTF = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 wantedPosition =
                playerTF.position + (playerTF.forward * playerForwardSpeed * Time.deltaTime);
            playerRB.MovePosition(wantedPosition);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 wantedPosition =
                playerTF.position - (playerTF.forward * playerBackSpeed * Time.deltaTime);
            playerRB.MovePosition(wantedPosition);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Quaternion wantedRotation = playerTF.rotation * Quaternion.Euler(Vector3.up * playerTurnSpeed);
            playerRB.MoveRotation(wantedRotation);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Quaternion wantedRotation = playerTF.rotation * Quaternion.Euler(Vector3.down * playerTurnSpeed);
            playerRB.MoveRotation(wantedRotation);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "torpedo")
        {
            Debug.Log("ouch");
        }
        if (collision.collider.gameObject.tag == "tree")
        {
            Debug.Log("who put dat tree here");
        }
        if (collision.collider.gameObject.tag != "Enemy" && collision.collider.gameObject.tag != "tree")
        {
            //Debug.Log("something got hit");
            //Destroy(gameObject);
        }
    }
}