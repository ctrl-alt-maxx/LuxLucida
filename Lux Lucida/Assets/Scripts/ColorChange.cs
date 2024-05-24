using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ColorChange : MonoBehaviour
{
    private List<Color> _Colors;
    private int _CurrentColorIndex;
    [SerializeField]
    private GameState _GameState;
    [SerializeField]
    private Material _PlayerMaterial;
    [SerializeField]
    private Image _Image;
    // Start is called before the first frame update
    void Start()
    {
        _Colors = new List<Color>();
        _Colors.Add(Color.red);
        _Colors.Add(Color.yellow);
        _Colors.Add(Color.green);
        _Colors.Add(Color.cyan);
        _Colors.Add(Color.blue);
        _Colors.Add(Color.magenta);
        _Colors.Add(Color.white);
        _Colors.Add(Color.black);
        _PlayerMaterial.SetColor("_Color", _Colors[_CurrentColorIndex]);
        _CurrentColorIndex = _GameState.ColorIndex;
        _Image.color = _Colors[_CurrentColorIndex];
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    public void ChangeColor()
    { 
        _CurrentColorIndex++;
        if(_CurrentColorIndex >= _Colors.Count)
        {
            _CurrentColorIndex = 0;
        }
        _Image.color = _Colors[_CurrentColorIndex];
        _PlayerMaterial.SetColor("_Color", _Colors[_CurrentColorIndex]);    
        _GameState.ColorIndex = _CurrentColorIndex;
    }
}
