using System.Collections;
using UnityEngine;

public class treetest : MonoBehaviour
{
    [Header("Full Time")]
    public float springDuration = 0.15f; // 弹簧效果的持续时间
    [Header("Two")]
    [SerializeField]private bool isTwo = true;
    public float springIntensity = 1.5f; // 弹簧效果的强度
    [Header("One")]
    [SerializeField] private bool isOne = false;
    public float JX = 1.5f; // 弹簧效果的强度
    public float JY = 1.5f; // 弹簧效果的强度

    private Vector3 originalScale;
    private float elapsedTime;

    private void Start()
    {
        originalScale = transform.localScale;
        elapsedTime = 0f;

        if (isTwo)
        StartCoroutine(PerformSpringEffect());

        if (isOne)
        StartCoroutine(SpringEffectX());
    }

    private IEnumerator PerformSpringEffect()
    {
        while (elapsedTime < springDuration)
        {
            elapsedTime += Time.deltaTime;

            // 计算弹簧效果的缩放值
            float t = elapsedTime / springDuration;
            float scale = Mathf.Lerp(springIntensity, 1f, t);

            // 应用弹簧效果
            transform.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        // 恢复初始缩放值
        transform.localScale = originalScale;
    }



    private IEnumerator SpringEffectX()
    {
        float elapsedTime = 0f;
        float initialScaleX = transform.localScale.x;

        while (elapsedTime < springDuration)
        {
            elapsedTime += Time.deltaTime;

            // 计算 X 缩放值的弹簧效果
            float t = elapsedTime / (springDuration * 0.5f); // 持续时间的一半
            float scaleX = Mathf.Lerp(JX, 1f, t);

            // 应用弹簧效果
            transform.localScale = new Vector3(scaleX, transform.localScale.y, 1f);

            yield return null;
        }

        // 恢复初始缩放值
        transform.localScale = new Vector3(initialScaleX, transform.localScale.y, 1f);

        // 开始 Y 轴的弹簧效果
        StartCoroutine(SpringEffectY());
    }

    private IEnumerator SpringEffectY()
    {
        float elapsedTime = 0f;
        float initialScaleY = transform.localScale.y;

        while (elapsedTime < springDuration)
        {
            elapsedTime += Time.deltaTime;

            // 计算 Y 缩放值的弹簧效果
            float t = elapsedTime / (springDuration * 0.5f); // 持续时间的一半
            float scaleY = Mathf.Lerp(1f, JY, t);

            // 应用弹簧效果
            transform.localScale = new Vector3(transform.localScale.x, scaleY, 1f);

            yield return null;
        }

        // 恢复初始缩放值
        transform.localScale = new Vector3(transform.localScale.x, initialScaleY, 1f);
    }



}
