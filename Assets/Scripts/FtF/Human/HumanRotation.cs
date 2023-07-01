using UnityEngine;

public class HumanRotation : MonoBehaviour
{
    [SerializeField] public int rotation;
    private Transform characterTransform;
    private Vector3 previousPosition;

    private void Start()
    {
        characterTransform = transform; // 获取角色的Transform组件
        previousPosition = characterTransform.position; // 保存初始位置
    }

    private void Update()
    {
        Vector3 currentPosition = characterTransform.position; // 获取当前位置

        if (currentPosition.x < previousPosition.x)
        {
            characterTransform.rotation = Quaternion.Euler(rotation, 0f, 0f); // 向左移动时，Y轴旋转为0度
        }
        else if (currentPosition.x > previousPosition.x)
        {
            characterTransform.rotation = Quaternion.Euler(-rotation, 180f, 0f); // 向右移动时，Y轴旋转为180度
        }

        previousPosition = currentPosition; // 更新前一位置
    }
}
