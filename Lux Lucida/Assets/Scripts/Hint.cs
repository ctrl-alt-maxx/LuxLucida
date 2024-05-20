using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(TMP_Text))]
public class Hint : MonoBehaviour
{
    private Animator _Animator;
    private TMP_Text _Text;
    private UnityAction<object> _ShowHint, _HideHint;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();   
        _Text = GetComponent<TMP_Text>();
        _ShowHint = ShowHint;
        _HideHint = HideHint;
        EventManager.StartListening(EventManager.PossibleEvent.eShowHint, _ShowHint);
        EventManager.StartListening(EventManager.PossibleEvent.eHideHint, _HideHint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowHint(object text)
    {
        string hintText = (string)text;
        _Text.text = hintText;
        _Animator.SetBool("Hidden", false);
    }
    private void HideHint(object text)
    {
        _Animator.SetBool("Hidden", true);
    }
}

