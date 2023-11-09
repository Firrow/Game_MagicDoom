using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellWave : MonoBehaviour
{
    private float speed;
    private Vector3 movement;
    private Vector3 sensShoot = new Vector3(1, 0, 0);
    private int damage;


    void Start()
    {
        damage = 10;
        speed = 2.5f;
    }

    void Update()
    {
        Debug.Log(sensShoot);
        movement = new Vector3(sensShoot.x, 0, 0);
        Debug.Log("movement.x : " + movement.x);

        gameObject.transform.Translate(movement * speed * 5 * Time.deltaTime);
        gameObject.transform.localScale += new Vector3(0.004f, 0.004f, 0);

        Destroy(this.gameObject, 2.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
