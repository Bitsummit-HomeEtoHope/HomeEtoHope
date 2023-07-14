using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBeltManager : SingletonManager<PortBeltManager>
{
    public List<Transform> portBeltsList;
    public Transform startPoint;
    public Transform endPoint;

    private List<Vector3> defaultPositions; // 用于保存默认位置

    private void Awake()
    {
        defaultPositions = new List<Vector3>();

        // 保存所有物体的默认位置
        foreach (var portBelt in portBeltsList)
        {
            defaultPositions.Add(portBelt.position);
        }
    }

    private void OnEnable()
    {


        // 开始移动物体
        StartCoroutine(MovePortBelts());
    }


    private void OnDisable()
    {
        // 将所有物体的位置重置为默认位置
        for (int i = 0; i < portBeltsList.Count; i++)
        {
            portBeltsList[i].position = defaultPositions[i];
        }        
    }

    private IEnumerator MovePortBelts()
    {
        while (true)
        {
            foreach (var portBelt in portBeltsList)
            {
                if (Vector3.Distance(portBelt.position, endPoint.position) > 0)
                {
                    portBelt.position -= new Vector3((float)(0.73 * -Time.deltaTime), 0, 0);
                }
            }

            foreach (var portBelt in portBeltsList)
            {
                if (Vector3.Distance(portBelt.position, endPoint.position) < 0.05f)
                {
                    portBelt.position = startPoint.position;
                }
            }

            yield return null;
        }
    }
}
