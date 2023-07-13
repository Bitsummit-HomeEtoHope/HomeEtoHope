using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField]public GameObject fin;
    [SerializeField]public GameObject fin_meto;
    // Update is called once per frame
    void Update()
    {
        // ���������
        if (Input.GetMouseButtonDown(0))
        {
            if (!fin.activeSelf) 
            { 
                fin_meto.active = false;
            }

            else EndGame();

            //else fin.SetActive(false);
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
