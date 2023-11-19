using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public void LoadScreenDifficulty()
    {
        SceneManager.LoadScene("Difficulty");
    }

    public void LoadScreenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void LoadScreenCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
