using UnityEngine;
using UnityEngine.UI;

public class Earth_rotate : MonoBehaviour
{
    public float rotationSpeed = -360f; // ÿ������ʱ����ת360�ȵ��ٶ�

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
