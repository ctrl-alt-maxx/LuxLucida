using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class RocketMeter : MonoBehaviour
{
    private UnityAction<object> _UpdateMeter;
    private Slider _Slider;
    private Image[] _ChildImages;
    private float _Value = 100.0f;
    private bool _IsShown = false;
    
    // Start is called before the first frame update
   void Start()
    {
        _ChildImages = GetComponentsInChildren<Image>();    
        _UpdateMeter = UpdateMeter;
        EventManager.StartListening(EventManager.PossibleEvent.eUpdateRocketMeter, _UpdateMeter);
        _Slider = GetComponent<Slider>();   

    }

    // Update is called once per frame
    void Update()
    {
        if(_IsShown)
        {
            _Slider.value = _Value;
            if(_Value == 100.0f)
            {
                _IsShown = false;
                HideChildImages();
            }
        }
        else
        {
            if(_Value < 100.0f)
            {
                _IsShown = true;
                ShowChildImages();
            }
        }
    }

    private void HideChildImages()
    {
        foreach(Image image in _ChildImages)
        {
            image.enabled = false;
        }
    }
    private void ShowChildImages()
    {
        foreach (Image image in _ChildImages)
        {
            image.enabled = true;
        }
    }

   
    private void UpdateMeter(object value)
    {
        float val = (float)value;
        if (val > 100)
        {
            val = 100;
        }
        _Value = val;
    }
}
