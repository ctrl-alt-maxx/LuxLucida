using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatingPlatformManager : MonoBehaviour
{

    private IEnumerator _Alternate;

    // Start is called before the first frame update
    void Start()
    {
        _Alternate = Alternate();
        StartCoroutine(_Alternate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Alternate()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOff, 1);
            yield return new WaitForSeconds(0.5f);
            EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOn, 2);
            yield return new WaitForSeconds(2.0f);
            EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOff, 2);
            yield return new WaitForSeconds(0.5f);
            EventManager.TriggerEvent(EventManager.PossibleEvent.eOnLeverOn, 1);
        }
    }
}
