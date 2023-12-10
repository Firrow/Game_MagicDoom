using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioClip soundClick;
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject settingsMenuUI;

    private bool isPause;
    private Player player;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isPause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Paused();
            }
        }
    }

    public void Paused()
    {
        player.CanMove = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        isPause = true;
    }

    public void Resume()
    {
        player.CanMove = true;
        pauseMenuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
        isPause = false;
    }

    public void LoadSettings()
    {
        player.CanMove = false;
        StartCoroutine(DelaySceneLoad(settingsMenuUI));
        Time.timeScale = 0;
        isPause = true;
    }

    public void LoadMainMenu()
    {
        Resume(); // Allow to disabled timeScale
        audioSource.PlayOneShot(soundClick);
        StartCoroutine(DelaySceneLoad("Menu"));
    }


    IEnumerator DelaySceneLoad(GameObject scene)
    {
        audioSource.PlayOneShot(soundClick);
        yield return new WaitWhile(() => audioSource.isPlaying);

        if (scene != null)
            scene.SetActive(true);
    }

    IEnumerator DelaySceneLoad(string sceneName)
    {
        yield return new WaitForSeconds(soundClick.length - 0.2f);
        SceneManager.LoadScene(sceneName);
    }

    public bool IsPause
    {
        get { return isPause; }
        set { isPause = value; }
    }

}
