using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowList_MRYM : MonoBehaviour
{
    public RectTransform listTransform; // ���� UI Image �� RectTransform ���
    public float moveDuration = 0.5f; // �ƶ�ʱ��
    public float moveFirst = 130f; // �ƶ���
    private Coroutine currentMoveCoroutine; // ���ڴ洢��ǰ�ƶ���Э������
    private Vector2 originalPosition; // ԭʼλ��
    private Quaternion originalRotation; // ԭʼ��ת�Ƕ�

    private void Start()
    {
        originalPosition = listTransform.anchoredPosition; // ����ԭʼλ��
        originalRotation = listTransform.rotation; // ����ԭʼ��ת�Ƕ�
    }

    public void OpenList()
    {
        // ����Ѿ��ڽ����ƶ�������ֹ֮ͣǰ���ƶ�Э��
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y + moveFirst, Quaternion.Euler(0f, 0f, -60f)));
    }

    public void OffList()
    {
        // ����Ѿ��ڽ����ƶ�������ֹ֮ͣǰ���ƶ�Э��
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
