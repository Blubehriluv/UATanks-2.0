using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public GameObject mine;
    public Transform spawnPoint;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (MinePickup.currentPickups != 0)
            {
                audioData.Play(0);
                GameObject mineHold = Instantiate(mine, spawnPoint.position, spawnPoint.rotation) as GameObject;
                Rigidbody temporary_RB = mineHold.GetComponent<Rigidbody>();
                Destroy(mineHold, 30);
                MinePickup.currentPickups--;
            }
            if (MinePickup.currentPickups == 0)
            {

            }
        }
    }
}
