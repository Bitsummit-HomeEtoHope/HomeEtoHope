using UnityEngine;

public class ItemsReading : MonoBehaviour
{
    private static ItemsReading instance; // 单例实例
    public static ItemsReading Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // 确保只有一个 ItemsReading 实例存在
        }
        else
        {
            instance = this;
        }
    }

    public void ReceiveClickedObject(GameObject clickedObject)
    {
        Debug.Log("Received clicked object: " + clickedObject.name);

        // 在这里根据物体的名称进行特定的操作


    }

    // 其他方法和逻辑...
}
