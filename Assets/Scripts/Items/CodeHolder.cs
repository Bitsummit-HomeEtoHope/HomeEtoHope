using UnityEngine;

public class CodeHolder : MonoBehaviour
{
    public string code; // �洢����Ĵ���
    public string tag; // �洢����ı�ǩ

    private ShowList_MRYM showList; // ���� ShowList_MRYM �ű�

    private void Start()
    {
        // ����������ʱ����������ӵ� code �����У���ɾ�� (Clone)
        code = gameObject.name.Replace("(Clone)", "");

        // ��ȡ����ı�ǩ
        tag = gameObject.tag;
        Debug.Log("Object Tag: " + tag);

        if (tag == "Human")
        {
            showList = FindObjectOfType<ShowList_MRYM>();
            if (showList != null)
            {
                showList.OpenList();
            }
        }
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

        Debug.Log("Object Clicked");

        ItemsReading.Instance.ReceiveClickedObject(gameObject); // ���������������Ϣ���͸� ItemsReading
        DisposeButtonScript.Instance.ReceiveClickedObject(gameObject); // ���������������Ϣ���͸� DisposeButtonScript
        ItemsManager.Instance._isCanRotate = true; // �� _isCanRotate ����Ϊ true

    }

    private void Update()
    {
        if (isClicked)
        {
            // ���屻�������߼�
            isClicked = false;
        }

        
    }
    private void OnDestroy()
    {
        if (showList != null)
        {
            showList.OffList();
        }
    }

}
