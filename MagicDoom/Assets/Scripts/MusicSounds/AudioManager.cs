using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlistMusics;
    public AudioSource audioSource;

    private List<string> sceneWithSameMusic = new List<string>();
    private string lastSceneName;
    private string currentScene;

    private void Awake()
    {
        lastSceneName = "";

        sceneWithSameMusic.Add("Menu");
        sceneWithSameMusic.Add("Difficulty");
        sceneWithSameMusic.Add("Settings");
        sceneWithSameMusic.Add("Credits");
    }

    void OnEnable()
    {
        SceneManager.activeSceneChanged += GetLastSceneName;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (sceneWithSameMusic.Contains(lastSceneName))
        {
            if (sceneWithSameMusic.Contains(currentScene))
                return; // Allow to continu the current music (same for all first menus)
            else
                changeMusic();
        }
        else
        {
            if (sceneWithSameMusic.Contains(currentScene))
            {
                audioSource.clip = playlistMusics[0];
                audioSource.Play();
            }
            else
                changeMusic();
        }
    }

    private void GetLastSceneName(Scene current, Scene next)
    {
        lastSceneName = currentScene;
    }

    private void changeMusic()
    {
        if (currentScene == "ManonGame")
        {
            audioSource.clip = playlistMusics[1];
            audioSource.Play();
        }
        else if (currentScene == "EndVictory")
        {
            audioSource.clip = playlistMusics[2];
            audioSource.Play();
        }
        else if (currentScene == "EndGameOver")
        {
            audioSource.clip = playlistMusics[3];
            audioSource.Play();
        }
    }
}
