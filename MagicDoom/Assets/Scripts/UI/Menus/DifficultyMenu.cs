using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DifficultyMenu : MonoBehaviour
{
    [SerializeField] AudioClip soundClick;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void LoadGame(string difficulty)
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelayGameLoad(difficulty, "Game"));
    }

    public void LoadMainMenu()
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Menu"));
    }


    // Allow to play sound before change scene
    IEnumerator DelayGameLoad(string difficulty, string sceneName)
    {
        yield return new WaitForSeconds(soundClick.length - 0.2f);
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(soundClick.length - 0.2f);
        SceneManager.LoadScene(sceneName);
    }
}
