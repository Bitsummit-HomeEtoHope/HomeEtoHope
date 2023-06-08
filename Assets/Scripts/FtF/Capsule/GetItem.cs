using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private Transform foodPoint; // Foodλ�õ�Transform
    [SerializeField] private Transform toolPoint; // Toolλ�õ�Transform
    [SerializeField] private Transform humanPoint; // Humanλ�õ�Transform

    [SerializeField] private GameObject applePrefab; // Ҫ���õ�ƻ��Ԥ�Ƽ�
    [SerializeField] private GameObject toolPrefab; // Ҫ���õĹ���Ԥ�Ƽ�
    [SerializeField] private GameObject humanPrefab; // Ҫ���õ�����Ԥ�Ƽ�

    [Header("Placement Settings")]
    [SerializeField] private int orderInLayer = 2; // Order in Layer
    [SerializeField] private Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f); // �ߴ�Scale
    [SerializeField] private Vector3 localOffset = new Vector3(0f, -0.753f, 0f); // ���ƫ����

    public void ReceiveCode(string code)
    {
        Debug.Log("Received code: " + code);
        // �����ﴦ����յ��Ĵ���
    }

    public void ReceiveTag(string tag)
    {
        Debug.Log("Received tag: " + tag);

        switch (tag)
        {
            case "Food":
                PlacePrefab(applePrefab, foodPoint);
                break;
            case "Tool":
                PlacePrefab(toolPrefab, toolPoint);
                break;
            case "Human":
                PlacePrefab(humanPrefab, humanPoint);
                break;
            default:
                Debug.Log("Invalid tag.");
                break;
        }
    }

    private void PlacePrefab(GameObject prefab, Transform point)
    {
        if (prefab != null && point != null)
        {
            GameObject instance = Instantiate(prefab, point.position, point.rotation);
            instance.transform.parent = point;
            instance.GetComponent<Renderer>().sortingOrder = orderInLayer;
            instance.transform.localScale = scale;
            instance.transform.localPosition += localOffset;
        }
    }
}
