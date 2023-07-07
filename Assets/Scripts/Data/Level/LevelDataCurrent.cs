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
    [Range(0,100)]
    public int _food_goodWeight;
    [Range(0,100)]
    public int _food_badWeight;
    [Range(0,100)]
    public int _tool_goodweight;
    [Range(0,100)]
    public int _tool_badweight;
    [Range(0,100)]
    public int _human_weight;
    [Range(0.5f,8)]
    public float _spawnInterval;

    public LevelData.Future_Data _future_Data;

    private void Awake()
    {
        _levelID = _levelData.levelID;
        _levelName = _levelData.levelName;
        _levelTime = _levelData.levelTime;
        _interval = _levelData.Interval;
        _levelBGM = _levelData.levelBGM;
        _future_Data=_levelData.future_Data;
        _food_goodWeight = _levelData._food_goodWeight;
        _food_badWeight = _levelData._food_badWeight;
        _tool_goodweight = _levelData._tool_goodweight;
        _tool_badweight = _levelData._tool_badweight;
        _human_weight = _levelData._human_weight;
        _spawnInterval = _levelData._spawnInterval;
        
        
    }
}
