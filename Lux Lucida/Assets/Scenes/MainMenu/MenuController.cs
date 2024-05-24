using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(AudioSource))]
public class MenuController : MonoBehaviour
{

    [SerializeField]
    private Animator _GlobeAnimator;
    [SerializeField]
    private Canvas _MainCanvas, _AvatarCanvas, _OptionsCanvas;
    [SerializeField]
    private GameState _GameState;
    [SerializeField]
    private Slider _MusicSlider;
    private AudioSource _AudioSource;

    private void Start()
    {
        _AudioSource = gameObject.GetComponent<AudioSource>();
        _MusicSlider.value = _GameState.MusicLevel;
        StartCoroutine(PlayMusic());
        //Load Game Progress
        
        
        _GameState.Load();
        
        PlayerPrefs.Save();
        

        _OptionsCanvas.enabled = false;
        _AvatarCanvas.enabled = false;
    }
   
    public void Quit()
    {
        _GameState.Save();
        Application.Quit();
        Debug.Log("Application.Quit()");
    }
    public void Play()
    {
        SceneManager.LoadScene("SelectScene");
    }
    public void Options()
    {   
        _GlobeAnimator.SetTrigger("GlobeDown");
        _MainCanvas.enabled = false;
        _OptionsCanvas.enabled = true;
    }

    public void Avatar()
    {
        _GlobeAnimator.SetTrigger("GlobeDown");
        _MainCanvas.enabled = false;
        _AvatarCanvas.enabled = true;
    }

    public void Retour()
    {
        _GlobeAnimator.SetTrigger("GlobeUp");
        _MainCanvas.enabled = true;
        _OptionsCanvas.enabled = false;
        _AvatarCanvas.enabled = false;
    }
    private IEnumerator PlayMusic()
    {
        _AudioSource.Play();
        _AudioSource.volume = _GameState.MusicLevel;
        
        yield return null;
    }

    public void OnMusicValueChange()
    {
        _GameState.MusicLevel = _MusicSlider.value;
        _AudioSource.volume = _GameState.MusicLevel;
    }
    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteAll();
        _GameState.CurrentLevel = 1;
        _GameState.MusicLevel = 1;
        _GameState.ColorIndex = 0;
        _GameState.Load();
        SceneManager.LoadScene("MenuScene");
    }
}
