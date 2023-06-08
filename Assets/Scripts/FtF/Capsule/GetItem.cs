using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private Transform foodPoint; // Food位置的Transform
    [SerializeField] private Transform toolPoint; // Tool位置的Transform
    [SerializeField] private Transform humanPoint; // Human位置的Transform

    [SerializeField] private GameObject applePrefab; // 要放置的苹果预制件
    [SerializeField] private GameObject toolPrefab; // 要放置的工具预制件
    [SerializeField] private GameObject humanPrefab; // 要放置的人物预制件

    [Header("Placement Settings")]
    [SerializeField] private int orderInLayer = 2; // Order in Layer
    [SerializeField] private Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f); // 尺寸Scale
    [SerializeField] private Vector3 localOffset = new Vector3(0f, -0.753f, 0f); // 相对偏移量

    public void ReceiveCode(string code)
    {
        Debug.Log("Received code: " + code);
        // 在这里处理接收到的代码
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
