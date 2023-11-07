using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private int health;
    private bool isDestroy;

    void Start()
    {
        health = 5;
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
