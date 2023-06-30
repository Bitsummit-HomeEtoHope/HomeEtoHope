using UnityEngine;

public class HumanLayer : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsAbove; // 第一个物体列表
    [SerializeField] private GameObject[] objectsBelow; // 第二个物体列表

    private int sortingOrderAbove = 1; // 第一个列表物体的图层顺序
    private int sortingOrderBelow = -1; // 第二个列表物体的图层顺序

    private void Start()
    {
        // 设置物体在第一个列表物体之上的图层顺序
        foreach (GameObject obj in objectsAbove)
        {
            SetSortingOrderTo(obj, sortingOrderAbove);
            sortingOrderAbove++;
        }

        // 设置物体在第二个列表物体之下的图层顺序
        foreach (GameObject obj in objectsBelow)
        {
            SetSortingOrderTo(obj, sortingOrderBelow);
            sortingOrderBelow--;
        }
    }

    private void SetSortingOrderTo(GameObject obj, int sortingOrder)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = sortingOrder;
        }
        else
        {
            Debug.LogWarning("Renderer component not found on object: " + obj.name);
        }
    }
}
