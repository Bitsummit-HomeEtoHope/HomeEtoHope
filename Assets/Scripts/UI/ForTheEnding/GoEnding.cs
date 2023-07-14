using UnityEngine;
using UnityEngine.UI;

public class GoEnding : MonoBehaviour
{
    public string fillAmountTargetObjectName; // �����Ŀ����Ϸ���������
    public string colorTargetObjectName; // ��ɫĿ����Ϸ���������
    public int buildCount = 9; // BuildCount����Ϊ9
    public float fillAmountIncreaseTime = 5f; // ����Fill Amount��ʱ��
    public float colorChangeTime = 5f; // ������ɫ��ʱ��
    private Image fillAmountTargetImage; // �����Ŀ��UI Image���
    private Image colorTargetImage; // ��ɫĿ��UI Image���
    private float fillAmountIncrement; // ���ӵ�Fill Amount��
    private float hIncrement; // ���ӵ�Hֵ��
    private float vIncrement; // ���ӵ�Vֵ��
    private bool hasIncreasedFillAmount; // �Ƿ��Ѿ����ӹ�Fill Amount
    private bool hasChangedColor; // �Ƿ��Ѿ����Ĺ���ɫ

    private void Start()
    {
        GameObject fillAmountTargetObject = GameObject.Find(fillAmountTargetObjectName);
        GameObject colorTargetObject = GameObject.Find(colorTargetObjectName);

        if (fillAmountTargetObject != null)
        {
            fillAmountTargetImage = fillAmountTargetObject.GetComponent<Image>();
        }

        if (colorTargetObject != null)
        {
            colorTargetImage = colorTargetObject.GetComponent<Image>();
        }

        fillAmountIncrement = 1f / buildCount;
        hIncrement = Mathf.Min(125f / (360f * buildCount), 1f / buildCount);
        vIncrement = 45f / buildCount;
        hasIncreasedFillAmount = false;
        hasChangedColor = false;

        // �� Start ʱͬʱ�������� Fill Amount �͸�����ɫ�ķ���
        IncreaseFillAmountSmoothly();
        ChangeColorHValueSmoothly();
        ChangeColorVValueSmoothly();
    }

    private void IncreaseFillAmountSmoothly()
    {
        if (fillAmountTargetImage != null && !hasIncreasedFillAmount)
        {
            StartCoroutine(IncrementFillAmountOverTime());
            hasIncreasedFillAmount = true;
        }
    }

    private System.Collections.IEnumerator IncrementFillAmountOverTime()
    {
        float currentFillAmount = fillAmountTargetImage.fillAmount;
        float targetFillAmount = currentFillAmount + fillAmountIncrement;
        float startTime = Time.time;

        while (Time.time < startTime + fillAmountIncreaseTime)
        {
            float elapsedTime = Time.time - startTime;
            float lerpValue = elapsedTime / fillAmountIncreaseTime;
            float newFillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, lerpValue);
            fillAmountTargetImage.fillAmount = newFillAmount;
            yield return null;
        }

        fillAmountTargetImage.fillAmount = targetFillAmount;
    }

    private void ChangeColorHValueSmoothly()
    {
        if (colorTargetImage != null && !hasChangedColor)
        {
            StartCoroutine(ChangeHValueOverTime());
            hasChangedColor = true;
        }
    }

    private System.Collections.IEnumerator ChangeHValueOverTime()
    {
        Color.RGBToHSV(colorTargetImage.color, out float currentH, out float currentS, out float currentV);
        float targetH = Mathf.Min(currentH + hIncrement, 125f / 360f);
        float startTime = Time.time;

        while (Time.time < startTime + colorChangeTime)
        {
            float elapsedTime = Time.time - startTime;
            float lerpValue = elapsedTime / colorChangeTime;
            float newH = Mathf.Lerp(currentH, targetH, lerpValue);

            Color.RGBToHSV(colorTargetImage.color, out float _, out float newS, out float newV);
            Color newColor = Color.HSVToRGB(newH, newS, newV);
            newColor.a = colorTargetImage.color.a;
            colorTargetImage.color = newColor;

            yield return null;
        }

        Color.RGBToHSV(colorTargetImage.color, out float _, out float finalS, out float finalV);
        Color finalColor = Color.HSVToRGB(targetH, finalS, finalV);
        finalColor.a = colorTargetImage.color.a;
        colorTargetImage.color = finalColor;
    }

    private void ChangeColorVValueSmoothly()
    {
        if (colorTargetImage != null && !hasChangedColor)
        {
            StartCoroutine(ChangeVValueOverTime());
            hasChangedColor = true;
        }
    }

    private System.Collections.IEnumerator ChangeVValueOverTime()
    {
        Color.RGBToHSV(colorTargetImage.color, out float currentH, out float currentS, out float currentV);
        float targetV = currentV + vIncrement;
        float startTime = Time.time;

        while (Time.time < startTime + colorChangeTime)
        {
            float elapsedTime = Time.time - startTime;
            float lerpValue = elapsedTime / colorChangeTime;
            float newV = Mathf.Lerp(currentV, targetV, lerpValue);

            Color.RGBToHSV(colorTargetImage.color, out float newH, out float newS, out _);
            Color newColor = Color.HSVToRGB(newH, newS, newV);
            newColor.a = colorTargetImage.color.a;
            colorTargetImage.color = newColor;

            yield return null;
        }

        Color.RGBToHSV(colorTargetImage.color, out float finalH, out float finalS, out _);
        Color finalColor = Color.HSVToRGB(finalH, finalS, targetV);
        finalColor.a = colorTargetImage.color.a;
        colorTargetImage.color = finalColor;
    }
}
