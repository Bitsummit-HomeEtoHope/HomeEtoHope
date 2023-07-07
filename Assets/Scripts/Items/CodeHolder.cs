using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodeHolder : MonoBehaviour
{
    public string code; // �洢����Ĵ���
    public string tag; // �洢����ı�ǩ

    public Material defaultMaterial; // Ĭ�ϲ���
    public Material highlightMaterial; // ��������
    private MeshRenderer meshRenderer; // ����� MeshRenderer ���

    public bool ihaveChild = false;
    
    [System.Serializable]
    public struct ChildRenderers
    {
        public MeshRenderer childRenderer; // ������� MeshRenderer ���
        public Material childDefaultMaterial; // �������Ĭ�ϲ���
        public Material childHighlightMaterial; // ������ĸ�������
    }
    public List<ChildRenderers> childRenderers = new List<ChildRenderers>();

    private static CodeHolder selectedObject;

    private ShowList_MRYM showList; // ShowList_MRYM �ű�������

    private void Start()
    {
        code = gameObject.name.Replace("(Clone)", "");
        tag = gameObject.tag;
        Debug.Log("Object Tag: " + tag);

        if (tag == "Human")
        {
            showList = FindObjectOfType<ShowList_MRYM>();
            if (showList != null && !showList.listing)
            {
                //
                showList.listing = true;
                showList.OpenList();
            }
        }
         meshRenderer = GetComponent<MeshRenderer>();
        
    }

    public string GetCode()
    {
        return code;
    }

    private bool isClicked = false; // ��¼�����Ƿ񱻵����

    private void OnMouseDown()
    {
        if (selectedObject != null)
        {
            if (selectedObject.ihaveChild)
            {
                foreach (ChildRenderers childRenderer in selectedObject.childRenderers)
                {
                    childRenderer.childRenderer.material = childRenderer.childDefaultMaterial;
                }
            }
            else
            {
                // ȡ����һ��ѡ������ĸ���
                selectedObject.meshRenderer.material = selectedObject.defaultMaterial;
            }
        }
        if(selectedObject != this )
        GetComponent<AudioSource>().Play();
        selectedObject = this;
        isClicked = true;

        if (ihaveChild)
        {
            foreach (ChildRenderers childRenderer in childRenderers)
            {
                childRenderer.childRenderer.material = childRenderer.childHighlightMaterial;
            }
        }
        else
        {
            // ����ǰ����Ĳ��ʸ���Ϊ��������
            meshRenderer.material = highlightMaterial;
        }

        Debug.Log("Object Clicked");

        ItemsReading.Instance.ReceiveClickedObject(gameObject); // �������������Ϣ���͸� ItemsReading
        DisposeButtonScript.Instance.ReceiveClickedObject(gameObject); // �������������Ϣ���͸� DisposeButtonScript
        ItemsManager.Instance._isCanRotate = true; // �� _isCanRotate ����Ϊ true
    }

    private void Update()
    {
        if (isClicked)
        {
            // ���屻�������߼�����
            isClicked = false;
        }
    }

    private void OnDestroy()
    {
        if (selectedObject == this)
        {
            // ������ٵ���ѡ�е����壬��ȡ������
            selectedObject = null;
        }

        if (showList != null)
        {
     //       showList.OffList();
        }
    }
}
