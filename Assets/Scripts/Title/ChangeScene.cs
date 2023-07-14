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

    private bool isPressing = false; // ��¼��ť�Ƿ��ڰ���״̬
    private float pressStartTime; // ��¼���°�ť����ʼʱ��

    private void Start()
    {
        hardButton.onClick.AddListener(OnButtonPressed);
    }

    private void Update()
    {
        if (isPressing)
        {
            float elapsedTime = Time.time - pressStartTime;
            float fillAmount = Mathf.Clamp01(elapsedTime / 3f);
            redImage.fillAmount = fillAmount;

            if (elapsedTime >= 3f)
            {
                SceneManager.LoadScene("Level2");
            }
        }
    }

    private void OnButtonPressed()
    {
        isPressing = true;
        pressStartTime = Time.time;
    }

    private void OnButtonReleased()
    {
        isPressing = false;
        redImage.fillAmount = 0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerEnter == hardButton.gameObject)
        {
            // ��ʼ������ʱ
            StartCoroutine(LongPressTimer());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter == hardButton.gameObject)
        {
            // �ɿ���ť�����ú�ɫImage��FillAmountΪ0
            redImage.fillAmount = 0f;

            // ֹͣ��ʱ��Э��
            StopCoroutine(LongPressTimer());
        }
    }

    private IEnumerator LongPressTimer()
    {
        yield return new WaitForSeconds(3f);

        if (isPressing)
        {
            SceneManager.LoadScene("Level2");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        EndGame();
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("TellYou");
    }

    public void OurList()
    {
        SceneManager.LoadScene("OurList");
    }

    private void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
    }
}
