using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDark : MonoBehaviour
{
    private Material _Material;
    private GroundLight _Light;
    // Start is called before the first frame update
    void Start()
    {
        _Material = gameObject.GetComponent<Renderer>().material;
        _Light = GetComponentInChildren<GroundLight>(); 
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ennemi"))
        {
            if (_Light.IsLit)
            {
                _Light.DarkenUp();
                lightClose();
            }
        }
    }
}
