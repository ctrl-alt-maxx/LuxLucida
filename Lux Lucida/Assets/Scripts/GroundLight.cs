using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GroundLight : MonoBehaviour
{
    private Animator _Animator;
    private GameController _GameController;
    private LightDark _ObjectScript;
    public bool IsLit = false;
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
    public void LightUp()
    {
        _Animator.SetBool("Lit", true);
        _GameController.LitLightCount++;
        IsLit = true;
    }
    public void DarkenUp()
    {
        _Animator.SetBool("Lit", false);
        _GameController.LitLightCount--;
        IsLit = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer)  == "LightActivator" && !IsLit) {
            LightUp();
            _ObjectScript.lightOpen();
        }

    }
   
}
