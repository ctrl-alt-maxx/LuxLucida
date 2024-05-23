using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    private Animator _GlobeAnimator;
    [SerializeField]
    private Canvas _MainCanvas, _AvatarCanvas, _OptionsCanvas;
    [SerializeField]
    private GameState _GameState;
    [SerializeField]
    private int _level;

    private void Start()
    {

        //Load Game Progress

        if (!(PlayerPrefs.HasKey("CurrentLevel"))){
            PlayerPrefs.SetInt("CurrentLevel", 1);
            _level = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            _level = PlayerPrefs.GetInt("CurrentLevel");
        }
        PlayerPrefs.Save();


        _OptionsCanvas.enabled = false;
        _AvatarCanvas.enabled = false;
    }
    public void Quit()
    {
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
}
