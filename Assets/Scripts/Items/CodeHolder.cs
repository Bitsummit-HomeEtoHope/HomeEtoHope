using UnityEngine;

public class CodeHolder : MonoBehaviour
{
    public string code; // 存储物体的代码

    public string GetCode()
    {
        return code;
    }

    private bool isClicked = false; // 物体是否被点击的状态

    private void OnMouseDown()
    {
        isClicked = true;
        // 在这里编写处理物体被点击的代码逻辑

        ItemsReading.Instance.ReceiveClickedObject(gameObject); // 将被点击的物体信息发送给 ItemsReading
        DisposeButtonScript.Instance.ReceiveClickedObject(gameObject); // 将被点击的物体信息发送给 DisposeButtonScript
        ItemsManager.Instance._isCanRotate = true; // 将 _isCanRotate 更改为 true
    }

    private void Update()
    {
        if (isClicked)
        {
            Debug.Log("ok");
            // 物体被点击后的逻辑
            isClicked = false;
        }
    }
}
