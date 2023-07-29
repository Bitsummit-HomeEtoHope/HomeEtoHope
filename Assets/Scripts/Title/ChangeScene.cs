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
    public Button hardButton; //level2 button
    public Image redImage; 

    private float pressDuration = 3f; 
    private bool isPressing = false; 
    private float pressStartTime; 

    private bool isPressed = false; 
    private float timer = 0f;

    private GameObject goplaySingle;

    private void Start()
    {
        goplaySingle = GameObject.Find("OpenSingleScene");
        hardButton.onClick.AddListener(OnButtonPressed);
    }

    private void Update()
    {
        if (isPressed)
        {
            timer += Time.deltaTime;

            if (timer >= 3f)
            {
                if(goplaySingle == null)
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

    //go to [Level1]
    public void PlayGame()
    {
        if (!isPressing)
        {
            if (goplaySingle == null)
            {
                SceneManager.LoadScene("Level1");
            }
            else
            {
                SceneManager.LoadScene("OneScene");

            }
        }
    }
    //Exit Game
    public void ExitGame()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null && hardButton != null)
            OUT();

        EndGame();
    }

    //go to [TellYou],tell you How to play
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
            StartCoroutine(StartLongPressTimer());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter == hardButton.gameObject)
        {
            redImage.fillAmount = 0f;

            isPressing = false;

            StopCoroutine(StartLongPressTimer());
        }
    }

    //press 3 secend then chage to the [Level2]
    private IEnumerator StartLongPressTimer()
    {
        float elapsedTime = 0f;

        pressStartTime = Time.time;
        isPressing = true;

        while (isPressing)
        {
            elapsedTime = Time.time - pressStartTime;

            float fillAmount = Mathf.Clamp01(elapsedTime / pressDuration);
            redImage.fillAmount = fillAmount;

            if (elapsedTime >= pressDuration)
            {
                SceneManager.LoadScene("Level2");
                break;
            }

            yield return null;
        }

        redImage.fillAmount = 0f;
    }

    public void OurList()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null && hardButton != null)
            OUT();

        SceneManager.LoadScene("OurList");
    }


    //Exit Game in Windows
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

        isPressing = false;
    }
}
