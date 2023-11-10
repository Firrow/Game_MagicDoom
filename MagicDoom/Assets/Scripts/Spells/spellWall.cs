using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellWall : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("touched ! ");
        //récupère l'ennemi
        //choiceTarget
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("touched STAY ! ");
    }
}
