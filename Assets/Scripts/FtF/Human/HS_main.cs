using UnityEngine;

public class HS_Main : MonoBehaviour
{
    // 需要在Inspector中设置的参数
    [Header("移动设置")]
    public float moveSpeed = 1.0f; // 移动速度
    public float maxDistance = 2.0f; // 最大距离限制
    public float maxMoveX = 0.5f; // 在x轴上最大移动距离
    public float maxMoveY = 0.5f; // 在y轴上最大移动距离
    public float moveFrequency = 3.0f; // 移动频率

    // 需要在Inspector中设置的游戏对象
    [Header("目标对象")]
    public Transform targetObject; // 目标对象

    // 移动状态
    private enum MoveState
    {
        RandomMove, // 随机移动状态
        XAxisMove // 沿着X轴移动状态
    }

    // 当前移动状态
    private MoveState currentMoveState = MoveState.RandomMove;

    // 目标位置
    private Vector3 targetPosition;

    // 随机移动时的计时器
    private float randomMoveTimer = 0.0f;
    private float randomMoveInterval = 0.0f; // 随机移动状态持续时间

    // Start is called before the first frame update
    void Start()
    {
        // 设置初始的旋转角度
        transform.rotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);

        // 初始化随机移动状态持续时间
        randomMoveInterval = Random.Range(moveFrequency - 0.5f, moveFrequency + 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // 检查是否有目标对象
        if (targetObject != null)
        {
            // 获取当前位置
            Vector3 currentPosition = transform.position;

            // 判断当前移动状态
            switch (currentMoveState)
            {
                case MoveState.RandomMove:
                    // 检查随机移动计时器是否超过持续时间
                    randomMoveTimer += Time.deltaTime;
                    if (randomMoveTimer >= randomMoveInterval)
                    {
                        // 切换到沿着X轴移动状态
                        currentMoveState = MoveState.XAxisMove;
                        targetPosition = new Vector3(
                            currentPosition.x + Random.Range(-maxMoveX, maxMoveX),
                            currentPosition.y,
                            currentPosition.z
                        );
                        randomMoveTimer = 0.0f;
                    }
                    else
                    {
                        // 根据目标对象的位置计算移动目标点
                        targetPosition = new Vector3(
                            Random.Range(targetObject.position.x - maxDistance, targetObject.position.x + maxDistance),
                            Random.Range(targetObject.position.y - maxDistance, targetObject.position.y + maxDistance),
                            Random.Range(targetObject.position.z - maxDistance, targetObject.position.z + maxDistance)
                        );
                    }
                    break;
                case MoveState.XAxisMove:
                    // 计算移动的方向
                    Vector3 moveDirection = targetPosition - currentPosition;

                    // 计算在x轴上的移动距离
                    float moveX = Mathf.Clamp(moveDirection.x, -maxMoveX, maxMoveX);

                    // 根据移动方向设置旋转角度
                    if (moveX > 0)
                    {
                        transform.rotation = Quaternion.Euler(-45.0f, -180.0f, 0.0f);
                    }
                    else if (moveX < 0)
                    {
                        transform.rotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);
                    }

                    // 计算最终的移动向量
                    Vector3 moveVector = new Vector3(moveX, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;

                    // 更新位置
                    transform.position += moveVector;

                    // 检查是否到达目标位置
                    if (Mathf.Abs(currentPosition.x - targetPosition.x) <= 0.01f)
                    {
                        // 切换回随机移动状态
                        currentMoveState = MoveState.RandomMove;

                        // 重新设置随机移动状态持续时间
                        randomMoveInterval = Random.Range(moveFrequency - 0.5f, moveFrequency + 0.5f);
                    }
                    break;
            }
        }
    }
}
