using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class List_Show : MonoBehaviour, IPointerClickHandler
{
    public RectTransform listTransform; // ���� UI Image �� RectTransform ���
    public float moveDuration = 0.5f; // �ƶ�ʱ��
    public float moveAmount = 300f; // �ƶ���
    public float rotationAmount = 0f; // ��ת�Ƕ�

    private Coroutine currentMoveCoroutine; // ���ڴ洢��ǰ�ƶ���Э������
    private Vector2 originalPosition; // ԭʼλ��
    private Quaternion originalRotation; // ԭʼ��ת�Ƕ�
    private bool isShowing = false; // �Ƿ�������ʾ�б�

    private void Start()
    {
        originalPosition = listTransform.anchoredPosition; // ����ԭʼλ��
        originalRotation = listTransform.rotation; // ����ԭʼ��ת�Ƕ�
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isShowing == false)
            {
                Show();
            }
            else
            {
                Close();
            }
        }
    }

    private void Show()
    {
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y + moveAmount, Quaternion.Euler(0f, 0f, rotationAmount)));

        isShowing = true;
    }

    private void Close()
    {
        if (isShowing)
        {
            isShowing = false;
            currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y + 130f, originalRotation));
        }
        else
        {
            currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y, originalRotation));
        }
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

    public void ForceCloseList()
    {
        if (isShowing)
        {
            isShowing = false;
            currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y + 130f, originalRotation));
        }
        else
        {
            currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y, originalRotation));
        }
    }
}
