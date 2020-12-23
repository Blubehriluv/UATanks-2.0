using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class EnemyData : MonoBehaviour
{

    public float enemyHealth;
    public float enemyCurrentHealth;
    private float dataHealth;
    public Image healthbar;

    public GameObject self;
    public int tanksDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        self = gameObject;
        healthbar = GetComponentInChildren<Image>();
        enemyCurrentHealth = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        if (dataHealth > 81)
        {
            healthbar.sprite = GameManager.Instance.redSprites[0];
        }

        if (dataHealth >= 61 && dataHealth <= 80)
        {
            healthbar.sprite = GameManager.Instance.redSprites[1];
        }

        if (dataHealth >= 41 && dataHealth <= 60)
        {
            healthbar.sprite = GameManager.Instance.redSprites[2];
        }

        if (dataHealth >= 21 && dataHealth <= 40)
        {
            healthbar.sprite = GameManager.Instance.redSprites[3];
        }

        if (dataHealth >= 1 && dataHealth <= 20)
        {
            healthbar.sprite = GameManager.Instance.redSprites[4];
        }

        if (dataHealth <= 0)
        {
            Debug.Log("Enemy died.");
            healthbar.sprite = GameManager.Instance.redSprites[5];
            tanksDestroyed++;
            GameManager.Instance.tanksDestroyed += 1;
            Destroy(self);
        }
    }

    void CheckHealth()
    {
        dataHealth = ((enemyHealth * 100) / enemyCurrentHealth);
        //Debug.Log(dataHealth);
    }
}