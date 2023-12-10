using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField] AudioClip soundClick;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void LoadMenu()
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Menu"));
    }

    // Allow to play sound before change scene
    IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(soundClick.length - 0.2f);
        SceneManager.LoadScene(sceneName);
    }
}
