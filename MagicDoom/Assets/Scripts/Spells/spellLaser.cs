using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellLaser : MonoBehaviour
{
    private int damage;
    private Player player;

    void Start()
    {
        damage = 100;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.CanMove = false;
    }

    void Update()
    {
        Destroy(this.gameObject, 2.0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void OnDestroy()
    {
        player.Animator.speed = 1f;
        player.Animator.SetBool("castSpell", false);
        player.CanMove = true;
    }
}
