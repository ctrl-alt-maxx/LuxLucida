using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZone : MonoBehaviour
{
    public bool PlayerIsInZone = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D()
    {
        PlayerIsInZone = true;
    }
    private void OnTriggerExit2D()
    {
        PlayerIsInZone = false;
    }
}
