using UnityEngine;

public class Tree : MonoBehaviour
{
    public bool isTree = false;
    private void Update()
    {
        if (transform.childCount > 0)
        {
            gameObject.name = "Tree_still";

            gameObject.tag = "Tree_still";
        }
        isTree = true;
    }
}
