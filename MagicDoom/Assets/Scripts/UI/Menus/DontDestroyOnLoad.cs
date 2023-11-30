using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DontDestroyOnLoad : MonoBehaviour
{
    public AudioSource audioSource;
    public static DontDestroyOnLoad instance;


    private void Start()
    {
        if (instance == null)
            instance = new DontDestroyOnLoad();

        DontDestroyOnLoad(audioSource);
    }
}
