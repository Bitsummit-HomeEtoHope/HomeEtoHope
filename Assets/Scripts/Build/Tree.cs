using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool isTree = false;
    private void Update()
    {
        // 检查是否存在子物体
        if (transform.childCount > 0)
        {
            // 更改物体名称为 "Tree_still"
            gameObject.name = "Tree_still";

            // 将标签更改为 "Tree_still"
            gameObject.tag = "Tree_still";
        }
        isTree = true;
    }
}
