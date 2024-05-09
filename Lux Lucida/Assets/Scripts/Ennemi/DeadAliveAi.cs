
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]

public class DeadAliveAi : MonoBehaviour
{

    [SerializeField] private Animator AnimatorE;
    
    private CapsuleCollider2D capsuleCollider;
    


    // Start is called before the first frame update
    void Start()
    {
        
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
            AnimatorE.SetBool("Dead", true);
        }
    }

    public void Dies()
    {
        Destroy(gameObject.transform.parent.gameObject);
    } 
}
