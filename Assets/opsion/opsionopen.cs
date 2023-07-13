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
    //�ݒ�p��canvas
    public GameObject Option_Canvas;
    //�ݒ�p�̉�ʂɈڍs����{�^��
    public GameObject settingButton;
    //
    public GameObject titleScreen;

    public GameObject mainMoniterCanvas;
    //�Q�[�����ꎞ��~���邩�ǂ����̃t���O
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
         //�I�v�V�����p�l�����A�N�e�B�u�ȊԂ̓Q�[�����ꎞ��~����
         if (gameObject.activeSelf)
         {
             PauseGame();
         }
         else
         {
             ResumeGame();
         }

     }
     //�Q�[�����ꎞ��~���郁�\�b�h
     private void PauseGame()
     {
         if(!isGamePaused)
         {
             Time.timeScale =0f;
             isGamePaused = true;
         }
     }
     //�Q�[�����ĊJ���郁�\�b�h
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
        //                       �@���ύX������scene�������
        SceneManager.LoadScene("Title");
    }
   
}
