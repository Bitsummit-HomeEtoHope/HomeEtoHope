using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    
    public Text tutorialTextUI;
    public float displayDuration = 3.0f; // テキストが表示される秒数
    private int currentTextIndex = 0;
    private bool shouldContinue = true;

    public string[] tutorialTexts = new string[]
    {
        "まずは食べ物からだな",
        "りんごだな、見ればわかるか。",
        "左下のモニターにサンプルが映ってある。",
        "それと同じならそのまま流していい。",
    };

    public void TextStart()
    {
        // 最初のテキストを表示します
        tutorialTextUI.text = tutorialTexts[currentTextIndex];

        // テキスト送りの開始
        StartCoroutine(StartAutoDisplay());
    }

    System.Collections.IEnumerator StartAutoDisplay()
    {
        while (currentTextIndex < tutorialTexts.Length && shouldContinue)
        {
            // 次のテキストを表示します
            yield return new WaitForSeconds(displayDuration);

            currentTextIndex++;
            if (currentTextIndex < tutorialTexts.Length)
            {
                tutorialTextUI.text = tutorialTexts[currentTextIndex];
            }
            else
            {
                // テキストが最後まで表示された後に実行される処理
            }
        }
    }

    // テキストの表示を途中で止めるメソッド
    public void StopTextDisplay()
    {
        shouldContinue = false;
    }

    // テキストの表示を再開するメソッド
    public void ContinueTextDisplay()
    {
        shouldContinue = true;
        StartCoroutine(StartAutoDisplay());
    }

    // テキストの内容を変えるメソッド
    public void ChangeTextContent(int index)
    {
        if (index >= 0 && index < tutorialTexts.Length)
        {
            currentTextIndex = index;
            tutorialTextUI.text = tutorialTexts[currentTextIndex];
        }
    }
}
