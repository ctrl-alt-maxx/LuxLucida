using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class FinalPuzzle : MonoBehaviour
{
    private enum Color
    {
        yellow = 1,
        red = 2,
        blue = 3,
        green = 4 
    }
    private UnityAction<object> _ValveActivated, _LeverOn, _LeverOff;
    private List<Color> _Solution = new List<Color>();
    private List<Color> _AttemptedSolution = new List<Color>();
    private bool _Solved = false, _DialogueIsShowing = false;
    // Start is called before the first frame update
    void Start()
    {
        _Solution.Add(Color.yellow);
        _Solution.Add(Color.blue);
        _Solution.Add(Color.red);
        _Solution.Add(Color.green);
        _ValveActivated = ValveActivated;
        _LeverOn = LeverOn;
        EventManager.StartListening(EventManager.PossibleEvent.eOnValveActivation, _ValveActivated);
        EventManager.StartListening(EventManager.PossibleEvent.eOnLeverOn, _LeverOn);
 
    }

    // Update is called once per frame
    void Update()
    {
        if(_Solved && _DialogueIsShowing && Input.GetKeyDown(KeyCode.C)){
            EventManager.TriggerEvent(EventManager.PossibleEvent.eCloseDialogue, null);
        }
    }

    private void ValveActivated(object idObject)
    {
        
        if(_AttemptedSolution.Count < 4)
        {
            _AttemptedSolution.Add((Color)idObject);
        }
        else
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eShowHint, "The code only takes 4 values");
        }

    }

    private void LeverOn(object idObject)
    {
        int id = (int)idObject;
        if(id == 1)
        {
            if(!_Solved)
            {
                _Solved = _AttemptedSolution.Count == 4;

                for (int i = 0; i < _AttemptedSolution.Count; i++)
                {
                    if (_Solution[i] != _AttemptedSolution[i])
                    {
                        _Solved = false;
                    }
                }
                _AttemptedSolution = new List<Color>();
                if (_Solved)
                {
                    OnPuzzleSolved();
                }
            }
        }
    }
    private void OnPuzzleSolved()
    {
        EventManager.TriggerEvent(EventManager.PossibleEvent.eStartDialogue, "The gods have rewarded your dedication with great power.\n\nYou now have chroma's touch.\n\nThis means you have unlimited Eye Power.\n\n It's time to finish what you started.\n\nPress [ C ] to close dialogue.");
        EventManager.TriggerEvent(EventManager.PossibleEvent.eChromasTouch, null);
        _DialogueIsShowing = true;
    }
}

