using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player player;
    private int numberEnemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        NumberEnemy = 25;

        //récupérer difficulté choisie dans menu des difficultés
        /*switch (difficulty)
        {
            case "easy":
                NumberEnemy = 25;
                break;
            case "normal" :
                NumberEnemy = 50;
                break;
            case "hard":
                NumberEnemy = 75;
                break;
            case "great wizard":
                NumberEnemy = 100;
                break;
            default:
                break;
        }*/
    }

    void Update()
    {
        if (player.GameOver)
        {
            Application.LoadLevel("EndGameOver");
        }
        if (player.Victory)
        {
            Application.LoadLevel("EndVictory");
        }
    }

    public int NumberEnemy
    {
        get { return numberEnemy; }
        set { numberEnemy = value; }
    }

}
