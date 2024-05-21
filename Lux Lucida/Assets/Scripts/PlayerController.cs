using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator _AnimatorPlayer;
    private Rigidbody2D _Rigidbody;
    private CapsuleCollider2D _Collider;

    private float controleX;
    private float controleY;
    private bool _IsGrounded, _IsRunning = false;

    [SerializeField]
    private float _DistanceRayCast = 0.5f;
    [SerializeField]
    private float walkingSpeed = 5, runningSpeed = 7;
    [SerializeField]
    private float jumpForce = 5;
    // Start is called before the first frame update
    void Start()
    {
        _Collider = GetComponent<CapsuleCollider2D>();
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        controleX = Input.GetAxis("Horizontal");
        controleY = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _IsRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _IsRunning = false;
        }

        _AnimatorPlayer.SetFloat("MouvementX", controleX);

        Vector2 direction = new Vector2(controleX, 0);
        float Vitesse = direction.magnitude;

        _AnimatorPlayer.SetFloat("Vitesse", Vitesse);
    }

    private void FixedUpdate()
    {
       
        _Rigidbody.velocity = new Vector2(controleX * ((_IsRunning) ? runningSpeed: walkingSpeed), _Rigidbody.velocity.y);

       
        CheckIfGrounded();
        
        if (_IsGrounded && Input.GetKey("space"))
        {
            _Rigidbody.AddForce(new Vector2(0, jumpForce));
            _IsGrounded = false;
        }
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Killzone"))
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eRestartLevel, null);
        }
    }
    private void CheckIfGrounded()
    {
        int layerMask = LayerMask.GetMask(new[] { "Ground" });
        Vector2 pointDebutRay = new Vector2(transform.position.x, _Collider.bounds.min.y);
        RaycastHit2D hit = Physics2D.Raycast(pointDebutRay , Vector2.down, _DistanceRayCast, layerMask);
        _IsGrounded = (hit.collider != null);
        Debug.DrawRay(pointDebutRay, Vector2.down * _DistanceRayCast, _IsGrounded ? Color.green : Color.red);
    }


}