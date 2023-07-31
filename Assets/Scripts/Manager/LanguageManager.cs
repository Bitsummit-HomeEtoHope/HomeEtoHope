using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;
using UnityEngine.Localization.Settings;
using System;

public class LanguageManager : MonoBehaviour
{
    private GameObject languageObject;
    private string folderPath = "Assets/Log";
    private string[] validFolderNames = { "1", "2", "3", "4" };

    public List<TextMeshProUGUI> uiTextList; // ���ڴ洢Text (TMP) ����б�
    public TMPro.TMP_FontAsset fontENJP; // ����Ӣ������������
    public TMPro.TMP_FontAsset fontCNTW; // �������ĺ�̨�������

    private IEnumerator Start()
    {
        // ��һ����Ѱ��Ŀ���ļ��в������������޸���Ϸ������������
        string currentFolderName = GetCurrentFolderName();
        if (IsValidFolderName(currentFolderName))
        {
            SetLanguageName(currentFolderName);
            UpdateUITextFont(currentFolderName);
            yield return StartCoroutine(UpdateLanguage(currentFolderName));
        }
        else
        {
            Debug.LogWarning("��ǰ�ļ���������Ч����Ϸ�������ƺ�����δ���ġ�");
        }
    }

    private void Update()
    {
        // ��һ�������ݰ������ı��ص������Լ�����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateFolderName("1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateFolderName("2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateFolderName("3");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateFolderName("4");
        }
    }

    private void UpdateFolderName(string newName)
    {
        // ��������ɾ��Ŀ���ļ��У�������ڣ�
        string currentFolderName = GetCurrentFolderName();
        string currentFolderPath = Path.Combine(folderPath, currentFolderName);

        if (Directory.Exists(currentFolderPath))
        {
            Directory.Delete(currentFolderPath, true);
        }

        // ���Ĳ���ɾ��Assets/LogĿ¼�������ļ���
        string[] folders = Directory.GetDirectories(folderPath);
        foreach (string folder in folders)
        {
            Directory.Delete(folder, true);
        }

        // ���岽�����ݰ����½��ļ���
        string newFolderPath = Path.Combine(folderPath, newName);
        Directory.CreateDirectory(newFolderPath);

        // ������Ϸ�������ƺ�����
        SetLanguageName(newName);
        UpdateUITextFont(newName);
        StartCoroutine(UpdateLanguage(newName));
    }

    private bool IsValidFolderName(string name)
    {
        return System.Array.Exists(validFolderNames, element => element == name);
    }

    private IEnumerator UpdateLanguage(string folderName)
    {
        int code;

        if (folderName == "1")
        {
            code = 3; // ����
        }
        else if (folderName == "2")
        {
            code = 0; // ��������
        }
        else if (folderName == "3")
        {
            code = 2; // Ӣ��
        }
        else if (folderName == "4")
        {
            code = 1; // ��������
        }
        else
        {
            Debug.LogWarning("Invalid folder name: " + folderName);
            yield break;
        }

        yield return LocalizationSettings.InitializationOperation;

        if (code >= 0 && code < LocalizationSettings.AvailableLocales.Locales.Count)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[code];
        }
        else
        {
            Debug.LogWarning("Invalid language code: " + code);
        }
    }

    private void UpdateUITextFont(string folderName)
    {
        TMPro.TMP_FontAsset selectedFont = null;

        // ��鳡�����Ƿ���ڶ�Ӧ����Ϸ�������ƣ���������ѡ��Ҫʹ�õ�����
        if (folderName == "1" || folderName == "3") // �����Ӣ��
        {
            selectedFont = fontENJP;
        }
        else if (folderName == "2" || folderName == "4") // �������Ļ�������
        {
            selectedFont = fontCNTW;
        }

        // ��������Text (TMP) ���������
        if (selectedFont != null)
        {
            foreach (TextMeshProUGUI text in uiTextList)
            {
                text.font = selectedFont;
            }
        }
    }

    private string GetCurrentFolderName()
    {
        // ��ȡ�ļ����б�
        string[] folders = Directory.GetDirectories(folderPath);

        // ���������ļ���
        foreach (string folder in folders)
        {
            string folderName = Path.GetFileName(folder);
            // ����ļ��е���������Ч�ģ��򷵻ظ�����
            if (IsValidFolderName(folderName))
            {
                return folderName;
            }
        }

        // ���û���ҵ���Ч���ļ��У��򷵻ؿ��ַ���
        return string.Empty;
    }

    private void SetLanguageName(string languageName)
    {
        if (languageObject != null)
        {
            languageObject.name = languageName;
        }
    }

    // ����������ܵķ���
    private void SomeOtherFunction()
    {
        // ����������������ܵ�ʵ�ִ���
    }
}
