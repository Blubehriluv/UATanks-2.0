using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TorpedoSpawn : MonoBehaviour
{
    public GameObject torpedo;
    public Transform spawnPoint;
    public static float thrust;
    public static float torpedoTimeout;
    public static float fireRate;
    public bool canShoot = true;
    public bool isPlayer;
    AudioSource audioData;

    void Start()
    {
        spawnPoint = gameObject.GetComponent<Transform>();
        audioData = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isPlayer == true)
        {
            //If the fire rate timer allows, the player can shoot again
            if (canShoot == true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    audioData.Play(0);
                    //Creates a game object specifically for the fired torpedo.
                    GameObject BulletHold = Instantiate(torpedo, spawnPoint.position, spawnPoint.rotation) as GameObject;
                    Rigidbody temporary_RB = BulletHold.GetComponent<Rigidbody>();
                    //Propels the torpedo forward with speed
                    temporary_RB.AddForce(transform.forward * (thrust * 2));
                    //Destroys torpedo after a certain time.
                    Destroy(BulletHold, torpedoTimeout);
                    canShoot = false;
                    BeginCode();
                    Debug.Log("shooting!");
                }
            }
        }
        else
        {
            if (canShoot == true)
            {
                Debug.Log("Do nothing");
            }
            else
            {
                audioData.Play(0);
                //Creates a game object specifically for the fired torpedo.
                GameObject BulletHold = Instantiate(torpedo, spawnPoint.position, spawnPoint.rotation) as GameObject;
                Rigidbody temporary_RB = BulletHold.GetComponent<Rigidbody>();
                //Propels the torpedo forward with speed
                temporary_RB.AddForce(transform.forward * (thrust * 2));
                //Destroys torpedo after a certain time.
                Destroy(BulletHold, torpedoTimeout);
                canShoot = false;
                BeginCode();
                Debug.Log("shooting!");
            }
        }
    }

    void BeginCode()
    {
        StartCoroutine(nameof(WaitAMinute));
    }

    //Waiter for fire rate.
    IEnumerator WaitAMinute()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(fireRate);
        Debug.Log("Done waiting");
        canShoot = true;
    }
}