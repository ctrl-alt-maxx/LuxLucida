using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<Image> _Images;
    private Animator _Animator;
    private int  _NInventorySpace = 3;
    public int _SelectedInventorySpace = 0;
    private float _LastActivityTime;
    [SerializeField]
    private float _InventoryShowTime = 5.0f;
    private bool _IsShown = false;
    // Start is called before the first frame update
    void Start()
    {
        _Animator =GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _NInventorySpace; i++)
        {
            if (i == _SelectedInventorySpace)
            {
                _Images[i].color = new Color(1.0f, 1.0f, 1.0f ,1.0f);   
            }
            else
            {
                _Images[i].color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }
        if(Input.mouseScrollDelta.y < -0.001)
        {
            if (_SelectedInventorySpace < _NInventorySpace - 1) {
                _SelectedInventorySpace++;
                ShowInventory();

            }
            
        }
        else if (Input.mouseScrollDelta.y > 0.001)
        {
            if (_SelectedInventorySpace > 0)
            {
                _SelectedInventorySpace--;
                ShowInventory();
            }
            
        }

        if (Time.time - _LastActivityTime > _InventoryShowTime && _IsShown)
        {
            HideInventory();
        }
    }

    private void ShowInventory() {
        if(_IsShown == false) {
            _Animator.SetTrigger("Show");
        }
        _LastActivityTime = Time.time;
        _IsShown = true;
    }
    private void HideInventory()
    {
        _Animator.SetTrigger("Hide");
        _IsShown = false;
    }
}
