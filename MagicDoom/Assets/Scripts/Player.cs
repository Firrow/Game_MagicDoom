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
    private GameObject spellPoint;
    private GameObject actualSpell;
    private string typeCauldron;


    void Start()
    {
        health = 20;
        speed = 2;
        rb = this.GetComponent<Rigidbody2D>();
        spellPoint = this.transform.GetChild(0).gameObject;
        actualSpell = null;

        foreach (var cauldron in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            cauldrons.Add(cauldron.GetComponent<Cauldron>().type, cauldron.gameObject);
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

        if (actualSpell != null && Input.GetKeyDown(KeyCode.Mouse0)) // changer pour input "UseSpell"
        {
            Debug.Log("TIRER !!!!!!!!!!!");
            UseSpell(actualSpell);
        }
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
        /*else if (collision.gameObject.layer == LayerMask.NameToLayer("Cauldrons"))
        {
            // Get potion
            if (actualSpell == null && Input.GetKeyDown(KeyCode.E)) // changer pour input "GetPotion"
            {
                // Get spell in link with the touched cauldron
                actualSpell = collision.gameObject.GetComponent<Cauldron>().spell;

                // Emptying the cauldron
                ChangeContentInCauldron(collision.gameObject.GetComponent<Cauldron>().type, false);
            }
        }*/
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
                ChangeContentInCauldron("laser", true);
                break;
            case "Green":
                ChangeContentInCauldron("wall", true);
                break;
            case "Red":
                ChangeContentInCauldron("life", true);
                break;
            case "Blue":
                ChangeContentInCauldron("wave", true);
                break;
            case "Purple":
                ChangeContentInCauldron("bomb", true);
                break;
            default:
                break;
        }
    }

    public void ChangeContentInCauldron(string typePotion, bool fill)
    {
        if (cauldrons[typePotion] != null)
        {
            GameObject empty = cauldrons[typePotion].transform.GetChild(0).gameObject;
            GameObject liquide = cauldrons[typePotion].transform.GetChild(1).gameObject;

            if (fill)
            {
                empty.SetActive(false);
                liquide.SetActive(true);
            }
            else
            {
                empty.SetActive(true);
                liquide.SetActive(false);
            }
        }
        else
        {
            return;
        }
    }

    private void UseSpell(GameObject spell)
    {
        Instantiate(spell, spellPoint.transform.position, Quaternion.Euler(0, 0, 0));
    }





    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public GameObject ActualSpell
    {
        get { return actualSpell; }
        set { actualSpell = value; }
    }

}
