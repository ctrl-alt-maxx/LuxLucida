using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameState _GameState;
    [SerializeField]
    private List<Image> _Images, _Icons;
    [SerializeField]
    private TMP_Text _KeyAmountText;
    private Animator _Animator;
    private int  _NInventorySpace = 4;
    public int _SelectedInventorySpace = 0;
    private float _LastActivityTime;
    [SerializeField]
    private float _InventoryShowTime = 5.0f;
    private bool _IsShown = false;
    private int _KeyAmount = 0;

    private UnityAction<object> _PickupKey, _UseKey;
    // Start is called before the first frame update
    void Start()
    {
        _PickupKey = Pickupkey;
        _UseKey = Usekey;
        
        EventManager.StartListening(EventManager.PossibleEvent.ePickupKey, _PickupKey);
        EventManager.StartListening(EventManager.PossibleEvent.eUseKey, _UseKey);
        _Animator = GetComponent<Animator>();
        if (_GameState.CurrentLevel < 2)
        {
            _Icons[0].gameObject.SetActive(false);
        }
        if (_GameState.CurrentLevel < 3)
        {
            _Icons[1].gameObject.SetActive(false);
        }
        if (_GameState.CurrentLevel < 4)
        {
            _Icons[2].gameObject.SetActive(false);
        }
        UpdateKeyAmountText();
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
                _Images[i].color = new Color(0.5f,0.5f,0.5f, 1.0f);
            }
        }
        if(Input.mouseScrollDelta.y < -0.001)
        {
            if (_SelectedInventorySpace < _NInventorySpace - 1) {
                _SelectedInventorySpace++;
                ShowInventory();
                EventManager.TriggerEvent(EventManager.PossibleEvent.eChangeInventorySpot, _SelectedInventorySpace);    
            }
            
        }
        else if (Input.mouseScrollDelta.y > 0.001)
        {
            if (_SelectedInventorySpace > 0) {
                _SelectedInventorySpace--;
                ShowInventory();
                EventManager.TriggerEvent(EventManager.PossibleEvent.eChangeInventorySpot, _SelectedInventorySpace);
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

    private void UpdateKeyAmountText()
    {
        _KeyAmountText.text = "x " + _KeyAmount.ToString();
    }

    private void Pickupkey(object _)
    {
        _KeyAmount++;
        UpdateKeyAmountText();
        EventManager.TriggerEvent(EventManager.PossibleEvent.eDoesPlayerHaveKey, true);
    }
    private void Usekey(object _)
    { 
        _KeyAmount--;
        UpdateKeyAmountText();
        if(_KeyAmount == 0)
        {
            EventManager.TriggerEvent(EventManager.PossibleEvent.eDoesPlayerHaveKey, false);
        }
    }
}
