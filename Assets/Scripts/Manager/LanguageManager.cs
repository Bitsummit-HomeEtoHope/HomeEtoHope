using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LanguageManager : MonoBehaviour
{
    public TMPro.TMP_FontAsset fontENJP; // Font for English and Japanese
    public TMPro.TMP_FontAsset fontCNTW; // Font for Chinese (Simplified) and Chinese (Traditional)

    private void Start()
    {
        // Load the default language (e.g., English)
        SetLanguage("en");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetLanguage("ja"); // Japanese
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetLanguage("en"); // English
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetLanguage("zh-Hans"); // Chinese (Simplified)
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetLanguage("zh-Hant"); // Chinese (Traditional)
        }
    }

    public void SetLanguage(string languageCode)
    {
        // Set the selected locale based on the language code
        LocalizationSettings.SelectedLocale = GetLocaleFromLanguageCode(languageCode);

        // Update the UI text font based on the selected locale
        UpdateUITextFont(languageCode);
    }

    private Locale GetLocaleFromLanguageCode(string languageCode)
    {
        var availableLocales = LocalizationSettings.AvailableLocales;
        foreach (var locale in availableLocales.Locales)
        {
            if (locale.Identifier.Code == languageCode)
            {
                return locale;
            }
        }
        return null;
    }

    private void UpdateUITextFont(string languageCode)
    {
        TMPro.TMP_FontAsset selectedFont = null;

        // Determine the font to use based on the language code
        if (languageCode == "ja" || languageCode == "en") // Japanese or English
        {
            selectedFont = fontENJP;
        }
        else // Chinese (Simplified) or Chinese (Traditional)
        {
            selectedFont = fontCNTW;
        }

        // Set the font for all Text (TMP) components
        var uiTextList = FindObjectsOfType<TextMeshProUGUI>();
        if (selectedFont != null)
        {
            foreach (TextMeshProUGUI text in uiTextList)
            {
                text.font = selectedFont;
            }
        }
    }
}
