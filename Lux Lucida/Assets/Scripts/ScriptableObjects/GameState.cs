using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameState : ScriptableObject
{
    public int CurrentLevel;
    public Vector3 PlayerPosition = new Vector3 (0, 0, 27.35f);
    public int ColorIndex;
    public float MusicLevel;
    public void Load()
    {
        if (!(PlayerPrefs.HasKey("CurrentLevel")))
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        if (!(PlayerPrefs.HasKey("ColorIndex")))
        {
            PlayerPrefs.SetInt("ColorIndex", 0);
        }
        if (!(PlayerPrefs.HasKey("MusicLevel")))
        {
            PlayerPrefs.SetFloat("MusicLevel", 1.0f);
        }

        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        ColorIndex = PlayerPrefs.GetInt("ColorIndex");
        MusicLevel = PlayerPrefs.GetFloat("MusicLevel");

    }
    public void Save()
    {
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.SetInt("ColorIndex", ColorIndex);
        PlayerPrefs.SetFloat("MusicLevel", MusicLevel);
    }
}
