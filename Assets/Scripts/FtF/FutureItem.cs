using System.Collections;
using UnityEngine;

public class FutureItem : MonoBehaviour
{
    [SerializeField] private float yOffset = 1f; // 相对当前位置的y轴偏移量
    [SerializeField] private float moveDuration = 1f; // 移动所需的时间

    private bool isMoving = false; // 是否正在移动

    // Start is called before the first frame update
    void Start()
    {
        MoveToOffsetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AutoMove());
        }
    }

    // 移动到偏移位置
    private void MoveToOffsetPosition()
    {
        Vector3 targetPosition = transform.localPosition;
        targetPosition.y += yOffset;

        // 使用协程实现在指定时间内平滑移动到目标位置
        StartCoroutine(MoveToPosition(targetPosition, moveDuration));
    }

    // 在指定时间内平滑移动到目标位置
    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        isMoving = true; // 设置为正在移动状态

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

        isMoving = false; // 设置为非移动状态
    }

    // 自动移动
    private IEnumerator AutoMove()
    {
        isMoving = true; // 设置为正在移动状态

        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = startPosition + new Vector3(-1f, 0f, 0f); // 目标位置为当前位置向 x 轴的负方向移动1f

        float elapsedTime = 0f;
        float duration = 1f; // 自动移动的时间

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

        isMoving = false; // 设置为非移动状态
    }
}
