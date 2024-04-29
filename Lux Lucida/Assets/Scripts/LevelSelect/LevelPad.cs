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
    private bool _Touched = false, _Completed = false, _Unlocked = false;
    [SerializeField]
    private int _level = 0;
    [SerializeField]
    private levelController _LevelController;

    void Start()
    {
        _animator = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        int lastUnlockedLevel = _LevelController._LastUnlockedLevel;
        if(_level <= lastUnlockedLevel)
        {
            _Unlocked = true;
        }
        if(_level < lastUnlockedLevel) { 
            _Completed = true;  
        }
    }
    private void FixedUpdate()
    {
        _animator.SetBool("Lit", (_Unlocked || _Completed));
        if((_Unlocked || _Completed))
        {
            Debug.Log("deez");
        }
        _animator.SetBool("MoreLit", (_Touched || _Completed));
        _spotLightAnimator.SetBool("Lit", _Completed);

    }
    private void OnCollisionEnter(Collision collision)
    {
        _Touched = true;
        if(_Unlocked)
        {
            _LevelController.SelectLevel(_level);
            _LevelController.ShowHUD();
        }
    }
    private void OnCollisionStay(Collision collision) {
        if (_Unlocked && Input.GetKeyDown(KeyCode.E))
        {
           
           
           _LevelController.StartLevel(_level);
           
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        _Touched = false;
        if (_Unlocked)
        {
            _LevelController.HideHUD();
        }
    }
    
}
