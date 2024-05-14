using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class GameState : ScriptableObject
{
    public int NextLevel = 2;
    public Vector3 PlayerPosition = new Vector3 (0, 0, 27.35f);
    
}
