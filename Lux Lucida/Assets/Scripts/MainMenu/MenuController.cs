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


    private bool _IsGlobeDown = false;

    private void Start()
    {
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
        SceneManager.LoadScene("LevelScene-0.0.1");
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
