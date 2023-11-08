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
    private Dictionary<string, GameObject> cauldrons = new Dictionary<string, GameObject>();

    void Start()
    {
        health = 20;
        speed = 2;
        rb = this.GetComponent<Rigidbody2D>();
        foreach (var cauldron in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            cauldrons.Add(cauldron.GetComponent<Cauldron>().type, cauldron.gameObject);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemiesImCollidingWith.Add(collision.gameObject);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Gems"))
        {
            FindCauldron(collision.transform.tag);
            Destroy(collision.gameObject);
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


    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal * speed, moveVertical * speed);

        rb.velocity = movement;
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

    private void FindCauldron(string typeGem)
    {
        switch (typeGem)
        {
            // fill cauldrons thanks to gem
            case "Yellow":
                FillCauldron("laser");
                break;
            case "Green":
                FillCauldron("wall");
                break;
            case "Red":
                FillCauldron("life");
                break;
            case "Blue":
                FillCauldron("wave");
                break;
            case "Purple":
                FillCauldron("bomb");
                break;
            default:
                Debug.Log("CHAUDRON DÉTRUIT");
                break;
        }
    }

    private void FillCauldron(string typePotion)
    {
        if (cauldrons[typePotion] != null)
        {
            GameObject empty = cauldrons[typePotion].transform.GetChild(0).gameObject;
            GameObject liquide = cauldrons[typePotion].transform.GetChild(1).gameObject;
            empty.SetActive(false);
            liquide.SetActive(true);
        }
        else
        {
            return;
        }

    }





    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
