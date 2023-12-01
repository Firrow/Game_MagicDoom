using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    [SerializeField] AudioClip soundClick;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void LoadScreenDifficulty()
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Difficulty"));
    }

    public void LoadScreenSettings()
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Settings"));
    }

    public void LoadScreenCredits()
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Credits"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(soundClick.length - 0.2f);
        SceneManager.LoadScene(sceneName);
    }
}
