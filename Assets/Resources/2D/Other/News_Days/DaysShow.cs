using UnityEngine;

public class DaysShow : MonoBehaviour
{
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // ������P��ʱ�л���ͣ״̬
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
        Time.timeScale = 0f; // ��ʱ����������Ϊ0��ʵ����ͣЧ��
        // �ڴ˴�����ִ��������ͣ��ص��߼�������ʾ��ͣ�˵���
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // ��ʱ����������Ϊ1���ָ�����ʱ������
        // �ڴ˴�����ִ�������ָ���Ϸ��ص��߼�����ر���ͣ�˵���
    }
}
