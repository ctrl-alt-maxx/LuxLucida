using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _DialogueText;
    private UnityAction<object> _StartDialogue;
    private UnityAction<object> _CloseDialogue;
    // Start is called before the first frame update
    void Start()
    {
        _StartDialogue = StartDialogue;
        EventManager.StartListening(EventManager.PossibleEvent.eStartDialogue, _StartDialogue);
        _CloseDialogue = CloseDialogue;
        EventManager.StartListening(EventManager.PossibleEvent.eCloseDialogue, _CloseDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartDialogue(object dialogue)
    {
        _DialogueText.text = dialogue.ToString();
        this.enabled = true;
    }
    private void CloseDialogue(object _)
    {
        Debug.Log("c pressed");
        this.enabled = false;
    }
}

