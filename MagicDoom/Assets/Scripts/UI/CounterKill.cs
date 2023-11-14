using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CounterKill : MonoBehaviour
{
    private Player player;
    private TMP_Text textScore;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        textScore = this.GetComponent<TMP_Text>();
    }

    void Update()
    {
        textScore.text = player.Score.ToString();
    }
}
