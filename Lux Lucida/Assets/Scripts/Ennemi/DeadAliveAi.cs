
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class DeadAliveAi : MonoBehaviour
{

    
    
    private CapsuleCollider2D capsuleCollider;
    private Animator AnimatorE;


    // Start is called before the first frame update
    void Start()
    {
        AnimatorE = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AnimatorE.
        }
    }
}
