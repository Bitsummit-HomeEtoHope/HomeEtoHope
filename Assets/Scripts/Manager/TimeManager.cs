using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // 控制暂停状态
    [SerializeField] public List<GameObject> targetObjects; // 多个游戏对象

    private List<GameObject> nonTargetObjects; // 非目标对象列表

    private void OnEnable()
    {
        Pause();
    }

    private void OnDisable()
    {
        Resume();
    }

    private void Start()
    {
        nonTargetObjects = new List<GameObject>();
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (!targetObjects.Contains(obj))
            {
                nonTargetObjects.Add(obj);
            }
        }
    }

    private void Update()
    {
        if (isPaused)
        {
            SetTimeScale(0f);
        }
    }

    private void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        SetTimeScale(1f);        
    }
}
