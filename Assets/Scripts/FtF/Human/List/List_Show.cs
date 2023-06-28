using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class List_Show : MonoBehaviour, IPointerClickHandler
{
    public RectTransform listTransform; // Reference to the RectTransform component of the UI Image
    public float moveDuration = 0.5f; // Movement duration
    public float moveAmount = 300f; // Amount of movement
    public float rotationAmount = 0f; // Rotation angle

    private Coroutine currentMoveCoroutine; // Stores the reference to the current move coroutine
    private Vector2 originalPosition; // Original position
    private Quaternion originalRotation; // Original rotation angle
    public bool isShowing = false; // Whether the list is currently being shown
    public bool isCanClick = false; // Clicking control switch

    private void Start()
    {
        originalPosition = listTransform.anchoredPosition; // Save the original position
        originalRotation = listTransform.rotation; // Save the original rotation angle
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isCanClick) // If the click switch is disabled, don't execute the following logic
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (isShowing == false)
            {
                Show();
                // play sound
                GetComponent<AudioSource>().Play();
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
