using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TutorialController : MonoBehaviour
{
    private enum TutorialStep
    {
        PlayerMove = 0,
        PlayerJump = 1,
        EyesOfRa = 2,
        InteractableObject = 3,
    }
    private TutorialStep _TutorialStep = TutorialStep.PlayerMove;
    private bool _FirstUpdate = true;   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_FirstUpdate)
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eStartDialogue, "Press a, d to move the player left to right\nPress C to close dialogue");
            _FirstUpdate = false;
        }

        switch (_TutorialStep)
        {
            case TutorialStep.PlayerMove:
                PlayerMoveUpdate();
                break;
            case TutorialStep.PlayerJump:
                PlayerJumpUpdate();
                break;
            case TutorialStep.EyesOfRa:
                EyesOfRaUpdate();
                break;
            case TutorialStep.InteractableObject:
                InteractableObjectUpdate();
                break;
        }
    }

    private void PlayerMoveUpdate()
    {

    }
    private void PlayerJumpUpdate()
    {

    }
    private void EyesOfRaUpdate()
    {

    }
    private void InteractableObjectUpdate()
    {

    }
}
