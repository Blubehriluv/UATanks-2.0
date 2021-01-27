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
            //If player presses space, try to shoot
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        //If we can shoot, then shoot
        if (canShoot == true)
        {
            //If we have audio data, play audio
            if (audioData)
            {
                audioData.Play(0);
            }

            //Creates a game object specifically for the fired torpedo.
            GameObject BulletHold = Instantiate(torpedo, spawnPoint.position, spawnPoint.rotation) as GameObject;

            Rigidbody temporary_RB = BulletHold.GetComponent<Rigidbody>();
            //Propels the torpedo forward with speed
            temporary_RB.AddForce(transform.forward * (thrust * 2));

            //Set canShoot to false
            canShoot = false;

            //Destroys torpedo after a certain time.
            Destroy(BulletHold, torpedoTimeout);

            //Manage shoot cool down
            ShootCoolDown();
        }

        //otherwise, we can't shoot. No else-block required.
    }

    void ShootCoolDown()
    {
        StartCoroutine(nameof(WaitForCooldown));
    }

    //Waiter for fire rate.
    IEnumerator WaitForCooldown()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(fireRate);
        Debug.Log("Done waiting");
        canShoot = true;
    }
}