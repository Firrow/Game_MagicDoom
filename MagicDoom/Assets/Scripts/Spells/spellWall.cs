using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellWall : MonoBehaviour
{
    private float lastDamageTime;
    private int health;
    private int defaultHealth;
    private List<GameObject> enemiesImCollidingWith = new List<GameObject>();
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] wallSprites;


    void Start()
    {
        defaultHealth = 50;
        Health = defaultHealth;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
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
            TakeDamage();
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

    public void TakeDamage()
    {
        foreach (var enemy in enemiesImCollidingWith)
        {
            Health -= enemy.GetComponent<Enemy>().Damage;
        }


        if (Health > defaultHealth * 0.67)
        {
            spriteRenderer.sprite = wallSprites[0];
            RecalculateColliderShape();
        }
        else if (Health > defaultHealth * 0.33)
        {
            spriteRenderer.sprite = wallSprites[1];
            RecalculateColliderShape();
        }
        else if (Health > 0)
        {
            spriteRenderer.sprite = wallSprites[2];
            RecalculateColliderShape();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void RecalculateColliderShape()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }



    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
