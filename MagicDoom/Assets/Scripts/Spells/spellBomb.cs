using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellBomb : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        //animator.SetBool("willExplode", true);
    }


    void Update()
    {

    }

}
