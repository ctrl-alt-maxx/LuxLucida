using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    public bool PlayerWasInZone = false, PlayerJustEnteredZone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D()
    {
        if (!PlayerWasInZone)
        {
            PlayerJustEnteredZone = true;
        }
        else
        {
            PlayerJustEnteredZone = false;
        }
        PlayerWasInZone = true;
    }

}
