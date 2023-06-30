using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> news_move;
    [SerializeField] public List<GameObject> news_zoom;
    [SerializeField] public List<Image> news_bord;
    [SerializeField] public GameObject news_open;
    [SerializeField] public bool isover = false;
    [Header("Show")]
    [SerializeField] private bool isPaused = false; // 控制暂停状态
    [SerializeField] public List<GameObject> targetObjects; // 多个游戏对象

    [Header("Move")]
    [SerializeField] private float move_x; // Distance to move along the positive x-axis
    [SerializeField] private float move_time; // Time taken for the movement

    [Header("Zoom")]
    [SerializeField] private float zoom_a; // First scale change
    [SerializeField] private float time_zoom_a; // Time taken for the first scale change
    [SerializeField] private float zoom_b; // Second scale change
    [SerializeField] private float time_zoom_b; // Time taken for the second scale change





    private void Start()
    {
        Pause();
        StartCoroutine(StartAfterDelay());
    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(MoveNewsObjects());
        StartCoroutine(OpenBord());
    }

    private void Update()
    {
        SetTimeScale(1f);
        StartCoroutine(UpdateAfterDelay());
    }
    private void SetTimeScale(float timeScale)
    {
        // 检查是否指定了游戏对象
        if (targetObjects != null && targetObjects.Count > 0)
        {
            foreach (GameObject obj in targetObjects)
            {
                // 设置游戏对象的时间缩放值
                obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // 重置刚体速度（可选）
            }
            Time.timeScale = timeScale;
        }
    }

    private IEnumerator UpdateAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (isover)
        {
            StartCoroutine(ZoomNewsObjects());
        }
    }

    private IEnumerator OpenBord()
    {
        float fillDuration = 0.1f;
        float elapsedTime = 0f;

        List<float> startFillAmounts = new List<float>();
        List<float> targetFillAmounts = new List<float>();

        foreach (Image bordImage in news_bord)
        {
            startFillAmounts.Add(bordImage.fillAmount);
            targetFillAmounts.Add(1f);
        }

        while (elapsedTime < fillDuration)
        {
            float t = elapsedTime / fillDuration;

            for (int i = 0; i < news_bord.Count; i++)
            {
                Image bordImage = news_bord[i];
                float startFillAmount = startFillAmounts[i];
                float targetFillAmount = targetFillAmounts[i];

                bordImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < news_bord.Count; i++)
        {
            Image bordImage = news_bord[i];
            bordImage.fillAmount = targetFillAmounts[i];
        }

        yield return new WaitForSeconds(0.1f);
        news_open.SetActive(true);
    }

    private IEnumerator ZoomNewsObjects()
    {
        if (isover)
        {
            isover = false;
        }

        foreach (GameObject newsObject in news_zoom)
        {
            yield return StartCoroutine(Zoom(newsObject.transform, zoom_a, time_zoom_a));
            yield return StartCoroutine(Zoom(newsObject.transform, zoom_b, time_zoom_b));
        }

        yield return new WaitForSeconds(1f);
        if (!isover)
        {
            isover = true;
        }
    }

    private IEnumerator Zoom(Transform targetTransform, float targetScale, float duration)
    {
        Vector3 initialScale = targetTransform.localScale;
        Vector3 targetScaleVector = Vector3.one * targetScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
                float t = elapsedTime / duration;
                targetTransform.localScale = Vector3.Lerp(initialScale, targetScaleVector, t);
                elapsedTime += Time.deltaTime;

            yield return null;
        }

        targetTransform.localScale = targetScaleVector;
    }

    private IEnumerator MoveNewsObjects()
    {
        foreach (GameObject newsObject in news_move)
        {
            Move(newsObject.transform);
        }

        yield return new WaitForSeconds(move_time);

        StartCoroutine(ZoomNewsObjects());
    }

    private void Move(Transform targetTransform)
    {
        StartCoroutine(MoveNews(targetTransform));
    }

    private IEnumerator MoveNews(Transform targetTransform)
    {
        float elapsedTime = 0f;
        float moveDuration = move_time;
        float moveDistance = move_x;
        Vector3 startPosition = targetTransform.localPosition;
        Vector3 targetPosition = targetTransform.localPosition + new Vector3(moveDistance, 0f, 0f);

        while (elapsedTime < moveDuration)
        {           
                float t = elapsedTime / moveDuration;
                targetTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;           

            yield return null;
        }

        targetTransform.localPosition = targetPosition;
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // 暂停时间缩放
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // 恢复时间缩放
    }
}
