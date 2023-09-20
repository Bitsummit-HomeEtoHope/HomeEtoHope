using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text tutorialTextUI;
    public TextManager textManager; // TextManagerを格納する変数
    public float displayDuration = 10.0f; // テキストが表示される秒数
    private int currentTextIndex = 0;
    public FadeController fadeController; // FadeControllerを格納する変数
    public string[] tutorialTexts = new string[]
    {
        "お前さんが今日から働く新人さんかい",
        "楽な仕事さ、俺が全部教えてやる",
        "今からベルトコンベアで物を流してやる、それで練習しな",
    };

    public GameObject[] objectsToHide; // フェードアウト時に非表示にするオブジェクトを指定

    void Start()
    {


        // 最初のテキストを表示します
        tutorialTextUI.text = tutorialTexts[currentTextIndex];

        // 一定時間ごとにテキストを自動で切り替えるコルーチンを開始します
        StartCoroutine(StartAutoDisplay());
    }

    System.Collections.IEnumerator StartAutoDisplay()
    {
        while (currentTextIndex < tutorialTexts.Length)
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

                // フェードアウトを開始
                StartCoroutine(fadeController.FadeOut());

                textManager.TextStart();
                
                // テキストが最後まで表示された後に実行される処理
                foreach (GameObject obj in objectsToHide)
                {
                    obj.SetActive(false); // オブジェクトを非表示にする
                }
            }
        }
    }
}
