using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_Student : MonoBehaviour
{
    [Header("your Ending")]
    //public bool isyourEnding = true;
    //public float points = 1f;
    [Header("will End_I -- total score")]
    public bool canEnd_i = false;
    public float points_end_i = 0f;
    [Header("will End_II -- bad number")]
    public bool canEnd_ii = false;
    public float points_end_ii = 0f ;
    [Header("will End_III -- die number")]
    public bool canEnd_iii = false;
    public float points_end_iii = 0f;
    [Header("will End_IV -- building number")]
    public bool canEnd_iv = false;
    public float points_end_iv = 0f;
    [Header("will End_V -- factory number")]
    public bool canEnd_v = false;
    public float points_end_v = 0f ;
    [Header("will End_VI -- farm number")]
    public bool canEnd_vi = false;
    public float points_end_vi = 0f;

    void Awake()
    {
        Point_Teacher teacher = FindObjectOfType<Point_Teacher>();

        if (teacher != null)
        {
            // if(isyourEnding) teacher.AddPoints(points,0,0,0,0,0,0);
            if (canEnd_i) teacher.AddPoints(0, points_end_i, 0, 0, 0, 0, 0);
            if (canEnd_ii) teacher.AddPoints(0, 0, points_end_ii, 0, 0, 0, 0);
            if (canEnd_iii) teacher.AddPoints(0, 0, 0, points_end_iii, 0, 0, 0);
            if (canEnd_iv) teacher.AddPoints(0, 0, 0, 0, points_end_iv, 0, 0);
            if (canEnd_v) teacher.AddPoints(0, 0, 0, 0, 0, points_end_v, 0);
            if (canEnd_vi) teacher.AddPoints(0, 0, 0, 0, 0, 0, points_end_vi);
        }
    }
}


/*
 
  if (teacher != null)
        {
            teacher.credits += points; // 将 points 的值添加到 teacher 的 credits 中
        }
 
 */