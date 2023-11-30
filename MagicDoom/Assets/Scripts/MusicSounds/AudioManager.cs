using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public List<AudioClip> playlistMusics = new List<AudioClip>();

    Dictionary<string, string> sceneNameToMusicName = new Dictionary<string, string>();
    private string lastSceneName;

    private static AudioManager instance = null;
    private AudioSource myAudio;

    private void Awake()
    {
        sceneNameToMusicName.Add("Menu", "menu");
        sceneNameToMusicName.Add("Difficulty", "menu");
        sceneNameToMusicName.Add("Settings", "menu");
        sceneNameToMusicName.Add("Credits", "menu");
        sceneNameToMusicName.Add("ManonGame", "game");
        sceneNameToMusicName.Add("EndVictory", "victory");
        sceneNameToMusicName.Add("EndGameOver", "gameOver");

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return;
            Destroy(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if(myAudio == null)
        {
            myAudio = this.GetComponent<AudioSource>();
            myAudio.clip = playlistMusics[0];
            myAudio.Play();
        }

        if (lastSceneName == null)
            lastSceneName = scene.name;

        Debug.Log("latest scene " + lastSceneName + "current scene " + scene.name);

        if (sceneNameToMusicName[lastSceneName] != sceneNameToMusicName[scene.name])
        {
            myAudio.clip = SearchMusicByName(sceneNameToMusicName[scene.name]);
            myAudio.Play();
        }

        lastSceneName = scene.name;
    }

    private AudioClip SearchMusicByName(string name)
    {
        foreach (var music in playlistMusics)
        {
            Debug.Log("music searched : " + music.name);
            if (music.name == name)
            {

                return music;
            }
        }
        throw new System.Exception("Music " + name + "not found");
    }
}
