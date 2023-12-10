using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] AudioClip soundClick;
    private AudioSource audioSource;

    public GameObject rules;
    public GameObject controls;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void LoadRules()
    {
        audioSource.PlayOneShot(soundClick);
        rules.SetActive(true);
        controls.SetActive(false);
    }

    public void LoadControls()
    {
        audioSource.PlayOneShot(soundClick);
        controls.SetActive(true);
        rules.SetActive(false);
    }

    public void LoadMenu()
    {
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Menu"));
    }

    IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(soundClick.length - 0.2f);
        SceneManager.LoadScene(sceneName);
    }
}
