using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowList_MRYM : MonoBehaviour
{
    public RectTransform listTransform; // Reference to the UI Image's RectTransform component
    public float moveDuration = 0.5f; // Duration of the movement
    public float moveFirst = 130f; // Amount of movement
    private Coroutine currentMoveCoroutine; // Stores the reference to the current move coroutine
    private Vector2 originalPosition; // Original position
    private Quaternion originalRotation; // Original rotation angle

    public List_Show listShow; // Reference to the List_Show script
   

    public bool listing = false;

    private void Update()
    {
        int humanCount = GameObject.FindGameObjectsWithTag("Human").Length;

        if (humanCount < 1 && listing)
        {
            Debug.Log("-----was off-----");
            listing = false;
            OffList();
        }
    }


    private void Start()
    {
        originalPosition = listTransform.anchoredPosition; // Save the original position
        originalRotation = listTransform.rotation; // Save the original rotation angle
    }

    public void OpenList()
    {
        // If there is a previous move coroutine, stop it first
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y + moveFirst, Quaternion.Euler(0f, 0f, 0f)));
        listShow.isCanClick = true;
    }

    public void OffList()
    {
        // If there is a previous move coroutine, stop it first
        if (currentMoveCoroutine != null)
            StopCoroutine(currentMoveCoroutine);

        currentMoveCoroutine = StartCoroutine(MoveList(originalPosition.y, originalRotation));
        listShow.isCanClick = false;
        listShow.isShowing = false;
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
