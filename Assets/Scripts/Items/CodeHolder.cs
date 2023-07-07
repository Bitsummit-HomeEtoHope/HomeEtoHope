using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodeHolder : MonoBehaviour
{
    public string code; // 存储物体的代码
    public string tag; // 存储物体的标签

    public Material defaultMaterial; // 默认材质
    public Material highlightMaterial; // 高亮材质
    private MeshRenderer meshRenderer; // 物体的 MeshRenderer 组件

    public bool ihaveChild = false;
    
    [System.Serializable]
    public struct ChildRenderers
    {
        public MeshRenderer childRenderer; // 子物体的 MeshRenderer 组件
        public Material childDefaultMaterial; // 子物体的默认材质
        public Material childHighlightMaterial; // 子物体的高亮材质
    }
    public List<ChildRenderers> childRenderers = new List<ChildRenderers>();

    private static CodeHolder selectedObject;

    private ShowList_MRYM showList; // ShowList_MRYM 脚本的引用

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

    private bool isClicked = false; // 记录物体是否被点击过

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
                // 取消上一个选中物体的高亮
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
            // 将当前物体的材质更改为高亮材质
            meshRenderer.material = highlightMaterial;
        }

        Debug.Log("Object Clicked");

        ItemsReading.Instance.ReceiveClickedObject(gameObject); // 将点击的物体信息发送给 ItemsReading
        DisposeButtonScript.Instance.ReceiveClickedObject(gameObject); // 将点击的物体信息发送给 DisposeButtonScript
        ItemsManager.Instance._isCanRotate = true; // 将 _isCanRotate 更改为 true
    }

    private void Update()
    {
        if (isClicked)
        {
            // 物体被点击后的逻辑处理
            isClicked = false;
        }
    }

    private void OnDestroy()
    {
        if (selectedObject == this)
        {
            // 如果销毁的是选中的物体，则取消高亮
            selectedObject = null;
        }

        if (showList != null)
        {
     //       showList.OffList();
        }
    }
}
