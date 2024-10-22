using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{

    public enum PossibleEvent
    {
        eStartDialogue = 0,
        eCloseDialogue = 1,
        eRestartLevel = 2,
        eOnLeverOn = 3,
        eOnLeverOff = 4,
        eShowHint = 5,
        eHideHint = 6,
        ePickupKey = 7,
        eUseKey = 8,
        eDoesPlayerHaveKey = 9,
        eLoseBattery = 10,
        eChangeInventorySpot = 11,
        eUpdateRocketMeter = 12,
        eGrenadeUsed = 13,
        eOnValveActivation = 14,
        eChromasTouch = 15,
        ePickupFirefly = 16,
        eEyeOfRaActivation = 17,
        eEyeOfRaDeactivation = 18,
        //////////////
        eMAX
    }

    private List<UnityEvent<object>> eventList;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventList == null)
        {
            eventList = new List<UnityEvent<object>>();
            for (int i = 0; i < (int)PossibleEvent.eMAX; i++)
            {
                eventList.Add(new UnityEvent<object>());
            }
        }
    }

    public static void StartListening(PossibleEvent eventEnum, UnityAction<object> listener)
    {
        instance.eventList[(int)eventEnum].AddListener(listener);
    }

    public static void StopListening(PossibleEvent eventEnum, UnityAction<object> listener)
    {
        instance.eventList[(int)eventEnum].RemoveListener(listener);
    }

    public static void TriggerEvent(PossibleEvent eventEnum, object data)
    {
        instance.eventList[(int)eventEnum].Invoke(data);
    }
}