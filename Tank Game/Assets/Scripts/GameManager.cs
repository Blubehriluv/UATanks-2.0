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

    public SpawnPoint sp;
    //public GameObject enemy;
    public Text points;
    private string pointHolder;

    public float playerHealth;
    public float setShellDamage;
    public float fireRate;
    public float playerForwardSpeed;
    public float playerTurnSpeed;
    public float playerBackSpeed;

    public float enemyForwardSpeed;
    public float enemyTurnSpeed;
    public float enemyBackSpeed;

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
    public float enemyBulletTimeout;

    public int totalPickups;
    public int currentPickups;
    
    /*
    public enum PlayerTarget { player, player2};
    public PlayerTarget playerTarget;
    public bool isMultiplayer;
    public GameObject[] targets;*/



    // Start is called before the first frame update
    void Start()
    {
        //SelectTargets();


        TorpedoHit.shellDamage = setShellDamage;
        TorpedoHit.enemyShellDamage = setEnemyShellDamage;

        InputManager.playerForwardSpeed = playerForwardSpeed;
        InputManager.playerTurnSpeed = playerTurnSpeed;
        InputManager.playerBackSpeed = playerBackSpeed;

        SampleAI.enemyForwardSpeed = enemyForwardSpeed;
        SampleAI.enemyTurnSpeed = enemyTurnSpeed;

        Avoidance.enemyForwardSpeed = enemyForwardSpeed;
        Avoidance.enemyTurnSpeed = enemyTurnSpeed;

        Courage.enemyForwardSpeed = enemyForwardSpeed;
        Courage.enemyTurnSpeed = enemyTurnSpeed;

        FSM.enemyHealth = enemyHealth;

        TorpedoSpawn.thrust = torpedoThrust;
        TorpedoSpawn.fireRate = fireRate;
        TorpedoSpawn.torpedoTimeout = bulletTimeout;

        //EnemyController.enemyThrust = enemyTorpedoThrust;
        //EnemyController.enemyBulletTimeout = enemyBulletTimeout;

        


        
    }

    void Update()
    {
        TotalPoints = (HitPointValue * TorpedoHit.hitsGiven) + (DestroyPointValue * tanksDestroyed);
                
        //points.text = TotalPoints.ToString();
    }
}
