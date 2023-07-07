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
    public Future_Data future_Data;
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


    [System.Serializable]
    //生成名为Future_Data的结构体
    public struct Future_Data
    {
        public float tree_Time;
        public float human_Speed;
        public float human_BadFood_Time;
        public float human_BadTool_Time;
        public float build_Time;
        public float thief_Time;
        public int thief_Food_Num;
        public float itemBrokenHuman_time;
        public float murderer_time;


    }

    


}
