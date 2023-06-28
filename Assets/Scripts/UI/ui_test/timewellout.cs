using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class timewellout : MonoBehaviour
{
    private RectTransform rectTransform;
    private float elapsedTime;
    private float targetPosX;
    private float startPosX;

    public float duration = 90f;
    public float increaseAmount = 150f;
    public float recoveryDuration = 0.5f; // 恢复时间

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

    private bool isRecovering; // 是否在恢复中
    private float recoveryElapsedTime; // 恢复流逝的时间
    private Vector2 originalPosition; // 原始位置

    private void Start()
    {
        levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        daysManager = FindObjectOfType<DaysManager>();

        rectTransform = GetComponent<RectTransform>();
        startPosX = rectTransform.anchoredPosition.x;
        targetPosX = startPosX + increaseAmount;
        elapsedTime = 0f;

        originalPosition = rectTransform.anchoredPosition;
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
        float fillSpeed = 1f / duration; // 每秒填充的速度

        float fillAmount = fillImage.fillAmount - fillSpeed * Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount); // 将填充量限制在0到1之间

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
            if(elapsedTime >= duration - 3 && isPlaySound){
                a.GetComponent<AudioSource>().PlayOneShot(b2);
                isPlaySound = false;
            }
        }
        else
        {
            // play sound
            a.GetComponent<AudioSource>().PlayOneShot(b1);

            // 计时结束后启用 DaysManager 的 daysChange 方法
            daysManager.daysChange();

            isRecovering = true;
            recoveryElapsedTime = 0f;
            isPlaySound = true;
        }
    }

    private void SunReset()
    {
        float fillSpeed = 1f / recoveryDuration; // 每秒填充的速度

        float fillAmount = fillImage.fillAmount + fillSpeed * Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount); // 将填充量限制在0到1之间

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
