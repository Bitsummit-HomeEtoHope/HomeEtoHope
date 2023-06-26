using UnityEngine;

public class DaysShow : MonoBehaviour
{
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // 当按下P键时切换暂停状态
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // 将时间缩放设置为0，实现暂停效果
        // 在此处可以执行其他暂停相关的逻辑，如显示暂停菜单等
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // 将时间缩放设置为1，恢复正常时间流逝
        // 在此处可以执行其他恢复游戏相关的逻辑，如关闭暂停菜单等
    }
}
