using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellWall : MonoBehaviour
{
    private float lastDamageTime;
    private int health;
    private int damage;
    private List<GameObject> enemiesImCollidingWith = new List<GameObject>();


    void Start()
    {
        Health = 30;
        damage = 2;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemiesImCollidingWith.Add(collision.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Abort if we already attacked recently.
        if (Time.time - lastDamageTime < 1)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            TakeDamage(damage);
            lastDamageTime = Time.time;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemiesImCollidingWith.Remove(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        foreach (var enemy in enemiesImCollidingWith)
        {
            Health -= enemy.GetComponent<Enemy>().Damage;

            //FEEDBACK DESTRUCTION WALL
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }



    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
