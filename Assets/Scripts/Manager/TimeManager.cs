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
    [SerializeField] private bool isPaused = false; // ������ͣ״̬
    [SerializeField] public List<GameObject> targetObjects; // �����Ϸ����
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

        // ����ָ��UIԪ�صĽ������ܺͿɼ���
        foreach (Graphic uiElement in uiElementsToDisable)
        {
            uiElement.raycastTarget = false;
            Color color = uiElement.color;
            color.a = 0f;
            uiElement.color = color;
        }

        // ���� CodeHolder �ű�
        CodeHolder codeHolder = FindObjectOfType<CodeHolder>();
        if (codeHolder != null)
            codeHolder.enabled = false;

        // ���� Rotate �ű�
        Rotate rotate = FindObjectOfType<Rotate>();
        if (rotate != null)
            rotate.enabled = false;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        // ����ָ��UIԪ�صĽ������ܺͿɼ���
        foreach (Graphic uiElement in uiElementsToDisable)
        {
            uiElement.raycastTarget = true;
            Color color = uiElement.color;
            color.a = 1f;
            uiElement.color = color;
        }

        // ���� CodeHolder �ű�
        CodeHolder codeHolder = FindObjectOfType<CodeHolder>();
        if (codeHolder != null)
            codeHolder.enabled = true;

        // ���� Rotate �ű�
        Rotate rotate = FindObjectOfType<Rotate>();
        if (rotate != null)
            rotate.enabled = true;
    }

}
