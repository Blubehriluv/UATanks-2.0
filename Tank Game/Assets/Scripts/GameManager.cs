using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public List<Sprite> blueSprites; //full, eighty, sixty, forty, twenty, zero;

    public List<Sprite> redSprites;// fullE, eightyE, sixtyE, fortyE, twentyE, zeroE;

    //public SpawnPoint sp;
    public GameObject enemy;
    public Text points;
    private string pointHolder;

    public float playerHealth;
    public float setShellDamage;
    public float fireRate;
    public float playerForwardSpeed;
    public float playerTurnSpeed;
    public float playerBackSpeed;
    
    public float enemyHealth;
    public float setEnemyShellDamage;


    public float torpedoThrust;
    public float enemyTorpedoThrust;

    private int hitsTaken = 0;
    private int hitsGiven = 0;
    public int tanksDestroyed = 0;
    public int selfDestroyed = 0;

    public int HitPointValue;
    public int DestroyPointValue;
    private int TotalPoints;

    
    public float bulletTimeout;
    //public float enemyBulletTimeout;
    




    // Start is called before the first frame update
    void Start()
    {
        //TankData.playerHealth = playerHealth;
        enemy.GetComponent<EnemyData>().enemyHealth = enemyHealth;
        enemy.GetComponent<EnemyData>().enemyCurrentHealth = enemyHealth;

        TorpedoHit.shellDamage = setShellDamage;
        TorpedoHit.enemyShellDamage = setEnemyShellDamage;

        PlayerMovement.playerForwardSpeed = playerForwardSpeed;
        PlayerMovement.playerTurnSpeed = playerTurnSpeed;
        PlayerMovement.playerBackSpeed = playerBackSpeed;

        TorpedoSpawn.thrust = torpedoThrust;
        TorpedoSpawn.fireRate = fireRate;
        TorpedoSpawn.bulletTimeout = bulletTimeout;

        //EnemyFire.enemyThrust = enemyTorpedoThrust;
        //EnemyFire.enemyBulletTimeout = enemyBulletTimeout;

        


        
    }

    // Update is called once per frame
    void Update()
    {
        TotalPoints = (HitPointValue * TorpedoHit.hitsGiven) + (DestroyPointValue * tanksDestroyed);

        
        points.text = TotalPoints.ToString();
    }
}
