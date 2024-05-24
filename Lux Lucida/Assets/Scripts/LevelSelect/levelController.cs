using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Level
{
    public int level;
    public string name, description, sceneName;
    public Level(int levelNumber, string _name, string _description) { 
        level = levelNumber;
        name = _name;
        description = _description;
        sceneName = "LevelScene" + (level).ToString();
    }

}
[RequireComponent(typeof(AudioSource))]
public class levelController : MonoBehaviour
{
    private LinkedList<Level> levels = new LinkedList<Level>();
    [SerializeField]
    private TMP_Text _NameTMP, _DescriptionTMP;
    [SerializeField]
    private GameObject _LevelInfoPanel;
    [SerializeField]
    private GameState _GameState;
    [SerializeField]
    private Transform _PlayerTransform;
    [SerializeField]
    private List<AudioClip> _MusicList;
    private AudioSource _AudioSource;
    public int LastUnlockedLevel;
    private Level _SelectedLevel = null;
    // Start is called before the first frame update
    void Start()
    {
        LastUnlockedLevel = _GameState.CurrentLevel;
        _AudioSource = GetComponent<AudioSource>();
        _AudioSource.volume = _GameState.MusicLevel;
        int musicId =0;
        switch (LastUnlockedLevel)
        {
            case 1:
                musicId = 0;
                break;
            case 2:
                musicId = 1;
                break;  
            case 3:
                musicId = 2;
                break;
            case 4:
                musicId = 2;
                break;
            case 5:
                musicId = 2;
                break;
            case 6:
                musicId = 3;
                break;
        }
        _AudioSource.clip = _MusicList[musicId];
        _AudioSource.Play();
        
        _PlayerTransform.position = _GameState.PlayerPosition;
        levels.AddLast(new Level(1, "Level One : A dark new world", "Here is where I start my journey to light up our world"));
        levels.AddLast(new Level(2, "Level Two : The plains of dawn", "These grenades will surely help me.\n I'm starting to see the light at the end of the tunnel...."));
        levels.AddLast(new Level(3, "Level Three : Light fuels me", "I can finally reach higher places using my new boots"));
        levels.AddLast(new Level(4, "Level Four : The air is different here", "This gauntlet is giving me weird proprieties. Somehow I feel lighter with it on."));
        levels.AddLast(new Level(5, "Level Five : A beautiful sunrise", "My quest is coming to an end but I can't give up yet."));
        
        HideHUD();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void ShowHUD()
    {
        _LevelInfoPanel.SetActive(true);
        _NameTMP.text = _SelectedLevel.name;
        _DescriptionTMP.text = _SelectedLevel.description;
    }
    public void HideHUD()
    {
        _LevelInfoPanel.SetActive(false);
    }

    public void SelectLevel(int level) {
        LinkedListNode<Level> node = levels.First;
        for(int i = 1; i < level; i++)
        {
            node = node.Next;
        }
        _SelectedLevel = node.Value;
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(_SelectedLevel.sceneName);
        _GameState.PlayerPosition = _PlayerTransform.position;
    }


    private void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
