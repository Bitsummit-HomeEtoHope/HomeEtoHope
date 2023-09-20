using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    
    public Text tutorialTextUI;
    public GameObject TutorialPanel;
    public float displayDuration = 3.0f; // テキストが表示される秒数
    private int currentTextIndex = 0;
    private bool shouldContinue = false;

    public string[] tutorialTexts = new string[]
    {
        "まずは食べ物からだな",
        "りんごだな、見ればわかるか。",
        "左下のモニターにサンプルが映ってある。",
        "それと同じならそのまま流していい。",
    };

    public void TextStart()
    {
        TutorialItemManager tutorialItemManager = TutorialItemManager.Instance;

        // 最初のテキストを表示します
        tutorialTextUI.text = tutorialTexts[currentTextIndex];

        // TutorialPanel ゲームオブジェクトをアクティブにする
        TutorialPanel.SetActive(true);

        // shouldContinue を true に設定する
        shouldContinue = true;

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
            if(currentTextIndex == 1)
            {
                tutorialItemManager.InitializeSpecificItem("3D/food/good/apple");
            }

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
