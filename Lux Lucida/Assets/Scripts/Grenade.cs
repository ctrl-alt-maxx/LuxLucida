using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Grenade : MonoBehaviour
{
    [SerializeField]
    private GameObject _GrenadeModel;
    [SerializeField]
    private Animator _GrenadeAnimator;
    [SerializeField]
    private float _GrenadeShineRadius = 3.0f;
    private Collider2D _GrenadeCollider;
    private Rigidbody2D _GrenadeRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _GrenadeCollider = GetComponent<Collider2D>();
        _GrenadeRigidBody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _GrenadeModel.SetActive(false);
            _GrenadeCollider.enabled = false;
            _GrenadeRigidBody.bodyType = RigidbodyType2D.Static;
            transform.localScale = new Vector3(_GrenadeShineRadius, _GrenadeShineRadius, _GrenadeShineRadius);
            _GrenadeAnimator.SetTrigger("Activated");
        }
    }
}
