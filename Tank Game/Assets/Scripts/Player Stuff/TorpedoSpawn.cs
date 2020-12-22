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
    public static float bulletTimeout;
    public static float fireRate;
    private bool canShoot = true;

    void Start()
    {
        spawnPoint = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (canShoot == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject BulletHold = Instantiate(torpedo, spawnPoint.position, spawnPoint.rotation) as GameObject;
                Rigidbody temporary_RB = BulletHold.GetComponent<Rigidbody>();
                temporary_RB.AddForce(transform.forward * (thrust * 2));
                Destroy(BulletHold, bulletTimeout);
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

    IEnumerator WaitAMinute()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(fireRate);
        Debug.Log("Done waiting");
        canShoot = true;
    }
}