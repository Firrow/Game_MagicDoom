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

    [SerializeField] GameObject[] gems;


    void Start()
    {
        speed = 0.5f;
        damage = 2;
        health = 10;

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

        if (Vector2.Distance(transform.position, targetEnemy.transform.position) > 0.3f)
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
        EnemyOrientation();
    }



    public void EnemyOrientation()
    {
        if (this.transform.position.x > targetEnemy.transform.position.x)
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            this.transform.rotation = Quaternion.Euler(0, -180, 0);
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

    // TODO : compléter les dégâts en fonction du sortilège du joueur
    private void TakeDamage(int damage)
    {
        // Calcul de la santé à faire ici (en fonction sortilège joueur)
        health -= damage;

        if (Health <= 0)
        {
            bool gemWillBeDrop = (Random.Range(0, 10) >= 7 ? true : false);

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
