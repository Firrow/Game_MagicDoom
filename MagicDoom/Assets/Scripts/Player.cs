using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed;
    private int health;
    private Vector2 movement;
    private List<GameObject> enemiesImCollidingWith = new List<GameObject>();
    private float lastDamageTime;
    private Rigidbody2D rb;

    void Start()
    {
        health = 20;
        speed = 2;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal * speed, moveVertical * speed);

        rb.velocity = movement;
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
        if (Time.time - lastDamageTime < 1)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            TakeDamage();
            lastDamageTime = Time.time;
        }
    }

    private void TakeDamage()
    {
        foreach (var enemy in enemiesImCollidingWith)
        {
            Health -= enemy.GetComponent<Enemy>().Damage;

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
