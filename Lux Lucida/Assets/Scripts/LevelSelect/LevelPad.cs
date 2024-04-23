using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(Animator))]
public class LevelPad : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    [SerializeField]
    private Animator _spotLightAnimator;
    [SerializeField]
    private bool _touched = false, _Completed = false, _Unlocked = false;
    [SerializeField]
    private int _level = 0;

    void Start()
    {
        _animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    private void FixedUpdate()
    {
        _animator.SetBool("Lit", (_Unlocked || _Completed));
        if((_Unlocked || _Completed))
        {
            Debug.Log("deez");
        }
        _animator.SetBool("MoreLit", (_touched || _Completed));
        _spotLightAnimator.SetBool("Lit", _Completed);

    }
    private void OnCollisionEnter(Collision collision)
    {
        _touched = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _touched = false;
    }
}
