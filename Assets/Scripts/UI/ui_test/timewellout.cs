using UnityEngine;

public class timewellout : MonoBehaviour
{
    public Transform targetPosition; // 指定的目标位置
    public float movementTime = 90f; // 移动时间（秒）

    private float timer = 0f;
    private Vector3 startingPosition;
    private bool isMoving = false;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / movementTime);

            // 在指定时间内将预制件移动到目标位置
            transform.position = Vector3.Lerp(startingPosition, targetPosition.position, t);

            if (t >= 1f)
            {
                // 移动完成后执行相关操作
                // 例如：播放动画、触发事件等
                Debug.Log("移动完成！");
                isMoving = false;
            }
        }
    }

    public void StartMovement()
    {
        timer = 0f;
        isMoving = true;
    }
}
