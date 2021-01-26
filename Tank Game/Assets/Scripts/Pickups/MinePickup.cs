using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePickup : MonoBehaviour
{
    public GameObject mine;
    public static int totalPickups;
    public static int currentPickups = 0;
    private bool mineCooldown;
    private bool healthCooldown;
    private bool incCooldown;
    private int waitTime;
    public int mineWaitTime = 10;
    public int healthWaitTime = 25;
    public int healthMaxWaitTime = 60;
    public TankData data;
    AudioSource audioData;
    public enum Pickups { mine, heal, hpIncrease }
    public Pickups pickupType;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        data = GameObject.Find("Player").GetComponent<TankData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        { 
            //audioData.Play(0);
            if (pickupType == Pickups.mine)
            {
                waitTime = mineWaitTime;
                currentPickups++;
            }
            if (pickupType == Pickups.heal)
            {
                waitTime = healthWaitTime;
                if (healthCooldown != true)
                {
                    if (data.hp < data.maxHp)
                    {
                        Debug.Log("Health orb acquired");
                        DrinkHealth();
                        healthCooldown = false;
                    }
                    else
                    {
                        Debug.Log("I'm too healthy uwu");
                        return;
                    }
                }
                else
                {
                    Debug.Log("Stop trying to take more than one!");
                    healthCooldown = true;
                    return;
                }

            }
            if (pickupType == Pickups.hpIncrease)
            {
                waitTime = healthMaxWaitTime;
                data.maxHp += data.maxOrb;
            }
            BeginCode();
        }
        
    }

    void DrinkHealth()
    {
        data.hp += data.healthOrb;
    }

    void Pickup()
    {
        mine.SetActive(false);
    }

    void Respawn()
    {
        mine.SetActive(true);
        Debug.Log("RESPAWNING!");
    }

    void BeginCode()
    {
        Pickup();
        StartCoroutine(nameof(WaitAMinute));
    }

    IEnumerator WaitAMinute()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Done waiting");
        Respawn();
    }
}