using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    private Transform[] _children;
    public Camera cameraSub;

    private void Start()
    {
        UpdateChildrenReferences();
    }

    private void Update()
    {
        foreach (var t in _children)
        {
            if (cameraSub != null)
                t.rotation = cameraSub.transform.rotation;
        }
    }

    private void UpdateChildrenReferences()
    {
        _children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _children[i] = transform.GetChild(i);
        }
    }

    private void OnTransformChildrenChanged()
    {
        UpdateChildrenReferences();
    }
}
