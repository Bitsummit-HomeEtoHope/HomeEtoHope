using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance => _instance;

    protected void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this as T;
    }
}
