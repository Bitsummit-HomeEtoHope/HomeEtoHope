using UnityEngine;

public class ResetTime : MonoBehaviour
{
    private float originalTimeScale;
    private Rigidbody[] childRigidbodies;

    private void Start()
    {
        // ��ȡ��Ϸ���弰���������ϵ����и������
        childRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {
        Resume();
    }

    public void Pause()
    {
        // ����ԭʼ��ʱ��߶�ֵ
        originalTimeScale = Time.timeScale;
        // ����ʱ��߶�Ϊ0����ͣ��Ϸ���弰��������
        Time.timeScale = 0f;

        // ����и���������������ǵ�ʱ��߶�Ϊ0��ֹͣ���ǵ��˶�
        if (childRigidbodies != null)
        {
            foreach (var rb in childRigidbodies)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    public void Resume()
    {
        // �ָ�ԭʼ��ʱ��߶�ֵ
        Time.timeScale = 1f;

        // ����и���������������ǵ�ʱ��߶�Ϊ1���ָ����ǵ��˶�
        if (childRigidbodies != null)
        {
            foreach (var rb in childRigidbodies)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
