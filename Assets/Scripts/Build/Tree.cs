using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool isTree = false;
    private void Update()
    {
        // ����Ƿ����������
        if (transform.childCount > 0)
        {
            // ������������Ϊ "Tree_still"
            gameObject.name = "Tree_still";

            // ����ǩ����Ϊ "Tree_still"
            gameObject.tag = "Tree_still";
        }
        isTree = true;
    }
}
