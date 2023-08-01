using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Point_Teacher : MonoBehaviour
{
    [Header("your Ending")]
    public UnityEvent BuildEvent;
    public UnityEvent BuildEvent_1;
    public UnityEvent BuildEvent_2;
    private int buildCount = 0;
    private float buildSpeed;
    public bool endingCheck = false;

    private GameObject endingSingle;
    //public float credits = 0f;
    [Header("will End_I -- numal")]
    public float credits_end_i = 0f;
    [Header("will End_II -- red(bad guys)")]
    public float credits_end_ii = 0f;
    [Header("will End_III -- flower(die)")]
    public float credits_end_iii = 0f;
    [Header("will End_IV -- building")]
    public float credits_end_iv = 0f;
    public bool for_credits_end_iv = false;
    [Header("will End_V -- factory")]
    public float credits_end_v = 0f;
    public bool for_credits_end_v = false;
    [Header("will End_VI -- farm")]
    public float credits_end_vi = 0f;
    public bool for_credits_end_vi = false;
    [Header("--will Pass-- if Bad Ending")]
    public float passScore;
    public float yourScore;

    private Coroutine lineCoroutine;
    private Coroutine markCoroutine;

    [Header("------------------------------")]
    [Header("Ending Line")]
    private Image endLine;
    private Image endMark;
    [SerializeField] private float changePoint;
    [SerializeField] private float changeTime;

    private float lineChanges;
    private float markChanges;
    private bool lineChaging = false;
    private bool markChaging = false;
    private bool isChangingLine = false;
    private bool isChangingMark = false;
    [Header("------------------------------")]


    [Header("Level_states")]
    public LevelDataCurrent levelDataCurrent;
    [SerializeField] private int number_buildend;
    [SerializeField] private int number_humanend;

    private void Update()
    {
        if (endingCheck) totalScore();

        Debug.Log("==+" + buildCount);

        if (buildCount < 0)
        {
            StopCoroutine(lineCoroutine);
            StopCoroutine(markCoroutine);
        }
    }


    public void EventCall()
    {
        BuildEvent.Invoke();
    }
    private void OnEnable()
    {
        // Register to listen to the BuildEvent

        BuildEvent.AddListener(HandleBuildEvent);
        BuildEvent_1.AddListener(SpeedUp);
        BuildEvent_2.AddListener(SpeedBack);


    }


    private void OnDisable()
    {
        // Unregister the listener when the script is disabled or destroyed to avoid memory leaks

        BuildEvent.RemoveListener(HandleBuildEvent);

    }

    private void HandleBuildEvent()
    {
        // Handle the BuildEvent here
        Debug.Log(" +++++welcom here+++++");

        buildWord();
        // You can add your custom logic here based on the BuildEvent.
    }

    void Start()
    {
        endingSingle = GameObject.Find("Play_single");

        BuildEvent.AddListener(HandleBuildEvent);

        //-----level----------------------------------------
        changeTime = levelDataCurrent._changeTime;
        passScore = changePoint = levelDataCurrent._buildcount;
        //  passScore = levelDataCurrent._clearPoints;
        number_buildend = levelDataCurrent._theBuildNumber;
        number_humanend = levelDataCurrent._theHumanNumber;

        //-----ending---------------------------------------
        GameObject EndLine = GameObject.Find("end_line");
        GameObject EndMark = GameObject.Find("end_mark");
        if (EndLine != null) endLine = EndLine.GetComponent<Image>();
        if (EndMark != null) endMark = EndMark.GetComponent<Image>();

        if (endLine != null) Debug.Log("+++++hi+++++");
        if (endMark != null) Debug.Log("+++++ok+++++");


    }

    public void AddPoints(float points, float points_end_i, float points_end_ii, float points_end_iii, float points_end_iv, float points_end_v, float points_end_vi)
    {
        //credits += points;
        credits_end_i += points_end_i;
        credits_end_ii += points_end_ii;
        credits_end_iii += points_end_iii;
        credits_end_iv += points_end_iv;
        credits_end_v += points_end_v;
        credits_end_vi += points_end_vi;

        yourScore = credits_end_i;
        Debug.Log("Points added: " + points + " | Total credits: " + credits_end_i);
    }

    public void totalScore()
    {
        number_buildend = levelDataCurrent._theBuildNumber;
        number_humanend = levelDataCurrent._theHumanNumber;

        if (credits_end_iv - credits_end_v >= number_buildend && credits_end_iv - credits_end_vi >= number_buildend) for_credits_end_iv = true;
        if (credits_end_v - credits_end_iv >= number_buildend && credits_end_v - credits_end_vi >= number_buildend) for_credits_end_v = true;
        if (credits_end_vi - credits_end_iv >= number_buildend && credits_end_vi - credits_end_v >= number_buildend) for_credits_end_vi = true;


        if (endingSingle == null)
        {
            if (credits_end_i < passScore) { SceneManager.LoadScene("Ending_bad"); Debug.Log("--BE--"); }
            if (credits_end_i >= passScore) { SceneManager.LoadScene("Ending_i"); Debug.Log("--nomal--"); }
            if (credits_end_i >= passScore) if (credits_end_ii >= number_humanend) { SceneManager.LoadScene("Ending_iii"); Debug.Log("--bad--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii >= number_humanend) { SceneManager.LoadScene("Ending_ii"); Debug.Log("--die--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_iv) { SceneManager.LoadScene("Ending_iv"); Debug.Log("--building--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_v) { SceneManager.LoadScene("Ending_v"); Debug.Log("--factory--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_vi) { SceneManager.LoadScene("Ending_vi"); Debug.Log("--farm--"); }
        }
        else if (endingSingle != null)
        {
            if (credits_end_i < passScore) { SceneManager.LoadScene("Ending_bad_single"); Debug.Log("--BE--"); }
            if (credits_end_i >= passScore) { SceneManager.LoadScene("Ending_i_single"); Debug.Log("--nomal--"); }
            if (credits_end_i >= passScore) if (credits_end_ii >= number_humanend) { SceneManager.LoadScene("Ending_iii_single"); Debug.Log("--bad--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii >= number_humanend) { SceneManager.LoadScene("Ending_ii_single"); Debug.Log("--die--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_iv) { SceneManager.LoadScene("Ending_iv_single"); Debug.Log("--building--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_v) { SceneManager.LoadScene("Ending_v_single"); Debug.Log("--factory--"); }
            if (credits_end_i >= passScore) if (credits_end_ii < number_humanend) if (credits_end_iii < number_humanend) if (for_credits_end_vi) { SceneManager.LoadScene("Ending_vi_single"); Debug.Log("--farm--"); }
        }


    }


    private void buildWord()
    {

        if (lineCoroutine != null) StopCoroutine(lineCoroutine);
        if (markCoroutine != null) StopCoroutine(markCoroutine);


        if (endLine != null)
        {
            lineCoroutine = StartCoroutine(ChangingLine(lineChanges));
        }
        if (endMark != null)
        {
            markCoroutine = StartCoroutine(ChangingMark(markChanges));
        }
    }

    public void SpeedUp()
    {
        buildCount = buildCount + 1;
        buildSpeed = buildCount * 0.9f;
        lineChanges = 1f / changePoint * buildSpeed;
        markChanges = Mathf.Min(125f / (360f * changePoint * buildSpeed), 1f / changePoint * buildSpeed);

        buildWord();

    }

    public void SpeedBack()
    {
        buildCount = buildCount - 1;
        buildSpeed = buildCount * 0.9f;
        lineChanges = 1f / changePoint * buildSpeed;
        markChanges = Mathf.Min(125f / (360f * changePoint * buildSpeed), 1f / changePoint * buildSpeed);

        buildWord();

    }


    private IEnumerator ChangingLine(float lineChanges)
    {
        float currentFillAmount = endLine.fillAmount;
        float targetFillAmount = currentFillAmount + lineChanges;
        float startTime = Time.time;

        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float lerpValue = elapsedTime / changeTime;
            float newFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, lerpValue);

            endLine.fillAmount = Mathf.Clamp(newFillAmount, currentFillAmount, targetFillAmount);
            yield return null;
        }
        endLine.fillAmount = targetFillAmount;
        isChangingLine = false;
    }

    private IEnumerator ChangingMark(float markChanges)
    {
        Color.RGBToHSV(endMark.color, out float currentH, out float currentS, out float currentV);
        float targetH = Mathf.Min(currentH + markChanges, 125f / 360f);
        float startTime = Time.time;

        while (true)
        {
            float elapsedTime = Time.time - startTime;
            float lerpValue = elapsedTime / changeTime;
            float newH = Mathf.Lerp(currentH, targetH, lerpValue);

            Color.RGBToHSV(endMark.color, out float _, out float newS, out float newV);
            Color newColor = Color.HSVToRGB(newH, newS, newV);
            newColor.a = endMark.color.a;

            endMark.color = newColor;
            yield return null;
        }
        Color.RGBToHSV(endMark.color, out float _, out float finalS, out float finalV);
        Color finalColor = Color.HSVToRGB(targetH, finalS, finalV);
        finalColor.a = endMark.color.a;
        endMark.color = finalColor;
        isChangingMark = false;
    }


}
