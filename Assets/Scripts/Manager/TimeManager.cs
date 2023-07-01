using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // 控制暂停状态
    [SerializeField] public List<GameObject> targetObjects; // 多个游戏对象

    private void Start()
    {
        Pause();
    }

    private void Update()
    {
        SetTimeScale(1);
    }

    private void SetTimeScale(float timeScale)
    {
        // 检查是否指定了游戏对象
        if (targetObjects != null && targetObjects.Count > 0)
        {
            foreach (GameObject obj in targetObjects)
            {
                // 设置游戏对象的时间缩放值
                obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // 重置刚体速度（可选）
            }
            Time.timeScale = timeScale;
        }
    }


    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // 暂停时间缩放
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // 恢复时间缩放
    }
}
