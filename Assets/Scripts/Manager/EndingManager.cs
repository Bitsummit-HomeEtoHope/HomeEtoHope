using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // ���������
        if (Input.GetMouseButtonDown(0))
        {
            // �������У����ڱ༭������Ч��
            EndGame();
        }
    }

    void EndGame()
    {
        // �ڱ༭����ֹͣ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����Ϸ���˳�Ӧ�ó���
        QuitGame();
#endif
    }

    void QuitGame()
    {
        // �˳�Ӧ�ó���
        Application.Quit();
    }
}

/*
  
  // ���������
        if (Input.GetMouseButtonDown(0))
        {
            // ��ת��ָ�����������賡������Ϊ "NextScene"��
            SceneManager.LoadScene("NextScene");
        } 
 
 */
