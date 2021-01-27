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

    public GameObject player;
    public SpawnPoint sp;
    //public GameObject enemy;
    public Text points;
    private string pointHolder;

    public float setShellDamage;

    public float setEnemyShellDamage;

    public float fireRate;
    public float enemyFireRate;
    public float torpedoThrust;
    public float enemyTorpedoThrust;

    public int hitsTaken = 0;
    public int hitsGiven = 0;
    public int tanksDestroyed = 0;
    public int selfDestroyed = 0;

    public int HitPointValue;
    public int DestroyPointValue;
    private int TotalPoints;

    
    public float bulletTimeout;
    public float enemyBulletTimeout;

    public int totalPickups;
    public int currentPickups;

    public float musicVolume;


    
    /*
    public enum PlayerTarget { player, player2};
    public PlayerTarget playerTarget;
    public bool isMultiplayer;
    public GameObject[] targets;*/



    // Start is called before the first frame update
    void Start()
    {
        //SelectTargets();
        player = GameObject.FindGameObjectWithTag("Player");

        TorpedoHit.shellDamage = setShellDamage;
        TorpedoHit.enemyShellDamage = setEnemyShellDamage;

        TorpedoSpawn.thrust = torpedoThrust;
        TorpedoSpawn.fireRate = fireRate;
        TorpedoSpawn.torpedoTimeout = bulletTimeout;

        //EnemyController.enemyThrust = enemyTorpedoThrust;
        //EnemyController.enemyBulletTimeout = enemyBulletTimeout;


        musicVolume = Volume.AudioVolume;

        
    }

    void Update()
    {
        hitsTaken = TorpedoHit.hitsTaken;
        TotalPoints = (HitPointValue * hitsTaken);
        musicVolume = Volume.AudioVolume;
        points.text = TotalPoints.ToString();
    }
}
