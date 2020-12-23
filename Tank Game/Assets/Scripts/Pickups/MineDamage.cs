using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDamage : MonoBehaviour
{
    private int mineDamage = 250;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnColliderEnter(Collision other)
    {
        if (other.gameObject.tag != "Player")
        {
            other.collider.gameObject.GetComponent<EnemyData>().enemyHealth = other.collider.gameObject.GetComponent<EnemyData>().enemyHealth - mineDamage;
            Debug.Log("Hit Enemy");
            Destroy(gameObject);
        }
    }
}
