using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    /*private Button button;

    void Start()
    {
        foreach (var button in GameObject.FindGameObjectsWithTag("Button"))
        {
            button.GetComponent<Button>().onClick.AddListener(ButtonSelected);
        }
    }

    private void ButtonSelected()
    {
        Debug.Log("click");
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(ButtonSelected);
    }*/

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
