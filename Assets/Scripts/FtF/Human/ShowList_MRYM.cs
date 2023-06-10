using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowList_MRYM : MonoBehaviour
{
    public RectTransform listTransform; // 引用 UI Image 的 RectTransform 组件
    public float moveDuration = 0.5f; // 移动时间
    public float moveFirst = 130f; // 移动量
    private Coroutine currentMoveCoroutine; // 用于存储当前移动的协程引用
    private Vector2 originalPosition; // 原始位置
    private Quaternion originalRotation; // 原始旋转角度

    private void Start()
    {
        originalPosition = listTransform.anchoredPosition; // 保存原始位置
        originalRotation = listTransform.rotation; // 保存原始旋转角度
    }

    public void OpenList()
    {
        // 如果已经在进行移动，则先停止之前的移动协程
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y + moveFirst, Quaternion.Euler(0f, 0f, -60f)));
    }

    public void OffList()
    {
        // 如果已经在进行移动，则先停止之前的移动协程
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y, originalRotation));
    }

    private IEnumerator MoveList(float targetY, Quaternion targetRotation)
    {
        Vector2 startPosition = listTransform.anchoredPosition;
        Quaternion startRotation = listTransform.rotation;
        Vector2 targetPosition = new Vector2(startPosition.x, targetY);

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            listTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            listTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        listTransform.anchoredPosition = targetPosition;
        listTransform.rotation = targetRotation;
        currentMoveCoroutine = null;
    }
}
