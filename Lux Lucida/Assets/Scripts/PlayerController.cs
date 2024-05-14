using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator AnimatorPlayer;
    private Rigidbody2D Rigidbody;


    private float controleX;
    private float controleY;
    private bool isGrounded;


    [SerializeField]
    private float walkingSpeed = 5;
    [SerializeField]
    private float jumpForce = 5;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        controleX = Input.GetAxis("Horizontal");
        controleY = Input.GetAxis("Vertical");
        
        
        AnimatorPlayer.SetFloat("MouvementX", controleX);

        Vector2 direction = new Vector2(controleX, 0);
        float Vitesse = direction.magnitude;

        AnimatorPlayer.SetFloat("Vitesse", Vitesse);
    }

    private void FixedUpdate()
    {   
        Rigidbody.velocity = new Vector2(controleX * walkingSpeed, Rigidbody.velocity.y);
        isGrounded = (Mathf.Abs(Rigidbody.velocity.y) <= 0.01);
        if (isGrounded && Input.GetKey("space"))
        {
            Rigidbody.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Killzone"))
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eRestartLevel, null);
        }
    }



}