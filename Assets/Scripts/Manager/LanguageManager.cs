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

    public List<TextMeshProUGUI> uiTextList; // 用于存储Text (TMP) 组件列表
    public TMPro.TMP_FontAsset fontENJP; // 用于英语和日语的字体
    public TMPro.TMP_FontAsset fontCNTW; // 用于中文和台湾的字体

    private void Awake()
    {
        // 第一步：寻找目标文件夹并根据其命名修改游戏物体名和字体
        string currentFolderName = GetCurrentFolderName();
        if (IsValidFolderName(currentFolderName))
        {
            SetLanguageName(currentFolderName);
            UpdateUITextFont(currentFolderName);
            UpdateLanguage(currentFolderName);
        }
        else
        {
            Debug.LogWarning("当前文件夹名称无效，游戏物体名称和字体未更改。");
        }
    }

    private void Update()
    {
        // 第一步：根据按键更改本地的语言以及字体
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
        // 第三步：重命名目标文件夹
        string currentFolderName = GetCurrentFolderName();
        if (currentFolderName != newName && IsValidFolderName(newName))
        {
            string newPath = Path.Combine(folderPath, newName);
            // 进行文件夹的重命名
            Directory.Move(Path.Combine(folderPath, currentFolderName), newPath);

            // 更新游戏物体名称和字体
            SetLanguageName(newName);
            UpdateUITextFont(newName);
            UpdateLanguage(newName);
        }
    }

    private bool IsValidFolderName(string name)
    {
        return System.Array.Exists(validFolderNames, element => element == name);
    }

    private void UpdateLanguage(string folderName)
    {
        int code = 2; // Default to English (EN)

        if (folderName == "1")
        {
            code = 3; // Japanese (JP)
        }
        else if (folderName == "2")
        {
            code = 0; // Chinese (Simplified) (CN)
        }
        else if (folderName == "3")
        {
            code = 2; // English (EN)
        }
        else if (folderName == "4")
        {
            code = 1; // Chinese (Traditional) (TW)
        }

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

        // 检查场景中是否存在对应的游戏物体名称，根据名称选择要使用的字体
        if (folderName == "1" || folderName == "3") // 日语或英语
        {
            selectedFont = fontENJP;
        }
        else if (folderName == "2" || folderName == "4") // 简体中文或繁体中文
        {
            selectedFont = fontCNTW;
        }

        // 设置所有Text (TMP) 组件的字体
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
        // 获取文件夹列表
        string[] folders = Directory.GetDirectories(folderPath);

        // 遍历所有文件夹
        foreach (string folder in folders)
        {
            string folderName = Path.GetFileName(folder);
            // 如果文件夹的名称是有效的，则返回该名称
            if (IsValidFolderName(folderName))
            {
                return folderName;
            }
        }

        // 如果没有找到有效的文件夹，则返回空字符串
        return string.Empty;
    }

    private void SetLanguageName(string languageName)
    {
        if (languageObject != null)
        {
            languageObject.name = languageName;
        }
    }

    // 添加其他功能的方法
    private void SomeOtherFunction()
    {
        // 在这里添加其他功能的实现代码
    }
}
