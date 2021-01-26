using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Powerup
{
    public float speedModifier;
    public float healthModifier;
    public float maxHealthModifier;
    public float fireRateModifier;
    public bool isPermanent;
    public float duration;

    public static float playerForwardSpeed;
    public static float playerHealth;
    public float maxPlayerHealth;
    public static float fireRate;

    public List<Powerup> powerups;

    // Start is called before the first frame update
    void Start()
    {
        maxPlayerHealth = playerHealth;
        powerups = new List<Powerup>();
        //speedModifier = ((playerForwardSpeed * 200) / 100);
    }

    // Update is called once per frame
    void Update()
    {
        // Create an List to hold our expired powerups
        List<Powerup> expiredPowerups = new List<Powerup>();

        // Loop through all the powers in the List
        foreach (Powerup power in powerups)
        {
            // Subtract from the timer
            power.duration -= Time.deltaTime;

            // Assemble a list of expired powerups
            if (power.duration <= 0)
            {
                expiredPowerups.Add(power);
            }
        }
        // Now that we've looked at every powerup in our list, use our list of expired powerups to remove the expired ones.
        foreach (Powerup power in expiredPowerups)
        {
            power.OnDeactivate();
            powerups.Remove(power);
        }
        // Since our expiredPowerups is local, it will *poof* into nothing when this function ends,
        ///    but let's clear it to learn how to empty an List
        expiredPowerups.Clear();
    }

    public void OnActivate()
    {
        GameManager.Instance.playerForwardSpeed += speedModifier;
        playerHealth += healthModifier;
        maxPlayerHealth += maxHealthModifier;
        fireRate += fireRateModifier;
    }

    public void OnDeactivate()
    {
        playerForwardSpeed -= speedModifier;
        playerHealth -= healthModifier;
        maxPlayerHealth -= maxHealthModifier;
       fireRate -= fireRateModifier;
    }

    public void Add(Powerup powerup)
    {
        // Run the OnActivate function of the powerup
        powerup.OnActivate();

        // Only add the permanent ones to the list
        if (!powerup.isPermanent)
        {
            powerups.Add(powerup);
        }
    }
}
