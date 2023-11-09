using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed;
    private int health;
    private bool canMove;
    private Vector2 movement;
    private List<GameObject> enemiesImCollidingWith = new List<GameObject>();
    private float lastDamageTime;
    private Rigidbody2D rb;
    private Dictionary<string, GameObject> cauldrons = new Dictionary<string, GameObject>();
    private GameObject spellPoint;
    private GameObject actualSpell;
    private Cauldron touchedCauldron;


    void Start()
    {
        health = 20;
        speed = 2;
        rb = this.GetComponent<Rigidbody2D>();
        spellPoint = this.transform.GetChild(0).gameObject;
        actualSpell = null;
        canMove = true;

        foreach (var cauldron in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            cauldrons.Add(cauldron.GetComponent<Cauldron>().type, cauldron.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!CanMove)
        {
            DontMovePlayer();
        }
        else
            MovePlayer();
    }

    private void Update()
    {
        // Get potion
        if (Input.GetKeyDown(KeyCode.E)) // changer pour input "GetPotion"
        {
            if (ActualSpell == null) // Player can get a unique spell at the same time
            {
                Debug.Log("nom sortil�ge : " + touchedCauldron.spell.name);
                // Get spell in link with the touched cauldron
                ActualSpell = touchedCauldron.spell;

                // Emptying the cauldron
                ChangeContentInCauldron(touchedCauldron.type, false);
            }
        }
        else if (actualSpell != null && Input.GetKeyDown(KeyCode.Mouse0)) // changer pour input "UseSpell"
        {
            // penser � lancer lorsque animation termin�e !
            UseSpell(actualSpell);
            ActualSpell = null;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cauldrons"))
        {
            // Get which cauldron player touched
            touchedCauldron = collision.gameObject.GetComponent<Cauldron>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Cauldrons"))
        {
            touchedCauldron = null;
        }
    }


    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(moveHorizontal * speed, moveVertical * speed);

        rb.velocity = movement;
    }

    private void DontMovePlayer()
    {
        rb.velocity = Vector2.zero;
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
            return;
    }

    private void UseSpell(GameObject spell)
    {
        switch (spell.tag)
        {
            case "laser":
                Instantiate(spell, spellPoint.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, 0));
                    break;
            case "bomb":
                break;
            case "wave":
                break;
            case "life":
                break;
            case "wall":
                break;
            default:
                break;
        }
        
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

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }


}
