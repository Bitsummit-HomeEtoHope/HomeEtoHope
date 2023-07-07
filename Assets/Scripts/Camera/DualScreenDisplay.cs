using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualScreenDisplay : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;    
    public Camera camera3;

    void Start()
    {
        int width1 = Display.displays[0].systemWidth;
        int height1 = Display.displays[0].systemHeight;
        //Debug.Log(Display.displays[1]);
        int width2 = Display.displays[1].systemWidth;
        int height2 = Display.displays[1].systemHeight;
        // 将Unity游戏窗口的分辨率设置为第一个外部屏幕的分辨率
        Screen.SetResolution(width1, height1, true);
        Screen.SetResolution(width2, height2, true);
    
    
    
        // 获取所有屏幕的数量
        int displayCount = Display.displays.Length;

        // 确保有至少两个屏幕可用
        if (displayCount < 2)
        {
            Debug.LogError("At least two displays are required for dual screen display.");
            return;
        }

        //// 启用第一个外部屏幕
        //Display.displays[0].Activate();

        //// 将第一个相机渲染到第一个外部屏幕
        //camera1.targetDisplay = 1;
        //Display.displays[1].Activate();
        //camera2.targetDisplay = 2;

        // 启用第一个外部屏幕
        Display.displays[1].Activate();
        Display.displays[2].Activate();

        // 将第一个相机渲染到第一个外部屏幕
        camera1.targetDisplay = 1;
        camera2.targetDisplay = 1;

        // 将第三个相机渲染到第二个外部屏幕
        camera3.targetDisplay = 2;

    }
}
    
