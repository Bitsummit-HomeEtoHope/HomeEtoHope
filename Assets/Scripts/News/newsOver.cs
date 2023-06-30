using UnityEngine;
using UnityEngine.SceneManagement;

public class newsOver : MonoBehaviour
{
    private bool sceneSwitched = false; // �Ƿ����л�����
    private float elapsedTime = 0f; // ������ʱ��


    private void Update()
    {
        // ���������
        if (Input.GetMouseButtonDown(0) && !sceneSwitched)
        {
            SwitchScene();
        }

        // �ۼӾ�����ʱ��
        elapsedTime += Time.deltaTime;

        // �ȴ�ʱ�䳬��10��
        if (elapsedTime >= 10f && !sceneSwitched)
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        sceneSwitched = true;
        // �л�����һ������������ʹ���˳���������Ҳ����ʹ�ó�������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
