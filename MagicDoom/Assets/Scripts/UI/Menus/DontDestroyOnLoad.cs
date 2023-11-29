using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DontDestroyOnLoad : MonoBehaviour
{
    public GameObject[] gameObjects;
    public static DontDestroyOnLoad instance;


    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;

        foreach (var gameObject in gameObjects)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void RemoveFromDontDestroyOnLoad()
    {
        foreach (var gameObject in gameObjects)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        }
    }
}
