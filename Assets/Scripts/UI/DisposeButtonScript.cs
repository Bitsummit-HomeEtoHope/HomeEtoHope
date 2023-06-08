using UnityEngine;

public class DisposeButtonScript : MonoBehaviour
{
    private static DisposeButtonScript _instance;
    public static DisposeButtonScript Instance => _instance;

    private GameObject clickedObject; // 被点击的物体

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
            // 在这里执行按钮作用于指定物体的逻辑

            // 将物体的标签更改为 "Dispose"
            clickedObject.tag = "Dispose";
        }
    }
}
