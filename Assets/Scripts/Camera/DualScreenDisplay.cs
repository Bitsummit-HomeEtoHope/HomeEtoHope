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
        int width2 = Display.displays[1].systemWidth;
        int height2 = Display.displays[1].systemHeight;
        
        // Set the Unity game window resolution to match the first external screen resolution
        Screen.SetResolution(width1, height1, true);
        Screen.SetResolution(width2, height2, true);
    
        // Get the number of available screens
        int displayCount = Display.displays.Length;

        // Ensure at least two displays are available
        if (displayCount < 2)
        {
            Debug.LogError("At least two displays are required for dual screen display.");
            return;
        }

        // Activate the first and second external screens
        Display.displays[1].Activate();
        Display.displays[2].Activate();

        // Render the first camera to the first external screen
        camera1.targetDisplay = 1;
        camera2.targetDisplay = 1;

        // Render the third camera to the second external screen
        camera3.targetDisplay = 2;
    }
}
