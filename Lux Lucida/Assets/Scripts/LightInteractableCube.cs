using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightInteractableCube : MonoBehaviour
{

    private GroundLight _Light;
    [SerializeField]
    private int _Id;
    private bool _Activated = false;



    // Start is called before the first frame update
    void Start()
    {
        _Light = GetComponentInChildren<GroundLight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_Activated)
        {
            if (_Light.IsLit)
            {
                EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOn, _Id);
                _Activated = true;  
            }
        }
    }
}
