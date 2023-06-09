using UnityEngine;

public class DisposeButtonScript : MonoBehaviour
{
    private static DisposeButtonScript _instance;
    public static DisposeButtonScript Instance => _instance;

    private GameObject clickedObject; // �����������

    void Awake()
    {
        _instance = this;
    }

    public void ReceiveClickedObject(GameObject clickedObject)
    {
        this.clickedObject = clickedObject;
    }

    public void OnClick()
    {
        if (clickedObject != null)
        {
            Debug.Log("Clicked Object: " + clickedObject.name);
            // ������ִ�а�ť������ָ��������߼�

            // ������ı�ǩ����Ϊ "Dispose"
            clickedObject.tag = "Dispose";
        }
    }
}
