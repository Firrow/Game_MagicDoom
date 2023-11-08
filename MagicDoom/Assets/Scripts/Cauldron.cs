using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public string type;

    private int health;
    private int defaultHealth;
    private List<GameObject> enemiesImCollidingWith = new List<GameObject>();
    private float lastDamageTime;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] cauldronSprites;


    void Start()
    {
        defaultHealth = 100;
        health = defaultHealth;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
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

            Debug.Log(this.gameObject.name + " : " + Health);
        }

        if (Health > defaultHealth * 0.75)
            spriteRenderer.sprite = cauldronSprites[0];
        else if (Health > defaultHealth * 0.5)
            spriteRenderer.sprite = cauldronSprites[1];
        else if (Health > defaultHealth * 0.25)
            spriteRenderer.sprite = cauldronSprites[2];
        else if (Health > 0)
            spriteRenderer.sprite = cauldronSprites[3];
        else
        {
            Destroy(this.gameObject);
        }

    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
