using UnityEngine;

public class HumanLayer : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsAbove; // ��һ�������б�
    [SerializeField] private GameObject[] objectsBelow; // �ڶ��������б�

    private int sortingOrderAbove = 1; // ��һ���б������ͼ��˳��
    private int sortingOrderBelow = -1; // �ڶ����б������ͼ��˳��

    private void Start()
    {
        // ���������ڵ�һ���б�����֮�ϵ�ͼ��˳��
        foreach (GameObject obj in objectsAbove)
        {
            SetSortingOrderTo(obj, sortingOrderAbove);
            sortingOrderAbove++;
        }

        // ���������ڵڶ����б�����֮�µ�ͼ��˳��
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
