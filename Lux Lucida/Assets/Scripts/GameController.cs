using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
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
    private bool _FirstUpdate = true;


    // Start is called before the first frame update
    void Start()
    {
        _LevelProgressSlider = _LevelProgressSliderObject.GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_FirstUpdate)
        {
            
        }
        
        
        if (TotalLightCount > 0)
        {
            PercentProgress = (int)(((float)LitLightCount / (float)TotalLightCount) * 100.0f);
        }
        UpdateLightProgressHUD(PercentProgress);



        if (PercentProgress >= 100)
        {
            _GameState.NextLevel++;
            SceneManager.LoadScene("SelectScene");
        }

        
        _FirstUpdate = false;
    }
    public void UpdateLightProgressHUD(int percent)
    {
        _LevelProgressText.text = "Level is " + (percent).ToString() + "% lit";
        _MainDirLight.intensity = (float)(((percent / 100.0f)) * (_MaxLightValue * 1.33f)) - _MaxLightValue / 3.00f;
        _LevelProgressSlider.value = percent;
        
    }

}
