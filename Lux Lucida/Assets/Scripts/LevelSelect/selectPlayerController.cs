using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class selectPlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator _Animator;
    [SerializeField]
    private GravityArea _GravityArea;
    [SerializeField]
    private float _MoveSpeed = 5.0f, _JumpForce = 5.0f;
    [SerializeField]
    private Transform _Transform3DModel;
    [SerializeField]
    private Vector3 _Adjustments = new Vector3(0.0f, -0.88f, 0.0f);
    private Vector3 _LookingDirection;
    private Rigidbody _Rigidbody;
    private Vector3 _MoveDir;
    private float ControleX, ControleY;
    private bool _JustStoppedMoving = false, _IsGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        ControleX = Input.GetAxis("Horizontal");
        ControleY = Input.GetAxis("Vertical");



        _MoveDir = new Vector3(ControleX, 0 , ControleY).normalized;
        _Animator.SetFloat("Vitesse", _MoveDir.magnitude);
        if (Input.GetKeyDown(KeyCode.Space) && _IsGrounded){
            _IsGrounded = false;
            _Rigidbody.AddForce(transform.up * _JumpForce);
        }
        _GravityArea.Attract(this.transform);
    }
    private void FixedUpdate()
    {
        if (_MoveDir.magnitude > 0.01)
        {
            _Rigidbody.MovePosition(_Rigidbody.position + transform.TransformDirection(_MoveDir) * _MoveSpeed * Time.deltaTime);

            if (Mathf.Abs(ControleX) > Mathf.Abs(ControleY))
            {
                if (ControleX > 0)
                {
                    _LookingDirection = new Vector3(0.0f, 90.0f, 0.0f);
                }
                else
                {
                    _LookingDirection = new Vector3(0.0f, 270.0f, 0.0f);
                }
            }
            else
            {
                if (ControleY > 0)
                {
                    _LookingDirection = new Vector3(0.0f, 0.0f, 0.0f);
                }
                else
                {
                    _LookingDirection = new Vector3(0.0f, 180.0f, 0.0f);
                }

            }
            _Transform3DModel.localEulerAngles = _LookingDirection;

            Debug.DrawRay(transform.position, transform.TransformDirection(_MoveDir) * _MoveSpeed, Color.green);
            
           
            

            _JustStoppedMoving = true;
        }
        else if(_JustStoppedMoving)
        {
            _Rigidbody.velocity = Vector3.zero;
            _Rigidbody.angularVelocity = Vector3.zero;
            _JustStoppedMoving = false;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        _IsGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _IsGrounded = false;
    }

}
