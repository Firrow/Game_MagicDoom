using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed;
    private List<GameObject> potentialTarget = new List<GameObject>();
    private float maxDistance = 1000;
    private GameObject targetEnemy;

    void Start()
    {
        speed = 0.5f;

        foreach (var item in GameObject.FindGameObjectsWithTag("Cauldron"))
        {
            potentialTarget.Add(item);
        }
        potentialTarget.Add(GameObject.FindGameObjectWithTag("Player"));

        ChoiceTarget();
    }


    void Update()
    {
        //when enemy is close to target
        if (Vector2.Distance(transform.position, targetEnemy.transform.position) > 0.5f)
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);
    }

    private void ChoiceTarget()
    {
        foreach (var item in potentialTarget)
        {
            var distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < maxDistance)
                targetEnemy = item;
                maxDistance = distance;
        }
    }
}
