using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed;
    private int health;
    private int damage;
    private bool canMove;
    private Vector2 movement;
    private Quaternion actualRotationSens;
    private List<GameObject> enemiesImCollidingWith = new List<GameObject>();
    private float lastDamageTime;
    private Rigidbody2D rb;
    private Dictionary<string, GameObject> cauldrons = new Dictionary<string, GameObject>();
    private GameObject actualSpell;
    private Cauldron touchedCauldron;
    private Vector2 mousePosition;
    private Animator animator;

    private Vector3 playerLastPosition;
    private Vector3 playerActualPosition;


    void Start()
    {
        Health = 20;
        speed = 2;
        rb = this.GetComponent<Rigidbody2D>();
        actualSpell = null;
        canMove = true;
        animator = this.GetComponent<Animator>();
        animator.SetBool("isWalking", false);

        foreach (var cauldron in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            cauldrons.Add(cauldron.GetComponent<Cauldron>().type, cauldron.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (!CanMove)
            DontMovePlayer();
        else
            MovePlayer();

        actualRotationSens = this.transform.rotation;
    }

    private void Update()
    {
        // Get potion
        if (Input.GetKeyDown(KeyCode.E)) // changer pour input "GetPotion" KeyCode.E Input.GetAxisRaw("Horizontal"); 
        {
            if (ActualSpell == null) // Player can get a unique spell at the same time
            {
                // Get spell in link with the touched cauldron
                ActualSpell = touchedCauldron.spell;

                // Emptying the cauldron
                ChangeContentInCauldron(touchedCauldron.type, false);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0)) // changer pour input "UseSpell" KeyCode.Mouse0
        {
            if (actualSpell != null)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // penser à lancer lorsque animation terminée !
                UseSpell(actualSpell);
                ActualSpell = null;
            }
            else if (actualSpell == null && enemiesImCollidingWith.Count > 0) // Player can attack even if he don't have spell
                PlayerAttack();
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            enemiesImCollidingWith.Add(collision.gameObject);
            damage = collision.gameObject.GetComponent<Enemy>().Health / 6;
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
        playerActualPosition = this.transform.position;

        // Play idle animation or walking animation
        IsMoving(playerActualPosition, playerLastPosition);

        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");


        movement = new Vector2(moveHorizontal * speed, moveVertical * speed);
        playerLastPosition = this.transform.position;

        PlayerOrientation(moveHorizontal);
        rb.velocity = movement;
    }

    private void DontMovePlayer()
    {
        animator.SetBool("isWalking", false);
        rb.velocity = Vector2.zero;
    }

    private void IsMoving(Vector3 playerActualPosition, Vector3 playerLastPosition)
    {
        if (playerActualPosition == playerLastPosition)
            animator.SetBool("isWalking", false);
        else
            animator.SetBool("isWalking", true);
    }

    public void PlayerOrientation(float moveHorizontal)
    {
        if (moveHorizontal > 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveHorizontal < 0)
            this.transform.rotation = Quaternion.Euler(0, -180, 0);
        else
            this.transform.rotation = actualRotationSens;
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
        GameObject spellPoint = this.transform.GetChild(0).gameObject;

        switch (spell.tag)
        {
            case "laser":
                Instantiate(spell, spellPoint.transform.position, Quaternion.Euler(0, actualRotationSens.y * 180, 0));
                    break;
            case "bomb":
                Instantiate(spell, mousePosition, Quaternion.Euler(0, actualRotationSens.y * 180, 0));
                break;
            case "wave":
                Instantiate(spell, spellPoint.transform.position, Quaternion.Euler(0, actualRotationSens.y * 180, 0));
                break;
            case "life":
                Health += 10;
                // FEEDBACK À AJOUTER
                // BARRE DE VIE À AUGMENTER
                break;
            case "wall":
                Instantiate(spell, mousePosition, Quaternion.Euler(0, actualRotationSens.y * 180, 0));
                break;
            default:
                break;
        }
        
    }

    private void PlayerAttack()
    {
        // ANIMATION COUP DE BATON AU SOL + CERCLE AUTOUR JOUEUR
        Debug.Log("playerAttack");
        animator.SetBool("attack", true);
        foreach (var enemy in enemiesImCollidingWith)
        {
            enemy.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            if (enemy.GetComponent<Enemy>().Health <= 0)
            {
                enemiesImCollidingWith.Remove(enemy);
                return;
            }
        }
        
    }

    private void test()
    {
        Debug.Log("test");
        animator.SetBool("attack", false);
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

    public Quaternion ActualRotation
    {
        get { return actualRotationSens; }
        set { actualRotationSens = value; }
    }

}
