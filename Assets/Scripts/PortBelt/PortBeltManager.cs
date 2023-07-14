using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBeltManager : SingletonManager<PortBeltManager>
{
    public List<Transform> portBeltsList;
    public Transform startPoint;
    public Transform endPoint;

    private List<Vector3> defaultPositions; // ���ڱ���Ĭ��λ��

    private void Awake()
    {
        defaultPositions = new List<Vector3>();

        // �������������Ĭ��λ��
        foreach (var portBelt in portBeltsList)
        {
            defaultPositions.Add(portBelt.position);
        }
    }

    private void OnEnable()
    {


        // ��ʼ�ƶ�����
        StartCoroutine(MovePortBelts());
    }


    private void OnDisable()
    {
        // �����������λ������ΪĬ��λ��
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
