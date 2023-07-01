using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // 检测鼠标左击
        if (Input.GetMouseButtonDown(0))
        {
            // 结束运行（仅在编辑器中有效）
            EndGame();
        }
    }

    void EndGame()
    {
        // 在编辑器中停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在游戏中退出应用程序
        QuitGame();
#endif
    }

    void QuitGame()
    {
        // 退出应用程序
        Application.Quit();
    }
}

/*
  
  // 检测鼠标左击
        if (Input.GetMouseButtonDown(0))
        {
            // 跳转到指定场景（假设场景名称为 "NextScene"）
            SceneManager.LoadScene("NextScene");
        } 
 
 */
