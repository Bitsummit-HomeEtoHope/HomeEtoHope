using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualScreenDisplay : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;

    void Start()
    {
        // 获取所有屏幕的数量
        int displayCount = Display.displays.Length;

        // 确保有至少两个屏幕可用
        if (displayCount < 2)
        {
            Debug.LogError("At least two displays are required for dual screen display.");
            return;
        }

        // 启用第一个外部屏幕
        Display.displays[1].Activate();

        // 将第一个相机渲染到第一个外部屏幕
        camera1.targetDisplay = 1;

        // 如果有第二个外部屏幕，则启用它并将第二个相机渲染到它上面
        if (displayCount > 2)
        {
            Display.displays[2].Activate();
            camera2.targetDisplay = 2;
        }
    }
}
