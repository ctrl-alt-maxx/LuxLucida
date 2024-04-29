using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GravityArea : MonoBehaviour
{
    [SerializeField]
    private float gravity = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attract(Transform body)
    {
        Vector3 gravityUp = -(body.position - transform.position).normalized;
        Vector3 bodyUp = body.up;
        Rigidbody rigidbody = body.gameObject.GetComponent<Rigidbody>();
        rigidbody.AddForce(gravityUp * gravity);

        Quaternion targetRotation = (Quaternion.FromToRotation(bodyUp, -gravityUp) * body.rotation);
        body.rotation = targetRotation;
    }
}
