using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TorpedoHit : MonoBehaviour
{
    public static float shellDamage;
    public static float enemyShellDamage;
    public static int hitsGiven;
    public static int hitsTaken;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            collision.collider.gameObject.GetComponent<EnemyData>().enemyHealth = collision.collider.gameObject.GetComponent<EnemyData>().enemyHealth - shellDamage;
            Debug.Log("Hit Enemy");
            hitsGiven++;
            Destroy(gameObject);
        }/*
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<TankData>().currentHealth = collision.collider.gameObject.GetComponent<TankData>().currentHealth - enemyShellDamage;
            Debug.Log("Hit Enemy");
            hitsTaken++;
            Destroy(gameObject);
        }*/
    }
}
