using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private int health;
    private bool isDestroy;
    private List<GameObject> enemiesImCollidingWith = new List<GameObject>();

    private float lastAttackTime;

    void Start()
    {
        health = 100;
        IsDestroy = false;
    }

    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemiesImCollidingWith.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemiesImCollidingWith.Remove(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Abort if we already attacked recently.
        if (Time.time - lastAttackTime < 1) 
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            TakeDamage();
            lastAttackTime = Time.time;
        }
    }

    private void TakeDamage()
    {
        foreach (var enemy in enemiesImCollidingWith)
        {
            Health -= enemy.GetComponent<Enemy>().Damage;

            Debug.Log(health);

            if (Health <= 0)
            {
                IsDestroy = true;
                Destroy(this.gameObject);
            }
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
