using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip music;
    public AudioSource audioSource;

    void Start()
    {
        audioSource.clip = music;
        audioSource.Play();
    }
}
