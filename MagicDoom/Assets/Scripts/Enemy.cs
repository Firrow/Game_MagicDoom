using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<GameObject> potentialTargetList = new List<GameObject>();
    private GameObject targetEnemy;
    private float speed;
    private int damage;


    void Start()
    {
        speed = 0.5f;
        damage = 2;

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
        if (this.transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Cauldrons"))
        {
            StartCoroutine(EnemyAttack(collision.gameObject));
        }

    }

    IEnumerator EnemyAttack(GameObject target)
    {
        if (target.layer == LayerMask.NameToLayer("Player"))
        {
            target.GetComponent<Player>().TakeDamage(damage);

            if (target.GetComponent<Player>().IsDestroy == true)
            {
                Debug.Log("Game Over");
            }
        }
        yield return new WaitForSeconds(1.0f);
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

}
