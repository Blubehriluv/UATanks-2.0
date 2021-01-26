using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

/// <summary>
/// New boi
/// </summary>
public class TankData : MonoBehaviour
{
    //Variables
    public float forwardSpeed = 3;
    public float reverseSpeed = 1.5f;
    public float turnSpeed = 180;
    public float shellDamage;
    public float fireRate;
    public float maxHp;
    public float hp;
    public int pointValue;
    public int score;
    public float fireRateModifier = 1;
    public int maxLives;
    public int healthOrb;
    public int maxOrb;

    void Start()
    {
        hp = maxHp;
        fireRateModifier = Mathf.Max(fireRateModifier, 1);
    }
}
