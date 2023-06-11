using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standingby : MonoBehaviour
{
    public Transform[] capsules; // 存储三个capsule的Transform组件
    public float yOffset = 0f; // Y轴偏移量

    private int playerLayer; // Player层的索引
    private float capsuleY; // capsule的Y值

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player"); // 获取Player层的索引
        capsuleY = capsules[0].position.y; // 获取capsule的Y值
    }

    // Update is called once per frame
    void Update()
    {
        // 检测角色是否与capsules重叠
        bool overlapping = false;
        foreach (Transform capsule in capsules)
        {
            // 将角色的世界坐标转换为相对于场景的本地坐标
            Vector3 localPosition = transform.InverseTransformPoint(capsule.position);

            // 使用本地坐标进行比较
            if (localPosition == Vector3.zero)
            {
                overlapping = true;
                break;
            }
        }

        if (overlapping)
        {
            // 检测角色与重叠capsule的位置关系
            if (transform.position.y > capsuleY)
            {
                // 角色在capsule后面，设置角色的order in layer为Player层的索引减1
                GetComponent<Renderer>().sortingOrder = playerLayer - 1;
            }
            else
            {
                // 角色在capsule前面，设置角色的order in layer为Player层的索引加1
                GetComponent<Renderer>().sortingOrder = playerLayer + 1;
            }

            // 对角色的Y值进行调整
            float adjustedY = capsuleY + yOffset;
            transform.position = new Vector3(transform.position.x, adjustedY, transform.position.z);
        }
    }
}
