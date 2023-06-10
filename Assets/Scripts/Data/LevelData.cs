using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Level Data/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int levelID;
    public string levelName;
    [Tooltip("The time limit for the level")]
    public int levelTime;
    [Range(3,6)]
    [Tooltip("The interval between the appearance of props in the level")]
    public float Interval;
    public AudioClip levelBGM;


}
