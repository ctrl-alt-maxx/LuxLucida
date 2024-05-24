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
        glider = 2,
        emptyHand = 3
    }
    [SerializeField]
    private Animator _AnimatorPlayer;
    private Rigidbody2D _Rigidbody;
    private CapsuleCollider2D _Collider;

    private float controleX;
    private float controleY;
    private bool _IsGrounded, _IsRunning, _IsFalling = false;
    [SerializeField]
    private GameState _GameState;
    [SerializeField]
    private float _DistanceRayCast = 0.5f;
    [SerializeField]
    private float walkingSpeed = 5, runningSpeed = 7;
    [SerializeField]
    private float jumpForce = 5, _MultiplierRocketLaunch = 1.5f;
    [SerializeField]
    private float _GrenadeLaunchForce = 5.0f;
    [SerializeField]
    private GameObject _ExemplaireGrenade;
    [SerializeField]
    private GameObject _Hand, _LeftBoot, _RightBoot;
    [SerializeField]
    private float _GliderGraviyScale;
    private InventorySpot _InventorySpot = 0;
    private UnityAction<object> _ChangeInventorySpot;
    private float _RocketLaunchCooldownLength = 1.0f, _RocketLaunchCooldownTime;
    private bool _CanFireGrenades = true, _CanRocketLauch = true, _CanGlide = true;
    private int _GrenadeCount = 3;
    // Start is called before the first frame update
    void Start()
    {
        _ChangeInventorySpot = ChangeInventorySpot;
        EventManager.StartListening(EventManager.PossibleEvent.eChangeInventorySpot, _ChangeInventorySpot);
        _Collider = GetComponent<CapsuleCollider2D>();
        _Rigidbody = GetComponent<Rigidbody2D>();
        if(_GameState.CurrentLevel < 2)
        {
            _CanFireGrenades = false;
        }
        else
        {
            EnterGrenade();
        }
        if(_GameState.CurrentLevel < 3)
        {

            _CanRocketLauch = false;
        }
        if (_GameState.CurrentLevel < 4)
        {

            _CanGlide = false;
        }
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
        _AnimatorPlayer.SetFloat("Height", _Rigidbody.velocity.y);

        _AnimatorPlayer.SetBool("Running", _IsRunning);

        if (_IsGrounded && _IsFalling)
        {
            _AnimatorPlayer.SetTrigger("Landed");
            _IsFalling = false;
        }


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
    private void EnterGrenade()
    {
        if(_CanFireGrenades) {
            _Hand.SetActive(true);
        }
        
    }
    private void EnterRocketLaunch()
    {
        _LeftBoot.SetActive(true);
        _RightBoot.SetActive(true);
    }
    private void EnterGlider()
    {
        if (_CanGlide)
        {
            _AnimatorPlayer.SetBool("GliderEquiped", true);
        }
    }
       
    private void UpdateGrenade()
    {
        if (_CanFireGrenades)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z; ;
            Vector2 direction = (Camera.main.ScreenToWorldPoint(mousePos) - _Hand.transform.position);
            direction.Normalize();
            
            if (Input.GetMouseButtonDown(0))
            {
                GameObject grenade = Instantiate(_ExemplaireGrenade, _Hand.transform.position, Quaternion.identity);
                grenade.GetComponent<Rigidbody2D>().AddForce(direction * _GrenadeLaunchForce, ForceMode2D.Impulse);
                _GrenadeCount--;
                EventManager.TriggerEvent(EventManager.PossibleEvent.eGrenadeUsed, null);
                if(_GrenadeCount == 0)
                {
                    _CanFireGrenades = false;
                }
            }
        }
       
        

    }
    private void UpdateRocketLaunch()
    {
        if (_CanRocketLauch)
        {
            if (_IsGrounded)
            {
                _RocketLaunchCooldownTime += Time.deltaTime;
            }
            EventManager.TriggerEvent(EventManager.PossibleEvent.eUpdateRocketMeter, (_RocketLaunchCooldownTime / _RocketLaunchCooldownLength) * 100);
        }
    }
    private void UpdateGlider()
    {
        if (_CanGlide)
        {
            if (!_IsGrounded && _Rigidbody.velocity.y < 0)
            {
                _Rigidbody.gravityScale = _GliderGraviyScale;
            }
            else
            {
                _Rigidbody.gravityScale = 2.0f;
            }
        }
       
    }


    private void FixedUpdate()
    {
       
        _Rigidbody.velocity = new Vector2(controleX * ((_IsRunning) ? runningSpeed: walkingSpeed), _Rigidbody.velocity.y);

       
        CheckIfGrounded();

        bool canJump = true;

        if(_InventorySpot == InventorySpot.rocketLaunch && _CanRocketLauch)
        {
            canJump = (_RocketLaunchCooldownTime > _RocketLaunchCooldownLength);
        }

        if (_IsGrounded && Input.GetKey("space") && canJump)
        {
            if (!_IsFalling)
            {
                _AnimatorPlayer.SetTrigger("Jump");
                _IsFalling = true;
            }
       
            
            bool isRocketLaunch = _InventorySpot == InventorySpot.rocketLaunch && _CanRocketLauch;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Head"))
        {
            _Rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); 
            _AnimatorPlayer.SetTrigger("Flip");
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
        switch (_InventorySpot)
        {
            case InventorySpot.grenade:
                EnterGrenade();
                break;
            case InventorySpot.glider:
                EnterGlider();
                break;
            case InventorySpot.rocketLaunch:
                EnterRocketLaunch();
                break;  
        }
        if(_InventorySpot != InventorySpot.grenade)
        {
            _Hand.SetActive(false);
        }
        if (_InventorySpot != InventorySpot.rocketLaunch)
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eUpdateRocketMeter, 100.0f);
            _LeftBoot.SetActive(false);
            _RightBoot.SetActive(false);
        }
        if(_InventorySpot != InventorySpot.glider)
        {
            _Rigidbody.gravityScale = 2.0f;
            _AnimatorPlayer.SetBool("GliderEquiped", false);
        }
    }


}