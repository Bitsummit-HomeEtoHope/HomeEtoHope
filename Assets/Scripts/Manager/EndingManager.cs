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

    private GameObject endingSingle;
    // Update is called once per frame

    void Start()
    {
        endingSingle = GameObject.Find("Ending_single");
        thankyou = FindObjectOfType<ThankYou>();

        _button = GameObject.Find("Button");

        if(SkipButton!=null)SkipButton.onClick.AddListener(SkipAction);

        if (ContinueButton != null) ContinueButton.onClick.AddListener(ContinueAction);


        if (_RestButton != null) _RestButton.onClick.AddListener(_RestGame);

        if (_TitleButton != null) _TitleButton.onClick.AddListener(_TitleGame);
        if (_Button != null) _Button.onClick.AddListener(_);

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
        if (endingSingle == null)
        {
            SceneManager.LoadScene("Title");
        }
        else
        {
            SceneManager.LoadScene("Title_single");
        }
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


        
    
    //exit Game

    void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        QuitGame();
#endif
    }

    void QuitGame()
    {
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
  
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("NextScene");
        } 
 
 */
