using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DifficultyMenu : MonoBehaviour
{
    public void LoadEasyGame(string difficulty)
    {
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene("ManonGame");
    }

    public void LoadNormalGame(string difficulty)
    {
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene("ManonGame");
    }

    public void LoadHardGame(string difficulty)
    {
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene("ManonGame");
    }

    public void LoadGreatWizardGame(string difficulty)
    {
        GameManager.difficulty = difficulty;
        SceneManager.LoadScene("ManonGame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
