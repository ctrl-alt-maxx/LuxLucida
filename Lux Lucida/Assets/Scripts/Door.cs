using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator _DoorAnimator;
    private bool _PlayerIsNear = false, _PlayerHasKey = false;
    private UnityAction<object> _DoesPlayerHaveKey;
    private Collider2D _DoorCollider, _DoorTrigger;  
    // Start is called before the first frame update
    void Start()
    {
        Collider2D[] colliderList = GetComponents<Collider2D>();
        _DoorCollider = (!colliderList[0].isTrigger) ? colliderList[0] : colliderList[1];
        _DoorTrigger = (colliderList[0].isTrigger) ? colliderList[0] : colliderList[1];
        _DoesPlayerHaveKey = DoesPlayerHaveKey;
        EventManager.StartListening(EventManager.PossibleEvent.eDoesPlayerHaveKey, _DoesPlayerHaveKey);
    }

    // Update is called once per frame
    void Update()
    {
        if( _PlayerIsNear)
        {
            if (Input.GetKeyDown(KeyCode.E)){
                if(_PlayerHasKey) {
                    _DoorAnimator.SetBool("Opened", true);
                    EventManager.TriggerEvent(EventManager.PossibleEvent.eUseKey, null);
                    _DoorCollider.enabled = false;
                    _DoorTrigger.enabled = false;
                }
                else
                {
                    EventManager.TriggerEvent(EventManager.PossibleEvent.eShowHint, "You can't open doors without a key.");
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eShowHint, "Press [ E ] to interact");
            _PlayerIsNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eHideHint, null);
            _PlayerIsNear = false;
        }
    }
    private void DoesPlayerHaveKey(object hasKey)
    {
        _PlayerHasKey = (bool)hasKey;
    }

    
}
