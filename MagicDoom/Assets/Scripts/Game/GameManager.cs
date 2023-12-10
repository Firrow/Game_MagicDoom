using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static string difficulty;

    private Player player;
    private int numberEnemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //récupérer difficulté choisie dans menu des difficultés
        switch (difficulty)
        {
            case "easy":
                NumberEnemy = 25;
                break;
            case "normal" :
                NumberEnemy = 40;
                break;
            case "hard":
                NumberEnemy = 70;
                break;
            case "greatWizard":
                NumberEnemy = 100;
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (player.GameOver)
        {
            SceneManager.LoadScene("EndGameOver");
        }
        if (player.Victory)
        {
            SceneManager.LoadScene("EndVictory");
        }
    }

    public int NumberEnemy
    {
        get { return numberEnemy; }
        set { numberEnemy = value; }
    }

}
