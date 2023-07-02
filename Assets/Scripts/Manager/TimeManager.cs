using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // 控制暂停状态
    [SerializeField] public List<GameObject> targetObjects; // 多个游戏对象
    [Header("JoJo")]
    [SerializeField] private string codeHolderScriptName = "CodeHolder";
    [SerializeField] private string rotateScriptName = "Rotate";
    [SerializeField] private List<Graphic> uiElementsToDisable;


    private void OnEnable()
    {
        Pause();
    }

    private void OnDisable()
    {
        Resume();
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        // 禁用指定UI元素的交互功能和可见性
        foreach (Graphic uiElement in uiElementsToDisable)
        {
            uiElement.raycastTarget = false;
            Color color = uiElement.color;
            color.a = 0f;
            uiElement.color = color;
        }

        // 禁用 CodeHolder 脚本
        CodeHolder codeHolder = FindObjectOfType<CodeHolder>();
        if (codeHolder != null)
            codeHolder.enabled = false;

        // 禁用 Rotate 脚本
        Rotate rotate = FindObjectOfType<Rotate>();
        if (rotate != null)
            rotate.enabled = false;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // 启用指定UI元素的交互功能和可见性
        foreach (Graphic uiElement in uiElementsToDisable)
        {
            uiElement.raycastTarget = true;
            Color color = uiElement.color;
            color.a = 1f;
            uiElement.color = color;
        }

        // 启用 CodeHolder 脚本
        CodeHolder codeHolder = FindObjectOfType<CodeHolder>();
        if (codeHolder != null)
            codeHolder.enabled = true;

        // 启用 Rotate 脚本
        Rotate rotate = FindObjectOfType<Rotate>();
        if (rotate != null)
            rotate.enabled = true;
    }

}
