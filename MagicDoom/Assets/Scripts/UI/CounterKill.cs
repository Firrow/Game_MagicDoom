using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterKill : MonoBehaviour
{
    private Player player;
    private TMP_Text textScore;
    private GameManager gameManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        textScore = this.GetComponent<TMP_Text>();
    }

    void Update()
    {
        textScore.text = player.Score.ToString() + " / " + gameManager.NumberEnemy.ToString();
    }
}
