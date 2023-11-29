using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject gameUI;
    public GameObject settingsMenuUI;

    private bool isPause;
    private Player player;

    private void Start()
    {
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
        settingsMenuUI.SetActive(true);
        gameUI.SetActive(false);
        Time.timeScale = 0;
        isPause = true;
    }

    public void LoadMainMenu()
    {
        Resume(); // Allow to disabled timeScale
        SceneManager.LoadScene("Menu");
    }



    public bool IsPause
    {
        get { return isPause; }
        set { isPause = value; }
    }

}
