using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LanguageManager : MonoBehaviour
{
    public List<TextMeshProUGUI> TxT_List;

    public TMPro.TMP_FontAsset fontENJP; // Font for English and Japanese
    public TMPro.TMP_FontAsset fontCNTW; // Font for Chinese (Simplified) and Chinese (Traditional)

    [Header("Language Change")]
    public Button btnJa;
    public Button btnEn;
    public Button btnZhCN;
    public Button btnZhTW;
    private GameObject titleScene;

    private void Start()
    {
        titleScene = GameObject.Find("HereTitle");
        // Load the default language (e.g., English)
        if(titleScene!=null)SetLanguage("en");

        // Bind button click events
        if(btnJa!=null)btnJa.onClick.AddListener(() => SetLanguage("ja"));
        if(btnEn!=null)btnEn.onClick.AddListener(() => SetLanguage("en"));
        if(btnZhCN!=null)btnZhCN.onClick.AddListener(() => SetLanguage("zh-CN"));
        if(btnZhTW!=null)btnZhTW.onClick.AddListener(() => SetLanguage("zh-TW"));
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
            SetLanguage("zh-CN"); // Chinese (Simplified)
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetLanguage("zh-TW"); // Chinese (Traditional)
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

        // Set the font for all Text (TMP) components in the TxT_List
        if (selectedFont != null)
        {
            foreach (TextMeshProUGUI text in TxT_List)
            {
                text.font = selectedFont;
            }
        }
    }
}
