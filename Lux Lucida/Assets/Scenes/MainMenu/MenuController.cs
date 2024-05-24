using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
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

    [SerializeField]
    private TMP_Text _ModText;

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


    void OnGUI()
    {
       
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Alpha1.ToString())))
        {
            _ModText.text = "MOD: 1";
            _GameState.CurrentLevel = 1;
        }
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Alpha2.ToString())))
        {
            _ModText.text = "MOD: 2";
            _GameState.CurrentLevel = 2;
            
        }
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Alpha3.ToString())))
        {
            _ModText.text = "MOD: 3";
            _GameState.CurrentLevel = 3;
        }
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Alpha4.ToString())))
        {
            _ModText.text = "MOD: 4";
            _GameState.CurrentLevel = 4;
        }
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Alpha5.ToString())))
        {
            _ModText.text = "MOD: 5";
            _GameState.CurrentLevel = 5;
        }
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Alpha6.ToString())))
        {
            _ModText.text = "MOD: Final";
            _GameState.CurrentLevel = 6;
        }
       

        
    }

    private void Update()
    {
        
       
        
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
        _AudioSource.Play();

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
