using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class EndingManager : MonoBehaviour
{
    [SerializeField] public GameObject fin;
    [SerializeField] public VideoPlayer fin_meto;
    private ThankYou thankyou;
    [Header("Skip")]
    [SerializeField] public GameObject Skip;
    public Button SkipButton;
    public Button ContinueButton;
    public Button _RestButton;
    public Button _TitleButton;
    public Button _Button;
    private GameObject _button;
    // Update is called once per frame

    void Start()
    {
        thankyou = FindObjectOfType<ThankYou>();

        _button = GameObject.Find("Button");

        SkipButton.onClick.AddListener(SkipAction);

        ContinueButton.onClick.AddListener(ContinueAction);


        _RestButton.onClick.AddListener(_RestGame);

        _TitleButton.onClick.AddListener(_TitleGame);
        _Button.onClick.AddListener(_);

    }

    //private void OnDisable()
    //{
    //    SkipButton.onClick.RemoveListener(SkipAction);

    //    ContinueButton.onClick.RemoveListener(ContinueAction);

    //    _TitleButton.onClick.RemoveListener(_TitleGame);

    //    _RestButton.onClick.RemoveListener(_RestGame);
    //    _Button.onClick.RemoveListener(_);

    //}

    private void _()
    {
        if(_button.activeSelf)_button.SetActive(false);
        if (thankyou.bgm)
        {
            Time.timeScale = 0f;
            fin_meto.Pause();
            Skip.SetActive(true);
        }
    }

    private void _RestGame()
    {
        SceneManager.LoadScene("Level1");
    }

    private void _TitleGame()
    {
        SceneManager.LoadScene("Title");
    }

    private void SkipAction()
    {
        Time.timeScale = 1.0f;
        thankyou.Skip();
    }

    private void ContinueAction()
    {
        if (!_button.activeSelf) _button.SetActive(true);

        fin_meto.Play();
        if (Skip.activeSelf)
        {
            Time.timeScale = 1f;
            Skip.SetActive(false);
        }

    }


        
    


    void EndGame()
    {
        // 在编辑器中停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在游戏中退出应用程序
        QuitGame();
#endif
    }

    void QuitGame()
    {
        // 退出应用程序
        Application.Quit();
    }

    private void BackTitle()
    {
        SceneManager.LoadScene("Title");
    }


    //                 StartCoroutine(WillSkip());


    //IEnumerator WillSkip()
    //{
    //    yield return StartCoroutine(WantSkip());
    //    Time.timeScale = 0;
    //}

    //IEnumerator WillWatch()
    //{
    //    Time.timeScale = 1;

    //    yield return StartCoroutine(WantWatch());
    //}


    //IEnumerator WantSkip()
    //{
    //    Vector3 startScale = Skip.transform.localScale;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < 0.2f)
    //    {
    //        elapsedTime += Time.unscaledDeltaTime;
    //        float t = Mathf.Clamp01(elapsedTime / 0.2f);
    //        Skip.transform.localScale = Vector3.Lerp(startScale, Vector3.one, t);
    //        yield return null;
    //    }

    //    Skip.transform.localScale = Vector3.one;

    //    Time.timeScale = 0f;
    //}



    //IEnumerator WantWatch()
    //{
    //    Vector3 startScale = Skip.transform.localScale;
    //    float elapsedTime = 0f;

    //    while (elapsedTime < 0.2f)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        float t = Mathf.Clamp01(elapsedTime / 0.2f);
    //        Skip.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
    //        yield return null;
    //    }


    //    Skip.transform.localScale = Vector3.zero;

    //    Time.timeScale = 1f;

    //}





}

/*
  
  // 检测鼠标左击
        if (Input.GetMouseButtonDown(0))
        {
            // 跳转到指定场景（假设场景名称为 "NextScene"）
            SceneManager.LoadScene("NextScene");
        } 
 
 */
