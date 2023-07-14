using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class timewellout : MonoBehaviour
{
    [Header("Slow switch")]
    [SerializeField] public bool needSlow = false;
    [Header("-----------")]

    private RectTransform rectTransform;
    private float elapsedTime;
    private float targetPosX;
    private float startPosX;

    [SerializeField] private int duration;
    public float increaseAmount = 150f;
    public float recoveryDuration = 0.5f; // Recovery time

    public LevelDataCurrent levelDataCurrent;
    public DaysManager daysManager;

    private int currentTime = 0;
    private int isChangeBefore = 0;
    private int isChangeAfter = 0;

    [SerializeField] private Image fillImage;

    [SerializeField] private AudioSource a;

    [SerializeField] private AudioClip b1;
    [SerializeField] private AudioClip b2;

    private bool isPlaySound = true;
    private bool isEndDaySound = true;

    private bool isRecovering; // Is recovering
    private float recoveryElapsedTime; // Elapsed time during recovery
    private Vector2 originalPosition; // Original position
    [Header("List Item Line Button Monitor")]
    public List<GameObject> endDayOff;
    public Image watingImage;
    private float waitngSpeed;
    private float waitngfill;
    private float waitngTime;

    private void Start()
    {
        levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        daysManager = FindObjectOfType<DaysManager>();

        duration = levelDataCurrent._levelTime;

        rectTransform = GetComponent<RectTransform>();
        startPosX = rectTransform.anchoredPosition.x;
        targetPosX = startPosX + increaseAmount;
        elapsedTime = 0f;

        originalPosition = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        duration = levelDataCurrent._levelTime;
    }

    private void Update()
    {
        if (isRecovering)
        {
            SunReset();
        }
        else
        {
            SunDown();
        }
    }

    private void SunDown()
    {
        float fillSpeed = 1f / duration;

        float fillAmount = fillImage.fillAmount - fillSpeed * Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount);

        if (Time.time - currentTime >= 1)
        {
            currentTime = (int)(Time.time % duration);
            isChangeAfter = (int)(Time.time / duration);
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime <= duration)
        {
            float t = elapsedTime / duration;
            float currentPosX = Mathf.Lerp(startPosX, targetPosX, t);
            rectTransform.anchoredPosition = new Vector2(currentPosX, rectTransform.anchoredPosition.y);

            if (needSlow)
            {
                if (elapsedTime >= duration - 5 && isPlaySound)
                {
                    a.GetComponent<AudioSource>().PlayOneShot(b2);
                    isPlaySound = false;
                }

                if (elapsedTime >= duration - 1.333f && elapsedTime <= duration - 1f && isEndDaySound)
                {
                    Time.timeScale = 0.5f;

                    a.GetComponent<AudioSource>().PlayOneShot(b1);
                    isEndDaySound = false;
                }
                if (elapsedTime >= duration - 1f && elapsedTime <= duration - 0.1f)
                {
                    //-----turn off the list in endDayOff-----

                    waitngSpeed = 1f / 4 * levelDataCurrent._endDaytime;

                    waitngfill = watingImage.fillAmount + waitngSpeed * Time.deltaTime;
                    watingImage.fillAmount = Mathf.Clamp01(waitngfill);

                    foreach (GameObject item in endDayOff)
                    {
                        item.SetActive(false);
                    }

                    //----------

                    Time.timeScale = 1 / levelDataCurrent._endDaytime;
                }
            }
            else
            {
                if (elapsedTime >= duration - 3 && isPlaySound)
                {
                    a.GetComponent<AudioSource>().PlayOneShot(b2);
                    isPlaySound = false;
                }
            }
        }
        else
        {
            if (needSlow)
            {
                Time.timeScale = 1f;
                //--------------------------------

                watingImage.fillAmount = Mathf.Clamp01(0);

                foreach (GameObject item in endDayOff)
                {
                    item.SetActive(true);
                }

                //--------------------------------

                isEndDaySound = true;
            }
            else
            {
                a.GetComponent<AudioSource>().PlayOneShot(b1);
            }

            // play sound
            daysManager.daysChange();

            isRecovering = true;
            recoveryElapsedTime = 0f;
            isPlaySound = true;
        }
    }

    private void SunReset()
    {
        float fillSpeed = 1f / recoveryDuration; // Fill speed per second

        float fillAmount = fillImage.fillAmount + fillSpeed * Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount);

        if (Time.time - currentTime >= 1)
        {
            currentTime = (int)(Time.time % recoveryDuration);
            isChangeAfter = (int)(Time.time / recoveryDuration);
        }

        recoveryElapsedTime += Time.deltaTime;

        if (recoveryElapsedTime <= recoveryDuration)
        {
            float t = recoveryElapsedTime / recoveryDuration;
            float currentPosX = Mathf.Lerp(targetPosX, startPosX, t);
            rectTransform.anchoredPosition = new Vector2(currentPosX, rectTransform.anchoredPosition.y);
        }
        else
        {
            isRecovering = false;
            elapsedTime = 0f;
        }
    }
}
