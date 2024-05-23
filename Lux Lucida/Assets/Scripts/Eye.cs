using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Eye : MonoBehaviour
{
    
    private Material _Material;
    private UnityAction<object> _Open, _Close;
   
    // Start is called before the first frame update
    void Start()
    {
        _Material = gameObject.GetComponent<Renderer>().material;
        _Open = Open;
        _Close = Close;
        
        EventManager.StartListening(EventManager.PossibleEvent.eEyeOfRaActivation, _Open);
        EventManager.StartListening(EventManager.PossibleEvent.eEyeOfRaDeactivation, _Close);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().material = _Material;
    }
    private void Open(object _) {
        _Material.SetColor("_Color", new Color(1.0f, 1.0f, 1.0f));
    }
    private void Close(object _) {
        _Material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f));
    }
}
