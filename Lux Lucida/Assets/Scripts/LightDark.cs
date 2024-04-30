using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDark : MonoBehaviour
{
    private Material _Material;
    // Start is called before the first frame update
    void Start()
    {
        _Material = gameObject.GetComponent<Renderer>().material;
        lightClose();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Renderer>().material = _Material;   
    }
    public void lightOpen()
    {
        _Material.SetFloat("_Saturation", 1);
    }
    public void lightClose()
    {
        _Material.SetFloat("_Saturation", 0);
    }
}
