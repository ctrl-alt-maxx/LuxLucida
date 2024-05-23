using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameState : ScriptableObject
{
    public int CurrentLevel = 2;
    public Vector3 PlayerPosition = new Vector3 (0, 0, 27.35f);
    public void Load()
    {
        if (!(PlayerPrefs.HasKey("CurrentLevel")))
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }

        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");

    }
    public void Save()
    {
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
    }
}
