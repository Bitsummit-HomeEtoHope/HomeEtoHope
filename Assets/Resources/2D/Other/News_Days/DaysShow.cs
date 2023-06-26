using UnityEngine;

public class DaysShow : MonoBehaviour
{
    private bool isClosed = false; // �����Ϸ�����Ƿ񱻹ر�
    private float timer = 0f; // ��ʱ��
    private float delayTime = 1f; // �ӳٹرյ�ʱ��

    private void OnMouseDown()
    {
        if (!isClosed)
        {
            Time.timeScale = 0f; // ��ͣ��Ϸ
            timer = 0f;
            isClosed = true;
        }
    }

    private void Update()
    {
        if (isClosed)
        {
            timer += Time.unscaledDeltaTime; // ʹ��unscaledDeltaTime��ʱ������Time.timeScaleӰ��

            if (timer >= delayTime)
            {
                Time.timeScale = 1f; // �ָ���Ϸʱ������
                gameObject.SetActive(false); // �ر���Ϸ����
            }
            else
            {
                float scale = Mathf.Lerp(1f, 0f, timer / delayTime); // ���������ֵ
                transform.localScale = new Vector3(scale, scale, 1f); // ���ñ�����ֵ
            }
        }
    }
}
