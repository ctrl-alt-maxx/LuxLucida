using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class OptionsController : MonoBehaviour
{
    
    [SerializeField]
    private TMP_Text _TextGraphics;
    [SerializeField]
    private TMP_Text _TextFullscreen;
    [SerializeField]
    private RenderPipelineAsset[] _QualityLevels;
    private RenderPipelineAsset _QualityLevel;
    private int _QualityLevelIndex;
    private bool _IsFullscreen = true;
    // Start is called before the first frame update
    private void Start()
    {
        _QualityLevelIndex = QualitySettings.GetQualityLevel();
        _QualityLevel = _QualityLevels[_QualityLevelIndex];
        string qualityText = "Graphics Quality : " + _QualityLevel.name.Split("_")[0];
    }
    // Update is called once per frame
    public void ChangeGraphics()
    {
        _QualityLevelIndex = (_QualityLevelIndex + 1) % 6;
        
        _QualityLevel = _QualityLevels[_QualityLevelIndex];
        string qualityText  = "Graphics Quality : " + _QualityLevel.name.Split("_")[0]; 
        _TextGraphics.SetText(qualityText);
        QualitySettings.SetQualityLevel(_QualityLevelIndex);
    }
    public void ChangeFullscreen() { 
        _IsFullscreen = !_IsFullscreen;
        Screen.fullScreen = _IsFullscreen;
        string modeText = _IsFullscreen ? "Fullscreen" : "Windowed";
        _TextFullscreen.SetText("Fullscreen mode : "  + modeText);  
    }
}
