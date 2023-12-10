using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellBomb : MonoBehaviour
{
    private Animator animator;
    private float appearTime;
    private bool explodeAnimation;
    private int damage;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        appearTime = Time.time;
        explodeAnimation = false;
        damage = 100;
    }

    void Update()
    {
        if (!explodeAnimation)
        {
            if (Time.time - appearTime < 3)
                return;
            else
            {
                animator.SetBool("explode", true);
                explodeAnimation = true;
            }
        }

        RecalculateColliderShape();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") && explodeAnimation == true)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void isExploded(string message)
    {
        if (message.Equals("exploded"))
        {
            Destroy(this.gameObject);
        }
    }

    private void RecalculateColliderShape()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
