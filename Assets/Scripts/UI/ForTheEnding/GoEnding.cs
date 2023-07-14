using UnityEngine;
using UnityEngine.UI;

public class GoEnding : MonoBehaviour
{
    public string fillAmountTargetObjectName; // 填充量目标游戏物体的名称
    public string colorTargetObjectName; // 颜色目标游戏物体的名称
    public int buildCount = 9; // BuildCount设置为9
    public float fillAmountIncreaseTime = 5f; // 增加Fill Amount的时间
    public float colorChangeTime = 5f; // 更改颜色的时间
    private Image fillAmountTargetImage; // 填充量目标UI Image组件
    private Image colorTargetImage; // 颜色目标UI Image组件
    private float fillAmountIncrement; // 增加的Fill Amount量
    private float hIncrement; // 增加的H值量
    private float vIncrement; // 增加的V值量
    private bool hasIncreasedFillAmount; // 是否已经增加过Fill Amount
    private bool hasChangedColor; // 是否已经更改过颜色

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

        // 在 Start 时同时触发增加 Fill Amount 和更改颜色的方法
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
