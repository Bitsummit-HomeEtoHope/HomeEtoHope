using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

public class timewellout : MonoBehaviour
{
    [Header("Slow switch")]

    [SerializeField] public bool needSlow = false;
    [Header("-----------")]

    private RectTransform rectTransform;
    private float elapsedTime;
    private float targetPosX;
    private float startPosX;

    [SerializeField]private int duration ;
    public float increaseAmount = 150f;
    public float recoveryDuration = 0.5f; // �ָ�ʱ��

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

    private bool isRecovering; // �Ƿ��ڻָ���
    private float recoveryElapsedTime; // �ָ����ŵ�ʱ��
    private Vector2 originalPosition; // ԭʼλ��


    private void OnEnable()
    {


        Debug.Log("--------------------------");
        Debug.Log(levelDataCurrent._levelTime);
        duration = levelDataCurrent._levelTime;
        Debug.Log(duration);
        Debug.Log("--------------------------");
    }

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

        float fillSpeed = 1f / duration; // ÿ�������ٶ�

        float fillAmount = fillImage.fillAmount - fillSpeed * Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount); // �������������0��1֮��

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

                if (elapsedTime >= duration - 1.333f && elapsedTime <= duration - 0.1f && isEndDaySound)
                {
                    // �ڵ���ʱ��ʣ��1.5��ʱ���ı�ʱ������Ϊ0.5
                    Time.timeScale = 0.5f;

                    // ��������
                    a.GetComponent<AudioSource>().PlayOneShot(b1);
                    isEndDaySound = false;
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
                isEndDaySound = true;
            }
            else
            {
                a.GetComponent<AudioSource>().PlayOneShot(b1);
            }
            // play sound

            // ��ʱ���������� DaysManager �� daysChange ����
            daysManager.daysChange();

            isRecovering = true;
            recoveryElapsedTime = 0f;
            isPlaySound = true;

        }
    }

    private void SunReset()
    {
        float fillSpeed = 1f / recoveryDuration; // ÿ�������ٶ�

        float fillAmount = fillImage.fillAmount + fillSpeed * Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount); // �������������0��1֮��

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
