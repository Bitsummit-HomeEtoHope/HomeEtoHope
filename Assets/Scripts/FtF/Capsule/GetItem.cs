using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    [SerializeField] private Transform foodPoint;
    [SerializeField] private Transform toolPoint;
    [SerializeField] private Transform humanPoint;

    [Header("Placement Settings")]
    [SerializeField] private int orderInLayer = 2;
    private int currentSortingOrder = 0;

    [Header("Food")]
    [SerializeField] private Vector3 foodScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 foodOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 foodInitialRotation = Vector3.zero;
    [SerializeField] private float foodMoveSpeed = 1f;
    [SerializeField] private Transform foodDestinationPoint;

    [Header("Tool")]
    [SerializeField] private Vector3 toolScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 toolOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 toolInitialRotation = Vector3.zero;
    [SerializeField] private float toolMoveSpeed = 1f;
    [SerializeField] private Transform toolDestinationPoint;

    [Header("Human")]
    [SerializeField] private Vector3 humanScale = new Vector3(0.8f, 0.8f, 0.8f);
    [SerializeField] private Vector3 humanOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 humanInitialRotation = Vector3.zero;
    [SerializeField] private float humanMoveSpeed = 1f;
    [SerializeField] private Transform humanDestinationPoint;

    private string readytag;
    private string readycode;
    public List<GameObject> foodList = new List<GameObject>();
    public List<GameObject> toolList = new List<GameObject>();
    public List<GameObject> humanList = new List<GameObject>();

    private void Start()
    {
        ReceiveCode(readycode);
        ReceiveTag(readytag);
    }

    public void ReceiveTag(string tag)
    {
        readytag = tag;
        CallItem();
    }

    public void ReceiveCode(string code)
    {
        readycode = code;
    }

    private void CallItem()
    {
        Debug.Log("Ready Tag: " + readytag);
        Debug.Log("Ready Code: " + readycode);

        GameObject prefab = null;

        if (readytag == "Food")
        {
            prefab = Resources.Load<GameObject>("2D_set/food/" + readycode);
        }
        else if (readytag == "Tool")
        {
            prefab = Resources.Load<GameObject>("2D_set/tool/" + readycode);
        }
        else if (readytag == "Human")
        {
            prefab = Resources.Load<GameObject>("2D_set/human/" + readycode);
        }

        if (prefab != null)
        {
            switch (readytag)
            {
                case "Food":
                    TakeMeOut(prefab, foodPoint, foodScale, foodOffset, foodInitialRotation, foodMoveSpeed, foodDestinationPoint, "Food_39");
                    foodList.Add(prefab);
                    Debug.Log("-----Food coming-----");
                    break;
                case "Tool":
                    TakeMeOut(prefab, toolPoint, toolScale, toolOffset, toolInitialRotation, toolMoveSpeed, toolDestinationPoint, "Tool_39");
                    toolList.Add(prefab);
                    Debug.Log("-----Tool coming-----");
                    break;
                case "Human":
                    TakeMeOut(prefab, humanPoint, humanScale, humanOffset, humanInitialRotation, humanMoveSpeed, humanDestinationPoint, "Human_39");
                    humanList.Add(prefab);
                    Debug.Log("-----Human coming-----");
                    break;
                default:
                    Debug.Log("Invalid tag: " + readytag);
                    break;
            }
        }
        else
        {
            Debug.Log("Prefab not found for tag: " + readytag);
        }
    }

    private void TakeMeOut(GameObject prefab, Transform point, Vector3 scale, Vector3 offset, Vector3 initialRotation, float moveSpeed, Transform destinationPoint, string newTag)
{

        if (point != null)
    {
            GameObject instance = Instantiate(prefab, point.position, Quaternion.Euler(initialRotation));

            currentSortingOrder += 1;
            instance.GetComponent<Renderer>().sortingOrder = currentSortingOrder;

            Vector3 initialScale = instance.transform.localScale;
        Vector3 adjustedScale = new Vector3(initialScale.x * scale.x, initialScale.y * scale.y, initialScale.z * scale.z);
        instance.transform.localScale = adjustedScale;

        instance.transform.rotation = Quaternion.Euler(instance.transform.rotation.eulerAngles.x, instance.transform.rotation.eulerAngles.y, prefab.transform.rotation.eulerAngles.z);

        StartCoroutine(MoveItem(instance.transform, moveSpeed, destinationPoint, newTag));
    }
}

    private IEnumerator MoveItem(Transform itemTransform, float moveDuration, Transform destinationPoint, string newTag)
    {

        Vector3 startPosition = itemTransform.localPosition;
        Vector3 targetPosition = destinationPoint.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            itemTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position is exact
        itemTransform.localPosition = targetPosition;

        // Change tag after moving
        itemTransform.gameObject.tag = newTag;
    }


}
