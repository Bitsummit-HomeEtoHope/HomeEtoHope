using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("System")]
    public Button playButton;
    public Button exitButton;
    public Button howButton;
    public Button listButton;
    public Button hardButton;
    public Image redImage; // 指定的红色Image

    private float pressDuration = 3f; // 长按的持续时间
    private bool isPressing = false; // 记录按钮是否处于按下状态
    private float pressStartTime; // 记录按下按钮的起始时间

    private bool isPressed = false; // 按钮是否被按下
    private float timer = 0f; // 计时器

    private void Start()
    {
        hardButton.onClick.AddListener(OnButtonPressed);
    }

    private void Update()
    {
        if (isPressed)
        {
            timer += Time.deltaTime;

            if (timer >= 3f)
            {
                SceneManager.LoadScene("Level2");
            }
        }
    }

    private void OnButtonPressed()
    {
        isPressed = true;
        timer = 0f;
    }

    private void OnButtonReleased()
    {
        isPressed = false;
        timer = 0f;
    }

    private void Awake()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null && hardButton != null)
            IN();
    }

    public void PlayGame()
    {
        if (!isPressing)
        {
            // 执行单击切换场景的逻辑
            SceneManager.LoadScene("Level1");
        }
    }

    public void ExitGame()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null && hardButton != null)
            OUT();

        EndGame();
    }

    public void HowToPlay()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null && hardButton != null)
            OUT();

        SceneManager.LoadScene("TellYou");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter == hardButton.gameObject)
        {
            // 开始长按计时的协程
            StartCoroutine(StartLongPressTimer());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter == hardButton.gameObject)
        {
            // 松开按钮后重置红色Image的FillAmount为0
            redImage.fillAmount = 0f;

            // 重置长按状态
            isPressing = false;

            // 停止计时器协程
            StopCoroutine(StartLongPressTimer());
        }
    }

    private IEnumerator StartLongPressTimer()
    {
        float elapsedTime = 0f; // 记录经过的时间

        // 记录按下按钮的起始时间
        pressStartTime = Time.time;
        isPressing = true;

        while (isPressing)
        {
            elapsedTime = Time.time - pressStartTime;

            // 更新红色Image的FillAmount
            float fillAmount = Mathf.Clamp01(elapsedTime / pressDuration);
            redImage.fillAmount = fillAmount;

            // 如果长按的持续时间超过预设的时间，执行长按切换场景的逻辑
            if (elapsedTime >= pressDuration)
            {
                // 执行长按切换场景的逻辑
                SceneManager.LoadScene("Level2");
                break;
            }

            yield return null;
        }

        // 松开按钮后重置红色Image的FillAmount为0
        redImage.fillAmount = 0f;
    }

    public void OurList()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null && hardButton != null)
            OUT();

        SceneManager.LoadScene("OurList");
    }

    private void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        QuitGame();
#endif
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void IN()
    {
        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        howButton.onClick.AddListener(HowToPlay);
        listButton.onClick.AddListener(OurList);
    }

    private void OUT()
    {
        playButton.onClick.RemoveListener(PlayGame);
        exitButton.onClick.RemoveListener(ExitGame);
        howButton.onClick.RemoveListener(HowToPlay);
        listButton.onClick.RemoveListener(OurList);

        // 重置长按状态
        isPressing = false;
    }
}
