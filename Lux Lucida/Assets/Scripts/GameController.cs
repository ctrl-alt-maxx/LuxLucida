using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Tilemap _GameTilemap;
    [SerializeField]
    private TMP_Text _LevelProgressText;
    [SerializeField]
    private GameObject _LevelProgressSliderObject;
    private Slider _LevelProgressSlider;
    [SerializeField]
    private Light _MainDirLight;
    
    [SerializeField]
    private float _MaxLightValue=3;
    [SerializeField]
    private Inventory _Inventory;
    [SerializeField]
    public int TotalLightCount = 0, LitLightCount = 0, PercentProgress = 0;
    [SerializeField]
    private GameState _GameState;

    private UnityAction<object> _RestartLevel;
    private float _TimeR;


    // Start is called before the first frame update
    void Start()
    {
        _LevelProgressSlider = _LevelProgressSliderObject.GetComponent<Slider>();
        _RestartLevel = RestartLevel;
        EventManager.StartListening(EventManager.PossibleEvent.eRestartLevel, _RestartLevel);   
    }

    // Update is called once per frame
    void Update()
    {
  
        
        if (TotalLightCount > 0)
        {
            PercentProgress = (int)(((float)LitLightCount / (float)TotalLightCount) * 100.0f);
        }

        UpdateLightProgressHUD(PercentProgress);

        if(Input.GetKey(KeyCode.R))
        {
            if(Input.GetKeyDown(KeyCode.R)) {
                _TimeR = 0;
            }
            _TimeR += Time.deltaTime;
            if(_TimeR > 2) {
                EventManager.TriggerEvent(EventManager.PossibleEvent.eRestartLevel, null);
            }
        }

        if (PercentProgress >= 100)
        {
            _GameState.CurrentLevel++;
            _GameState.Save();
            SceneManager.LoadScene("SelectScene");
        }
    }
    public void UpdateLightProgressHUD(int percent)
    {
        _LevelProgressText.text = "Level is " + (percent).ToString() + "% lit";
        _MainDirLight.intensity = (float)(((percent / 100.0f)) * (_MaxLightValue * 1.33f)) - _MaxLightValue / 3.00f;
        _LevelProgressSlider.value = percent;
        
    }

    private void RestartLevel(object _)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
