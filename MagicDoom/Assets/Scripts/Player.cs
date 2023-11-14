using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthBar healthBar;

    private float speed;
    private int health;
    private int maxHealth = 20;
    private int damage;
    private int score;
    private bool canMove;
    private bool isDead;
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
        Health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        speed = 2;
        rb = this.GetComponent<Rigidbody2D>();
        actualSpell = null;
        canMove = true;
        Animator = this.GetComponent<Animator>();
        isDead = false;
        score = 0;


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

                Animator.SetBool("castSpell", true);
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
        if (!isDead)
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
    }

    private void DontMovePlayer()
    {
        Animator.SetBool("isWalking", false);
        rb.velocity = Vector2.zero;
    }

    private void IsMoving(Vector3 playerActualPosition, Vector3 playerLastPosition)
    {
        if (playerActualPosition == playerLastPosition)
            Animator.SetBool("isWalking", false);
        else
            Animator.SetBool("isWalking", true);
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
        Animator.SetBool("takeDamage", true);

        foreach (var enemy in enemiesImCollidingWith)
        {
            Health -= enemy.GetComponent<Enemy>().Damage;
            healthBar.SetHealth(Health);

            if (Health <= 0)
            {
                isDead = true;
                this.GetComponent<PolygonCollider2D>().enabled = false;
                Animator.SetTrigger("isDead");
                Animator.SetBool("takeDamage", false);
            }
        }
    }

    private void StopTakeDamageAnimation()
    {
        Animator.SetBool("takeDamage", false);
    }

    private void DestroyPlayer() // Called when animation is over
    {
        Destroy(this.gameObject);
        // Lancer fin du jeu
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

    private void CastSpell() // Called when animation is over
    {
        if (actualSpell.transform.tag == "laser")
        {
            animator.speed = 0f;
            UseSpell(actualSpell);
            ActualSpell = null;
        }
        else
        {
            UseSpell(actualSpell);
            ActualSpell = null;
            Animator.SetBool("castSpell", false);
        }
        
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
                if (Health < maxHealth)
                {
                    Health += 10;
                    healthBar.SetHealth(Health);
                }
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
        Animator.SetBool("attack", true);
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

    private void StopAttackAnimation()
    {
        Animator.SetBool("attack", false);
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


    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }


}
