using System.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _DialogueText;
    private UnityAction<object> _StartDialogue;
    private UnityAction<object> _CloseDialogue;
    private Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();

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
        _Animator.SetBool("Hidden", false);
    }
    private void CloseDialogue(object _)
    {
        Debug.Log("fgud");
        _Animator.SetBool("Hidden", true);
    }
}

