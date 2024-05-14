using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using static Unity.VisualScripting.FlowStateWidget;

public class EventManagerLevelSelect : MonoBehaviour
{

    public enum PossibleEvent
    {
        eChangeTitle = 1,
        eChangeDescription = 2,
        eShowHUD =3,
        eHideHUD =4,    
        
        //////////////
        eMAX
    }

    private List<UnityEvent<object>> eventList;

    private static EventManagerLevelSelect eventManager;

    public static EventManagerLevelSelect instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManagerLevelSelect;

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