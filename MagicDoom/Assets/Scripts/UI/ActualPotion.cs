using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActualPotion : MonoBehaviour
{
    [SerializeField] Sprite[] potionSprites;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (player.ActualSpell != null)
        {
            switch (player.ActualSpell.transform.tag)
            {
                case "laser":
                    this.GetComponent<Image>().sprite = potionSprites[1];
                    break;
                case "wave":
                    this.GetComponent<Image>().sprite = potionSprites[2];
                    break;
                case "wall":
                    this.GetComponent<Image>().sprite = potionSprites[3];
                    break;
                case "bomb":
                    this.GetComponent<Image>().sprite = potionSprites[4];
                    break;
                case "life":
                    this.GetComponent<Image>().sprite = potionSprites[5];
                    break;
                default:
                    break;
            }
        }
        else
            this.GetComponent<Image>().sprite = potionSprites[0];
    }
}
