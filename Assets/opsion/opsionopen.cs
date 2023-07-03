using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
public class opsionopen : MonoBehaviour
{
    //設定用のcanvas
    public GameObject Option_Canvas;
    //設定用の画面に移行するボタン
    public GameObject settingButton;
    //
    public GameObject titleScreen;

    public GameObject mainMoniterCanvas;
    // Start is called before the first frame update
    public void GoSetting()
    {
        Option_Canvas.SetActive(true);
        mainMoniterCanvas.SetActive(false);

    }
    public void GoGame()
    {
        Option_Canvas.SetActive(false);
        mainMoniterCanvas.SetActive(true);
    }
   
}
