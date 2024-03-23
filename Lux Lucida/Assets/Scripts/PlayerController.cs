using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator AnimatorPlayer;
    private Rigidbody2D Rigidbody;

    private float ControleX;
    private float ControleY;

    private float QuantiteForce = 5 ;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ControleX = Input.GetAxis("Horizontal");
        AnimatorPlayer.SetFloat("MouvementX", ControleX);
        ControleY = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(ControleX, 0);

        if (direction.magnitude > 0.01)
        {
            //ds
        }

        float Vitesse = Rigidbody.velocity.magnitude;
        AnimatorPlayer.SetFloat("Vitesse", Vitesse);
    }

    private void FixedUpdate()
    {
        Rigidbody.AddForce(new Vector2(ControleX, 0) * QuantiteForce);
    }
}