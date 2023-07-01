using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // ������ͣ״̬
    [SerializeField] public List<GameObject> targetObjects; // �����Ϸ����

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
        // ����Ƿ�ָ������Ϸ����
        if (targetObjects != null && targetObjects.Count > 0)
        {
            foreach (GameObject obj in targetObjects)
            {
                // ������Ϸ�����ʱ������ֵ
                obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // ���ø����ٶȣ���ѡ��
            }
            Time.timeScale = timeScale;
        }
    }


    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // ��ͣʱ������
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // �ָ�ʱ������
    }
}
