using UnityEngine;

public class Trrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player�����˴�������");
            // ������ִ������Ҫ�Ĳ���
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player�뿪�˴�������");
            // ������ִ������Ҫ�Ĳ���
        }
    }
}
