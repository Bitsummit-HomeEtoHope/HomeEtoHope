using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBeltManager : SingletonManager<PortBeltManager>
{
    public List<Transform> portBeltsList;
    public Transform startPoint;
    public Transform endPoint;

    private void MovePortBelts(List<Transform> portBelts)
    {
        foreach (var portBelt in portBelts)
        {
            if (Vector3.Distance(portBelt.position,endPoint.position) > 0)
            {
                portBelt.position += new Vector3(-Time.deltaTime, 0, 0);
            }
        }
    }

    private void Update()
    {
        MovePortBelts(portBeltsList);
        TransportPortBelts(portBeltsList);
    }

    private void TransportPortBelts(List<Transform> portBelts)
    {
        foreach (var portBelt in portBelts)
        {
            if (Vector3.Distance(portBelt.position,endPoint.position) < 0.1f)
            {
                portBelt.position = startPoint.position;
            }
        }
    }
}