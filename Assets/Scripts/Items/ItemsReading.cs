using UnityEngine;

public class ItemsReading : MonoBehaviour
{
    private static ItemsReading instance; // ����ʵ��
    public static ItemsReading Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // ȷ��ֻ��һ�� ItemsReading ʵ������
        }
        else
        {
            instance = this;
        }
    }

    public void ReceiveClickedObject(GameObject clickedObject)
    {
        Debug.Log("Received clicked object: " + clickedObject.name);

        // �����������������ƽ����ض��Ĳ���


    }

    // �����������߼�...
}
