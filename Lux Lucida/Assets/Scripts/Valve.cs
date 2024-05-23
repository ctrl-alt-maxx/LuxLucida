using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Valve : MonoBehaviour
{
    [SerializeField]
    private int _ValveId;
    [SerializeField]
    private Animator _Animator;
    private bool _PlayerIsNear = false;

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
                EventManager.TriggerEvent(EventManager.PossibleEvent.eOnValveActivation, _ValveId);
                _Animator.SetTrigger("Activated");
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eShowHint, "[ E ] to activate valve "+ _ValveId.ToString());
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
