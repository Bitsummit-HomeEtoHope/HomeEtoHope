using UnityEngine;

public class CodeHolder : MonoBehaviour
{
    public string code; // �洢����Ĵ���

    private void Start()
    {
        // ����������ʱ����������ӵ� code �����У���ɾ�� (Clone)
        code = gameObject.name.Replace("(Clone)", "");
    //    Debug.Log("Object Name: " + code);
    }

    public string GetCode()
    {
        return code;
    }

    private bool isClicked = false; // �����Ƿ񱻵����״̬

    private void OnMouseDown()
    {
        isClicked = true;
        // �������д�������屻����Ĵ����߼�

        ItemsReading.Instance.ReceiveClickedObject(gameObject); // ���������������Ϣ���͸� ItemsReading
        DisposeButtonScript.Instance.ReceiveClickedObject(gameObject); // ���������������Ϣ���͸� DisposeButtonScript
        ItemsManager.Instance._isCanRotate = true; // �� _isCanRotate ����Ϊ true
    }

    private void Update()
    {
        if (isClicked)
        {
            Debug.Log("ok");
            // ���屻�������߼�
            isClicked = false;
        }
    }
}
