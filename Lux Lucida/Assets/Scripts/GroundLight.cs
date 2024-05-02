using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GroundLight : MonoBehaviour
{
    private Animator _Animator;
    private GameController _GameController;
    private LightDark _ObjectScript;
    private bool _IsLit = false;
    private Material _ParentMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _ObjectScript = gameObject.transform.parent.GetComponent<LightDark>();
        
        _GameController = GameObject.Find("GameController").GetComponent<GameController>();
        _GameController.TotalLightCount++;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer)  == "LightActivator" && !_IsLit) {
            _Animator.SetBool("Lit", true);
            _GameController.LitLightCount++;
            _IsLit = true;
            _ObjectScript.lightOpen();
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "LightActivator" && _IsLit)
        {
            //_Animator.SetBool("Lit", false);
        }
    }
}
