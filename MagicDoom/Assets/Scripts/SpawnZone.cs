using System.Collections;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject enemy;

    private float delay = 1f; //à voir si temps aléatoire ou évolu

    private float centerZoneX;
    private float centerZoneY;
    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        centerZoneX = transform.position.x;
        centerZoneY = transform.position.y;
        halfWidth = this.gameObject.GetComponent<BoxCollider2D>().size.x / 2;
        halfHeight = this.gameObject.GetComponent<BoxCollider2D>().size.y / 2;

        StartCoroutine(CallSpawnEnemy());
    }

    IEnumerator CallSpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(centerZoneX - halfWidth, centerZoneX + halfWidth), Random.Range(centerZoneY - halfHeight, centerZoneY + halfHeight));
        Instantiate(enemy, spawnPosition, Quaternion.Euler(0, 0, 0));

        yield return new WaitForSeconds(delay);

        StartCoroutine(CallSpawnEnemy());
    }
}