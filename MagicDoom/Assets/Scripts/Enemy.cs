using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<GameObject> potentialTargetList = new List<GameObject>();
    private GameObject targetEnemy;
    private float speed;
    private int damage;
    private int health;
    private bool isCollided;
    private int spawnRateGems;

    [SerializeField] GameObject[] gems;


    void Start()
    {
        speed = 0.5f;
        damage = 2;
        health = 10;
        isCollided = false;
        spawnRateGems = 5;

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

        //Debug.Log(isCollided);    BUG COLLISION QUAND CHANGEMENT SOUDAIN TARGET

        if (!isCollided)
        {
            MoveEnemy();
        }
        EnemyOrientation();

        // UNIQUEMENT POUR LE DÉVELOPPEMENT : A ENLEVER QUAND PLUS NÉCESSAIRE
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
        //---------------------------------------------------------------------
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemies"))
        {
            isCollided = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isCollided = false;
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
        health -= damage;

        if (Health <= 0)
        {
            bool gemWillBeDrop = (Random.Range(0, 10) >= spawnRateGems ? true : false);

            if (gemWillBeDrop)
                spawnGem();

            Destroy(this.gameObject);
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
