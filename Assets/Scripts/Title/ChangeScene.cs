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
    public Image redImage; // ָ���ĺ�ɫImage

    private float pressDuration = 3f; // �����ĳ���ʱ��
    private bool isPressing = false; // ��¼��ť�Ƿ��ڰ���״̬
    private float pressStartTime; // ��¼���°�ť����ʼʱ��

    private bool isPressed = false; // ��ť�Ƿ񱻰���
    private float timer = 0f; // ��ʱ��

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
            // ִ�е����л��������߼�
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
            // ��ʼ������ʱ��Э��
            StartCoroutine(StartLongPressTimer());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter == hardButton.gameObject)
        {
            // �ɿ���ť�����ú�ɫImage��FillAmountΪ0
            redImage.fillAmount = 0f;

            // ���ó���״̬
            isPressing = false;

            // ֹͣ��ʱ��Э��
            StopCoroutine(StartLongPressTimer());
        }
    }

    private IEnumerator StartLongPressTimer()
    {
        float elapsedTime = 0f; // ��¼������ʱ��

        // ��¼���°�ť����ʼʱ��
        pressStartTime = Time.time;
        isPressing = true;

        while (isPressing)
        {
            elapsedTime = Time.time - pressStartTime;

            // ���º�ɫImage��FillAmount
            float fillAmount = Mathf.Clamp01(elapsedTime / pressDuration);
            redImage.fillAmount = fillAmount;

            // ��������ĳ���ʱ�䳬��Ԥ���ʱ�䣬ִ�г����л��������߼�
            if (elapsedTime >= pressDuration)
            {
                // ִ�г����л��������߼�
                SceneManager.LoadScene("Level2");
                break;
            }

            yield return null;
        }

        // �ɿ���ť�����ú�ɫImage��FillAmountΪ0
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

        // ���ó���״̬
        isPressing = false;
    }
}
