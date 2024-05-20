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
        LevelProgress = 4,
    }
    private TutorialStep _TutorialStep = TutorialStep.PlayerMove;
    private bool _FirstUpdate = true;
    private GameController _GameController;

    [SerializeField]
    private TutorialZone ZoneEyesOfRa, ZonePlayerJump;
    // Start is called before the first frame update
    void Start()
    {
        _GameController = gameObject.GetComponent<GameController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_FirstUpdate)
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eStartDialogue, "Press the [ A ] or [ D ] keys to walk left to right");
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
            case TutorialStep.LevelProgress:
                LevelProgressUpdate();
                break;
            case TutorialStep.InteractableObject:
                InteractableObjectUpdate();
                break;
        }
    }

    private void PlayerMoveUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eCloseDialogue, null);
            _TutorialStep = TutorialStep.PlayerJump;
        }
    }
    private void PlayerJumpUpdate()
    {
        if (ZonePlayerJump.PlayerWasInZone)
        {
            if (ZonePlayerJump.PlayerJustEnteredZone)
            {
                EventManager.TriggerEvent(EventManager.PossibleEvent.eStartDialogue, "Press the [ SPACEBAR ] to jump.");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventManager.TriggerEvent(EventManager.PossibleEvent.eCloseDialogue, null);
                _TutorialStep = TutorialStep.EyesOfRa;
            }
        }

    }
    private void EyesOfRaUpdate()
    {
        if (ZoneEyesOfRa.PlayerWasInZone)
        {
            if (ZoneEyesOfRa.PlayerJustEnteredZone)
            {
                string dialogueText = "You have the Eyes of Ra.\n They are a powerfull light source that you will have to use to complete your quest in lighting up the globe.\n Press [ Q ] to activate/deactivate your glowing eyes.";
                EventManager.TriggerEvent(EventManager.PossibleEvent.eStartDialogue, dialogueText);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                EventManager.TriggerEvent(EventManager.PossibleEvent.eCloseDialogue, null);
                _TutorialStep = TutorialStep.LevelProgress;
            }
            
        }
        
        
    }

    private void LevelProgressUpdate()
    {   
        if (_GameController.PercentProgress > 25)
        {
            string dialogueText = "To complete the level you need to light up 100% of the ground and objects that are darkened in the level.\n The bar in the top will help you keep track how much more there is to do in the level. \n\n But be carefull! Your powers are limited\n In the bottom right of the screen you can see how much energy you have left in your eyes.\n\nHold [ R ] to restart level.";
            _TutorialStep = TutorialStep.LevelProgress;
            EventManager.TriggerEvent(EventManager.PossibleEvent.eStartDialogue, dialogueText);

        }
    }
    private void InteractableObjectUpdate()
    {

    }
}
