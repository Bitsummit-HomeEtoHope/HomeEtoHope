using UnityEngine;
using UnityEngine.UI;

public class HideUIOnCameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    private Canvas uiCanvas;

    [SerializeField]
    private bool isUIVisible = true;

    void Start()
    {
        uiCanvas = GetComponent<Canvas>();
    }
    void Update()
    {
        // 检测是否切换到其他相机
        if (Camera.current != mainCamera)
        {
            // 如果UI当前可见，则隐藏它
            if (isUIVisible)
            {
                uiCanvas.enabled = false;
                isUIVisible = false;
            }
        }
        else
        {
            // 如果UI当前不可见，则显示它
            if (!isUIVisible)
            {
                uiCanvas.enabled = true;
                isUIVisible = true;
            }
        }
    }
}
