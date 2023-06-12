using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGo : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度

    // Update is called once per frame
    void Update()
    {
        // 通过键盘输入获取移动方向
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 计算移动向量
        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;

        // 更新角色位置
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
