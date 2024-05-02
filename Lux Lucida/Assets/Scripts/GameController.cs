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

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Tilemap _GameTilemap;
    [SerializeField]
    private TMP_Text _LevelProgressText;
    [SerializeField]
    private Light _MainDirLight;

    [SerializeField]
    private float _MaxLightValue=3;
    public int TotalLightCount = 0, LitLightCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        int percentProgress = 0;
        if (TotalLightCount > 0)
        {
            percentProgress = (int)(((float)LitLightCount / (float)TotalLightCount) * 100.0f);
        }
        _LevelProgressText.text = "Level is " + (percentProgress).ToString() + "% lit";
        _MainDirLight.intensity = (float)(((percentProgress/100.0f)) * (_MaxLightValue *1.33f)) - _MaxLightValue / 3.00f;

        if ( percentProgress>= 100)
        {
            GameObject.Find("LevelController").GetComponent<levelController>().LastUnlockedLevel = 2;
            SceneManager.LoadScene("SelectScene");
        }
    }

}
