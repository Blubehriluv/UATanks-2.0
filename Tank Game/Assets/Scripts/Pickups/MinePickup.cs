using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePickup : MonoBehaviour
{
    public GameObject mine;
    public static int totalPickups;
    public static int currentPickups;
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

    private void OnTriggerEnter(Collider other)
    {
        {
            audioData.Play(0);
            currentPickups++;
            BeginCode();
        }
        
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
        yield return new WaitForSeconds(5);
        Debug.Log("Done waiting");
        Respawn();
    }
}