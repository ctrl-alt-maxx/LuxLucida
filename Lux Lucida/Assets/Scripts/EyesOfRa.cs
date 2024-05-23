using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class EyesOfRa : MonoBehaviour
{
    [SerializeField]
    private Slider _BatterySlider;

    [SerializeField]
    private float _MaxBatteryValue = 10.0f;
    [SerializeField]
    private float _CurrentBatteryValue;
    [SerializeField]
    private GameObject _EyesOfRaObject;
    [SerializeField]
    private TMP_Text _BatteryText;
    private Animator _ZoneAnimator;
    private Image _BackgroundPanel;
    private bool _IsActive=false, _ChromasTouchActivated = false;
    private UnityAction<object> _LoseBattery, _ChromasTouchActivation;
    // Start is called before the first frame update
    void Start()
    {
        _CurrentBatteryValue = _MaxBatteryValue;
        _BatterySlider.minValue = 0.0f;
        _BatterySlider.maxValue = _MaxBatteryValue;
        _BackgroundPanel = GetComponent<Image>();
        _ZoneAnimator = _EyesOfRaObject.GetComponent<Animator>();

        _LoseBattery = LoseBattery;
        EventManager.StartListening(EventManager.PossibleEvent.eLoseBattery, _LoseBattery);
        _ChromasTouchActivation = ChromasTouchActivation;
        EventManager.StartListening(EventManager.PossibleEvent.eChromasTouch, _ChromasTouchActivation);

    }

    // Update is called once per frame
    void Update()
    {
        _BatterySlider.value = _CurrentBatteryValue;
        if(!_ChromasTouchActivated)
        {
            _BatteryText.text = (Mathf.Round((_CurrentBatteryValue / _MaxBatteryValue) * 100)).ToString() + "% Eye Power";
        }
        else
        {
            _BatteryText.text = "Chroma's touch";
        }
        if (_IsActive)
        {
            if(!_ChromasTouchActivated)
            {
                _CurrentBatteryValue -= Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _IsActive = !_IsActive;
            if(_IsActive)
            {
                TurnOn();
            }
            else
            {
                TurnOff();
            }
        }
    }
    

    public void TurnOn()
    {
        _ZoneAnimator.SetBool("Opened", true);
        _BackgroundPanel.color = new Color(1, 1, 1, (147.0f / 255.0f));

    }
    public void TurnOff()
    {
        _ZoneAnimator.SetBool("Opened", false);
        _BackgroundPanel.color = new Color(0.15f, 0.15f, 0.15f, (147.0f / 255.0f));
    }
    public void LoseBattery(object value)
    {
        if (!_ChromasTouchActivated)
        {
            float batteryLost = (float)value;
            _CurrentBatteryValue -= batteryLost;
        }
    }
    private void ChromasTouchActivation(object _)
    {
        _ChromasTouchActivated = true;
        _CurrentBatteryValue = _MaxBatteryValue;
    }
}
