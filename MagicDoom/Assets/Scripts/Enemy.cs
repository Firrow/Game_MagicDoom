using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private List<GameObject> potentialTarget = new List<GameObject>();
    private float maxDistance;
    private GameObject targetEnemy;

    void Start()
    {
        speed = 0.5f;
        maxDistance = float.PositiveInfinity;

        foreach (var item in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            potentialTarget.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            potentialTarget.Add(item);
        }


        ChoiceTarget();
        EnemyOrientation();
    }


    void Update()
    {
        ChoiceTarget();
        //when enemy is close to target
        if (Vector2.Distance(transform.position, targetEnemy.transform.position) > 0.5f)
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
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
        foreach (var item in potentialTarget)
        {
            var distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < maxDistance)
            {
                targetEnemy = item;
                maxDistance = distance;
            }
        }
    }
}
