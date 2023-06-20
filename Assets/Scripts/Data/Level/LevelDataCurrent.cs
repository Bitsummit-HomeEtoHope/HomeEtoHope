using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataCurrent : MonoBehaviour
{
    public int _levelID;
    public string _levelName;
    [Tooltip("The time limit for the level")]
    public int _levelTime;
    [Range(3,6)]
    [Tooltip("The interval between the appearance of props in the level")]
    public float _interval;
    public AudioClip _levelBGM;
    public LevelData _levelData;

    private void Awake()
    {
        _levelID = _levelData.levelID;
        _levelName = _levelData.levelName;
        _levelTime = _levelData.levelTime;
        _interval = _levelData.Interval;
        _levelBGM = _levelData.levelBGM;
        
    }
}
