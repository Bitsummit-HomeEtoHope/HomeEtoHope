using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OpsionOpen : MonoBehaviour
{
    //設定用のcanvas
    public GameObject Option_Canvas;
    //設定用の画面に移行するボタン
    public GameObject settingButton;
    //
    public GameObject titleScreen;

    public GameObject mainMoniterCanvas;
    //ゲームを一時停止するかどうかのフラグ
    private static bool isOpened = false;
    public static bool isGamePaused { get { return isOpened; } }

    private TimeManager timeManager;

    private void Awake()
    {
        isOpened = false;
        timeManager = FindObjectOfType<TimeManager>();
    }

    private void OnDestroy()
    {
        isOpened = false;
    }

    /*private void Update()
     {
         //オプションパネルがアクティブな間はゲームを一時停止する
         if (gameObject.activeSelf)
         {
             PauseGame();
         }
         else
         {
             ResumeGame();
         }

     }
     //ゲームを一時停止するメソッド
     private void PauseGame()
     {
         if(!isGamePaused)
         {
             Time.timeScale =0f;
             isGamePaused = true;
         }
     }
     //ゲームを再開するメソッド
     private void ResumeGame()
     {
         if (isGamePaused)
         {
             Time.timeScale = 1f;
             isGamePaused=false; 
         }
     }*/
    // Start is called before the first frame update
    public void GoSetting()
    {
        if(mainMoniterCanvas!=null)mainMoniterCanvas.SetActive(false);
        Option_Canvas.SetActive(true );
        //mainMoniterCanvas.SetActive(false);

        //---the world----
        if (timeManager != null)
        {
            timeManager.enabled = true; 
        }
        //----------------

        isOpened = true;

    }
    public void GoGame()
    {
        Option_Canvas.SetActive(false);
        if (mainMoniterCanvas != null) mainMoniterCanvas.SetActive(true);

        //---free the time---
        if (timeManager != null)
        {
            timeManager.enabled = false;
        }
        //--------------------


        isOpened = false;
    }
    public void GoTitle()
    {
        //                       　↓変更したいscene名いれる
        SceneManager.LoadScene("Title");
    }
   
}
