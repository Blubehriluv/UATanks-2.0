using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    [Header("Settings")]
    public float moveVolume;
    public float turnVolume;
    public float shootVolume;

    [Header("Volume")]
    public float volume;
    public float decayPerFrameDraw;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Sound decays
        if (volume > 0)
        {
            volume -= decayPerFrameDraw;
        }

    }
}
