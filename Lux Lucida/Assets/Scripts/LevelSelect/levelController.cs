using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
    public int LastUnlockedLevel;
    private Level _SelectedLevel = null;
    // Start is called before the first frame update
    void Start()
    {
        LastUnlockedLevel = _GameState.CurrentLevel;
        _PlayerTransform.position = _GameState.PlayerPosition;
        levels.AddLast(new Level(1, "A dark new world", "Here is where you start your journey to light up our world"));
        levels.AddLast(new Level(2, "The plains of dawn", "I see the light at the end of the tunnel"));
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
        _NameTMP.text = "Level " + _SelectedLevel.level.ToString() + ": "  + _SelectedLevel.name;
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
