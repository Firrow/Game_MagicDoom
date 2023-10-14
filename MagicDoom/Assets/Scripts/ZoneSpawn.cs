using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawn : MonoBehaviour
{
    public GameObject enemy;

    private Vector2 spawnPosition;
    private float delay = 2f; //à voir si temps aléatoire ou évolu

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        spawnPosition = new Vector2(Random.Range(this.transform.position.x-0.3f, this.transform.position.x+0.3f), Random.Range(this.transform.position.y-2f, this.transform.position.y + 2f));
        if(this.gameObject.name == "ZoneSpawn1")
            Instantiate(enemy, spawnPosition, Quaternion.Euler(0, 0, 0));
        else
            Instantiate(enemy, spawnPosition, Quaternion.Euler(0, -180, 0));
        yield return new WaitForSeconds(delay);

        StartCoroutine(Spawn());
    }
}

