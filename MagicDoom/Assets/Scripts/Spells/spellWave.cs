using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellWave : MonoBehaviour
{
    [SerializeField] AudioClip sound;

    private float speed;
    private Vector3 movement;
    private Vector3 sensShoot = new Vector3(1, 0, 0);
    private int damage;
    private Animator animator;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
        damage = 100;
        speed = 3f;

        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        movement = new Vector3(sensShoot.x, 0, 0);

        gameObject.transform.Translate(movement * speed * 2.5f * Time.deltaTime);
        gameObject.transform.localScale += new Vector3(0.0035f, 0.0035f, 0);

        RecalculateColliderShape();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void RecalculateColliderShape()
    {
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }

    private void DestroyWave()
    {
        Destroy(this.gameObject);
    }

}
