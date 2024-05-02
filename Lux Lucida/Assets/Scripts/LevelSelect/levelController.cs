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
    public Level(int levelNumber, string _name, string _description, string _sceneName) { 
        level = levelNumber;
        name = _name;
        description = _description;
        sceneName = _sceneName;
    }

}
public class levelController : MonoBehaviour
{
    [SerializeField]
    private LinkedList<Level> levels = new LinkedList<Level>();
    [SerializeField]
    private TMP_Text _NameTMP, _DescriptionTMP;
    [SerializeField]
    private GameObject _LevelInfoPanel;
    public int LastUnlockedLevel = 1;
    private Level _SelectedLevel = null;
    // Start is called before the first frame update
    void Start()
    {   
        levels.AddLast(new Level(1, "La valée sombre", "Ici ce débute votre quête pour illuminé le globe.", "LevelScene-0.0.1"));
        levels.AddLast(new Level(2, "Plaine crépuscule", "Je vois la lumiere au bout du tunnel", "deezNuts"));
        HideHUD();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHUD()
    {
        _LevelInfoPanel.SetActive(true);
        _NameTMP.text = "Level" + _SelectedLevel.name;
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
        Debug.Log(level);
        SceneManager.LoadScene(_SelectedLevel.sceneName);
    }

}
