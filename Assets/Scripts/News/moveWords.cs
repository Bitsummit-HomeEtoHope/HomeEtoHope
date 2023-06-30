using UnityEngine;

public class moveWords : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float moveDuration = 5f;
    private float moveDistance = 1514f;
    private float elapsedTime = 0f;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(moveDistance, 0f, 0f);
    }

    private void Update()
    {
        if (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
        }
        else
        {
            transform.position = targetPosition; // 确保到达目标位置
        }
    }
}
