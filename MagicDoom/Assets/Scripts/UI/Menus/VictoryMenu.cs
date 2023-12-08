using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public GameObject TitleAndButtons;
    public Animator animatorVictory;

    public void Appearing()
    {
        animatorVictory.SetTrigger("Appear");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Difficulty");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
