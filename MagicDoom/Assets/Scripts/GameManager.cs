using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static string difficulty;

    private Player player;
    private int numberEnemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //r�cup�rer difficult� choisie dans menu des difficult�s
        switch (difficulty)
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
            case "greatWizard":
                NumberEnemy = 100;
                break;
            default:
                break;
        }

        Debug.Log("Nombre d'ennemi s�lectionn� : " + NumberEnemy);
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
