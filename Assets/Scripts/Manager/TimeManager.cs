using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // ������ͣ״̬
    [SerializeField] public List<GameObject> targetObjects; // �����Ϸ����

    private List<GameObject> nonTargetObjects; // ��Ŀ������б�

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
