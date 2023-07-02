using System.Collections;
using UnityEngine;

public class treetest : MonoBehaviour
{
    [Header("Full Time")]
    public float springDuration = 0.15f; // ����Ч���ĳ���ʱ��
    [Header("Two")]
    [SerializeField]private bool isTwo = true;
    public float springIntensity = 1.5f; // ����Ч����ǿ��
    [Header("One")]
    [SerializeField] private bool isOne = false;
    public float JX = 1.5f; // ����Ч����ǿ��
    public float JY = 1.5f; // ����Ч����ǿ��

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

            // ���㵯��Ч��������ֵ
            float t = elapsedTime / springDuration;
            float scale = Mathf.Lerp(springIntensity, 1f, t);

            // Ӧ�õ���Ч��
            transform.localScale = new Vector3(scale, scale, 1f);

            yield return null;
        }

        // �ָ���ʼ����ֵ
        transform.localScale = originalScale;
    }



    private IEnumerator SpringEffectX()
    {
        float elapsedTime = 0f;
        float initialScaleX = transform.localScale.x;

        while (elapsedTime < springDuration)
        {
            elapsedTime += Time.deltaTime;

            // ���� X ����ֵ�ĵ���Ч��
            float t = elapsedTime / (springDuration * 0.5f); // ����ʱ���һ��
            float scaleX = Mathf.Lerp(JX, 1f, t);

            // Ӧ�õ���Ч��
            transform.localScale = new Vector3(scaleX, transform.localScale.y, 1f);

            yield return null;
        }

        // �ָ���ʼ����ֵ
        transform.localScale = new Vector3(initialScaleX, transform.localScale.y, 1f);

        // ��ʼ Y ��ĵ���Ч��
        StartCoroutine(SpringEffectY());
    }

    private IEnumerator SpringEffectY()
    {
        float elapsedTime = 0f;
        float initialScaleY = transform.localScale.y;

        while (elapsedTime < springDuration)
        {
            elapsedTime += Time.deltaTime;

            // ���� Y ����ֵ�ĵ���Ч��
            float t = elapsedTime / (springDuration * 0.5f); // ����ʱ���һ��
            float scaleY = Mathf.Lerp(1f, JY, t);

            // Ӧ�õ���Ч��
            transform.localScale = new Vector3(transform.localScale.x, scaleY, 1f);

            yield return null;
        }

        // �ָ���ʼ����ֵ
        transform.localScale = new Vector3(transform.localScale.x, initialScaleY, 1f);
    }



}
