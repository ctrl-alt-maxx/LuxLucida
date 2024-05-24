using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindTileMap : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        foreach(Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
