using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<GameObject> potentialTarget = new List<GameObject>();
    private float maxDistance;
    private GameObject targetEnemy;
    private float speed;
    private int damage;


    void Start()
    {
        speed = 0.5f;
        damage = 2;
        maxDistance = float.PositiveInfinity;

        foreach (var item in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            potentialTarget.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            potentialTarget.Add(item);
        }

        ChoiceTarget(potentialTarget);
        EnemyOrientation();
    }


    void Update()
    {
        ChoiceTarget(potentialTarget);
        //when enemy is close to target
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

    private void ChoiceTarget(List<GameObject> potentialTarget)
    {
        foreach (var item in potentialTarget)
        {
            //Debug.Log(item.name);
            //Debug.Log("position ennemi : " + transform.position + " ||| item name : " + item.name + " item position : " + item.transform.position);
            var distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < maxDistance)
            {
                targetEnemy = item;
                maxDistance = distance;
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
        if(target.layer == LayerMask.NameToLayer("Cauldrons"))
        {
            target.GetComponent<Cauldron>().takeDamage(damage);

            if (target.GetComponent<Cauldron>().IsDestroy == true)
            {
                Debug.Log(potentialTarget.Count);
                potentialTarget.Remove(target.gameObject);
                Debug.Log(potentialTarget.Count);
                Destroy(target.gameObject);


            }
        }
        else if (target.layer == LayerMask.NameToLayer("Player"))
        {
            target.GetComponent<Player>().takeDamage(damage);

            if (target.GetComponent<Player>().IsDestroy == true)
            {
                Debug.Log("Game Over");
            }
        }
        yield return new WaitForSeconds(1.0f);
    }
}
