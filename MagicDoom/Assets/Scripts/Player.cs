using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    private bool isDestroy;

    //FAIRE LA MÊME CHOSE QUE POUR LES CHAUDRONS


    void Start()
    {
        health = 20;
        IsDestroy = false;
}

    void Update()
    {
        
    }


    public void TakeDamage(int damage)
    {
        this.Health -= damage;

        if (this.Health <= 0)
        {
            IsDestroy = true;
        }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public bool IsDestroy
    {
        get { return isDestroy; }
        set { isDestroy = value; }
    }
}
