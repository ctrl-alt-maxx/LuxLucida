using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class GrenadeHUDText : MonoBehaviour
{
    private int _GrenadeCount = 3;
    private TMP_Text _Text;
    private UnityAction<object> _GrenadeUsed;
    // Start is called before the first frame update
    void Start()
    {
        _GrenadeUsed = GrenadeUsed;
        EventManager.StartListening(EventManager.PossibleEvent.eGrenadeUsed, _GrenadeUsed);
        _Text = GetComponent<TMP_Text>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateText()
    {
        _Text.text = _GrenadeCount.ToString();
    }
    private void GrenadeUsed(object _)
    {
        _GrenadeCount--;
        UpdateText();
    }
}
