using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject[] gems;
    [SerializeField] AudioClip[] soundsScream;

    private List<GameObject> potentialTargetList = new List<GameObject>();
    private GameObject targetEnemy;
    private Player player;
    private float speed;
    private int damage;
    private int health;
    private int spawnRateGems;
    private int collisionCount;
    private Animator animator;
    private AudioSource audioSource;


    void Start()
    {
        speed = 0.5f;
        damage = 2;
        Health = 12;
        spawnRateGems = 5;
        collisionCount = 0;
        animator = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioSource = this.GetComponent<AudioSource>();


        foreach (var item in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            potentialTargetList.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            potentialTargetList.Add(item);
        }

        ChoiceTarget();
        EnemyOrientation();
    }

    void Update()
    {
        potentialTargetList.RemoveAll(item => item == null); // Removing destroy Cauldrons after coroutine
        ChoiceTarget();

        if (collisionCount == 0)
        {
            animator.SetBool("isAttacking", false);
            MoveEnemy();
        }
        EnemyOrientation();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemies"))
        {
            collisionCount++;
            animator.SetBool("isAttacking", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
    }

    private void MoveEnemy()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
    }


    public void EnemyOrientation()
    {
        if (this.transform.position.x > targetEnemy.transform.position.x)
            this.transform.rotation = Quaternion.Euler(0, -180, 0);
        else
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void ChoiceTarget()
    {
        float maxDistance = float.PositiveInfinity;

        foreach (var item in potentialTargetList)
        {
            if(item != null) {
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < maxDistance)
                {
                    targetEnemy = item;
                    maxDistance = distance;
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(soundsScream[Random.Range(0, 4)]);
        animator.SetTrigger("takeDamage");
        health -= damage;

        if (Health <= 0)
        {
            bool gemWillBeDrop = (Random.Range(0, 10) >= spawnRateGems ? true : false);

            if (gemWillBeDrop)
                spawnGem();

            Destroy(this.gameObject);
            player.Score++;
            player.checkEndGame();
        }
    }

    private void spawnGem()
    {
        Instantiate(gems[Random.Range(0, 5)], this.transform.position, this.transform.rotation);
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }


}
