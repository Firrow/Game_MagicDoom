using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("Difficulty");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
