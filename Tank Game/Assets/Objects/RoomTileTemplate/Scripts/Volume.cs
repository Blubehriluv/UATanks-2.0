using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume : MonoBehaviour
{
    public AudioSource Music;

    public static float AudioVolume = 1f;

    void Start()
    {
        Music = GetComponent<AudioSource>();
    }

    void Update()
    {
        Music.volume = AudioVolume;
    }

    public void SetVolume(float vol)
    {
        AudioVolume = vol;
    }
}
