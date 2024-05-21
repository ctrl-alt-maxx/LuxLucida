using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReclinableCube : MonoBehaviour
{
    [SerializeField]
    private int _IdLever;
    private Animator _CubeAnimator;
    private Collider2D _CubeCollider;
    private UnityAction<object> _ReclineCube, _ForwardCube;

    // Start is called before the first frame update
    void Start()
    {
        _CubeAnimator = GetComponentInChildren<Animator>();
        _CubeCollider = GetComponentInChildren<Collider2D>();
        _ReclineCube = ReclineCube;
        _ForwardCube = ForwardCube;
        EventManager.StartListening(EventManager.PossibleEvent.eOnLeverOn, _ReclineCube);
        EventManager.StartListening(EventManager.PossibleEvent.eOnLeverOff, _ForwardCube);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ReclineCube(object id)
    {
        int idLever = (int)id;
        if (idLever == _IdLever)
        {
            _CubeAnimator.SetBool("Reclined", true);
            _CubeCollider.enabled = false;
        }
    }
    private void ForwardCube(object id)
    {
        int idLever = (int)id;
        if(idLever == _IdLever)
        {
            _CubeAnimator.SetBool("Reclined", false);
            _CubeCollider.enabled = true;
        }
    }
}
