using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TorpedoHit : MonoBehaviour
{
    public static float shellDamage;
    public static float enemyShellDamage;
    public static int hitsTaken;
    private TankData holder;
    public GameManager pointer;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TankData>())
        {
            holder = collision.collider.gameObject.GetComponent<TankData>();
            holder.hp -= holder.shellDamage;
            //collision.collider.gameObject.GetComponent<TankData>().hp -= collision.collider.gameObject.GetComponent<TankData>().shellDamage;
            Debug.Log("Hit Enemy");
            hitsTaken++;
        }
        audioData.Play(0);
        Destroy(gameObject);

    }
}
