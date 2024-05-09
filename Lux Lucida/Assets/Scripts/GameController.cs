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
    public int TotalLightCount = 0, LitLightCount = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        _LevelProgressSlider = _LevelProgressSliderObject.GetComponent<Slider>();
        EventManager.TriggerEvent(EventManager.PossibleEvent.eStartDialogue, "Press a, d to move the player left to right\nPress C to close dialogue");
    }

    // Update is called once per frame
    void Update()
    {

        int percentProgress = 0;
        if (TotalLightCount > 0)
        {
            percentProgress = (int)(((float)LitLightCount / (float)TotalLightCount) * 100.0f);
        }
        UpdateLightProgressHUD(percentProgress);



        if ( percentProgress>= 100)
        {
            //GameObject.Find("LevelController").GetComponent<levelController>().LastUnlockedLevel = 2;
            SceneManager.LoadScene("SelectScene");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("c pressed");
            EventManager.TriggerEvent(EventManager.PossibleEvent.eCloseDialogue, null);
        }
    }
    public void UpdateLightProgressHUD(int percent)
    {
        _LevelProgressText.text = "Level is " + (percent).ToString() + "% lit";
        _MainDirLight.intensity = (float)(((percent / 100.0f)) * (_MaxLightValue * 1.33f)) - _MaxLightValue / 3.00f;
        _LevelProgressSlider.value = percent;
        
    }

}
