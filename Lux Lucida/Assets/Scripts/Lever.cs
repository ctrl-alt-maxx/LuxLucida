using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField]
    private int _LeverId;
    private bool _IsOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventManager.TriggerEvent(EventManager.PossibleEvent.eShowHint, "Press [ E ] to interact with lever");
        Debug.Log("enter");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {   
                if(!_IsOn) {
                    EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOn, _LeverId);
                }
                else
                {
                    EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOff, _LeverId);
                }
                _IsOn = !_IsOn;
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        EventManager.TriggerEvent(EventManager.PossibleEvent.eShowHint, null);
    }
}
