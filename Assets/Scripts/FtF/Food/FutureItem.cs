using System.Collections;
using UnityEngine;

public class FutureItem : MonoBehaviour
{
    [SerializeField] private float yOffset = 1f; // 相对当前位置的y轴偏移量
    [SerializeField] private float moveDuration = 1f; // 移动所需的时间

    // Start is called before the first frame update
    void Start()
    {
        MoveToOffsetPosition();
    }

    // 移动到偏移位置
    private void MoveToOffsetPosition()
    {
        Vector3 targetPosition = transform.localPosition + new Vector3(0f, yOffset, 0f);

        // 使用协程实现在指定时间内平滑移动到目标位置
        StartCoroutine(MoveToPosition(targetPosition, moveDuration));
    }

    // 在指定时间内平滑移动到目标位置
    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 计算当前时间点的插值比例
            float t = elapsedTime / duration;

            // 使用插值计算当前位置
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 移动结束时确保到达目标位置
        transform.localPosition = targetPosition;
    }
}
