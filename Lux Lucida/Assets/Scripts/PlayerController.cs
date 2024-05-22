using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    private enum InventorySpot
    {
        grenade = 0,
        rocketLaunch =1,
        glider = 2
    }
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
    private float jumpForce = 5, _MultiplierRocketLaunch = 1.5f;
    private InventorySpot _InventorySpot = 0;
    private UnityAction<object> _ChangeInventorySpot;
    private float _RocketLaunchCooldownLength = 2.0f, _RocketLaunchCooldownTime;
    // Start is called before the first frame update
    void Start()
    {
        _ChangeInventorySpot = ChangeInventorySpot;
        EventManager.StartListening(EventManager.PossibleEvent.eChangeInventorySpot, _ChangeInventorySpot);
        _Collider = GetComponent<CapsuleCollider2D>();
        _Rigidbody = GetComponent<Rigidbody2D>();

        _RocketLaunchCooldownTime = _RocketLaunchCooldownLength;
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


        switch (_InventorySpot)
        {
            case InventorySpot.grenade:
                UpdateGrenade();
                break;
            case InventorySpot.rocketLaunch:
                UpdateRocketLaunch();
                break;
            case InventorySpot.glider:
                UpdateGlider();
                break;
        }
    }

    private void UpdateGrenade()
    {

    }
    private void UpdateRocketLaunch()
    {
        if(_IsGrounded)
        {
            _RocketLaunchCooldownTime += Time.deltaTime;  
        }
        EventManager.TriggerEvent(EventManager.PossibleEvent.eUpdateRocketMeter, (_RocketLaunchCooldownTime / _RocketLaunchCooldownLength) * 100);

    }
    private void UpdateGlider()
    {

    }


    private void FixedUpdate()
    {
       
        _Rigidbody.velocity = new Vector2(controleX * ((_IsRunning) ? runningSpeed: walkingSpeed), _Rigidbody.velocity.y);

       
        CheckIfGrounded();

        bool canJump = true;

        if(_InventorySpot == InventorySpot.rocketLaunch)
        {
            canJump = (_RocketLaunchCooldownTime > _RocketLaunchCooldownLength);
        }

        if (_IsGrounded && Input.GetKey("space") && canJump)
        {
            bool isRocketLaunch = _InventorySpot == InventorySpot.rocketLaunch;
            float force = isRocketLaunch ? jumpForce * _MultiplierRocketLaunch : jumpForce;
            _Rigidbody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            _IsGrounded = false;
            _RocketLaunchCooldownTime = 0.0f;
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

    private void ChangeInventorySpot(object spot)
    {
        _InventorySpot = (InventorySpot)spot;
        
    }
}