using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private int _LeverId;
    [SerializeField]
    private Animator _Animator;
    private bool _IsOn = false, _PlayerIsNear = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_PlayerIsNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_IsOn)
                {
                    EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOn, _LeverId);
                }
                else
                {
                    EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOff, _LeverId);
                }
                _IsOn = !_IsOn;
                _Animator.SetBool("LeverOn", _IsOn);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
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
}
