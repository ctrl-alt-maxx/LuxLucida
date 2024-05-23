using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PressurePlate : MonoBehaviour
{

    [SerializeField]
    private Transform _Start;
    [SerializeField]
    private Transform _End;
    private Transform _Cible;


    [SerializeField]
    private float _Vitesse = 0.01f;

    private bool _CanMove = true;
    private Rigidbody2D _Rigidbody2D;
    Vector2 _DirectionMouvement;
    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Cible = _Start;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = _Cible.position - gameObject.transform.position;
        Vector2 _DirectionVision = delta.normalized;

        _CanMove = !(delta.magnitude < _Vitesse);
        _DirectionMouvement = _DirectionVision;
    }

    private void FixedUpdate()
    {
        if (_CanMove)
        {
            _Rigidbody2D.MovePosition(_DirectionMouvement * _Vitesse + (Vector2)transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            
            
            _Cible = _End;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

           
            _Cible = _Start;
        }
    }
}
