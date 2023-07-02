using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

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
    [Header("The List")]
    public List<GameObject> foodList = new List<GameObject>();
    public List<GameObject> toolList = new List<GameObject>();
    public List<GameObject> humanList = new List<GameObject>();

    [Header("HumanTell")]
    [SerializeField] DaysManager daysManager;
    [SerializeField] private string selectedHumanNames;
    [SerializeField] private string path;
    [SerializeField] private List<string> theirList = new List<string>();

    private void Awake()
    {
        daysManager = FindObjectOfType<DaysManager>();
    }

    private void Start()
    {
        ReceiveCode(readycode);
        ReceiveTag(readytag);
    }

    private void HandleHumanSelected(string selectedHumanName)
    {
        if (!theirList.Contains(selectedHumanName))
        {
            theirList.Add(selectedHumanName);
            Debug.Log("Added selected human name: " + selectedHumanName);
        }
        else
        {
            Debug.Log("Selected human name already exists: " + selectedHumanName);
        }

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
            string childPrefabName = readycode;
            DaysManager.Days currentDay = daysManager.GetCurrentDay();
            HandleHumanSelected(DaysManager.SelectedHumanName);

            Debug.Log(currentDay);
            Debug.Log(childPrefabName);
             switch (currentDay.ToString())
            {
                case "Day1":
                    path = "2D_set/human/Day1/";
                    break;
                case "Day2":
                    path = "2D_set/human/Day2/";
                    break;
                case "Day3":
                    path = "2D_set/human/Day3/";
                    break;
            }

            string human1 = "human1";
            string human2 = "human2";
            string human3 = "human3";

            foreach (string selectedHumanName in theirList)
            {
                if (childPrefabName.Contains(selectedHumanName))
                {
                    if (selectedHumanName == theirList[0])
                    {
                        human1 = "thief1";
                        human2 = "thief2";
                        human3 = "thief3";
                        Debug.Log("this guy!!!");
                    }
                    else if (selectedHumanName == theirList[1])
                    {
                        human1 = human2 = human3 = "itembreaker";
                        Debug.Log("wow-------");
                    }
                    else if (selectedHumanName == theirList[2])
                    {
                        human1 = "murderer1";
                        human2 = "murderer2";
                        human3 = "murderer3";
                        Debug.Log("let me go............................");
                    }
                    break;
                }
            }

            Debug.Log(human1 + " " + human2 + " " + human3);

            if (childPrefabName != null)
            {
                GameObject parentGameObject = GameObject.Find("-----Human_Bag-----");
                GameObject referenceGameObject = GameObject.Find("-----Human_Bag-----");

                GameObject childPrefab = null;

                if (childPrefabName.Contains("human1"))
                {
                    childPrefab = Resources.Load<GameObject>(path + human1);
                }
                else if (childPrefabName.Contains("human2"))
                {
                    childPrefab = Resources.Load<GameObject>(path + human2);
                }
                else if (childPrefabName.Contains("human3"))
                {
                    childPrefab = Resources.Load<GameObject>(path + human3);
                }

                if (childPrefab != null)
                {
                    prefab = Instantiate(childPrefab, parentGameObject.transform);

                    if (referenceGameObject != null)
                    {
                        prefab.transform.position = referenceGameObject.transform.position;
                        // 修改这里，将默认的旋转应用到预制件
                        prefab.transform.rotation = referenceGameObject.transform.rotation * childPrefab.transform.rotation;
                    }
                    else
                    {
                        Debug.LogWarning("ReferencePoint not found!");
                    }
                }
                else
                {
                    Debug.LogWarning("Prefab not found for: " + childPrefabName);
                }
            }

        }


        if (prefab != null)
        {
            switch (readytag)
            {
                case "Food":
                    TakeMeOut(prefab, foodPoint, foodScale, foodOffset, foodInitialRotation, foodMoveSpeed, foodDestinationPoint, "Food_39");
                    //foodList.Add(prefab);
                    Debug.Log("-----Food coming-----");
                    break;
                case "Tool":
                    TakeMeOut(prefab, toolPoint, toolScale, toolOffset, toolInitialRotation, toolMoveSpeed, toolDestinationPoint, "Tool_39");
                    //toolList.Add(prefab);
                    Debug.Log("-----Tool coming-----");
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
            GameObject parentObject = null;

            if (newTag == "Tool_39")
            {
                // 寻找名为 "-----Tool_Bag-----" 的游戏物体作为Tool的父物体
                parentObject = GameObject.Find("-----Tool_Bag-----");
            }
            else if (newTag == "Food_39")
            {
                // 寻找名为 "-----Food_Bag-----" 的游戏物体作为Food的父物体
                parentObject = GameObject.Find("-----Food_Bag-----");
            }

            if (parentObject != null)
            {
                GameObject instance = Instantiate(prefab, point.position, Quaternion.Euler(initialRotation), parentObject.transform);

                // 根据标签将实例添加到相应的列表中
                switch (instance.gameObject.tag)
                {
                    case "Food":
                        foodList.Add(instance);
                        break;
                    case "Tool":
                        toolList.Add(instance);
                        break;
                    case "Human":
                        humanList.Add(instance);
                        break;
                }

                currentSortingOrder += 1;
                instance.GetComponent<Renderer>().sortingOrder = currentSortingOrder;

                Vector3 initialScale = instance.transform.localScale;
                Vector3 adjustedScale = new Vector3(initialScale.x * scale.x, initialScale.y * scale.y, initialScale.z * scale.z);
                instance.transform.localScale = adjustedScale;

                instance.transform.rotation = Quaternion.Euler(instance.transform.rotation.eulerAngles.x, instance.transform.rotation.eulerAngles.y, prefab.transform.rotation.eulerAngles.z);

                StartCoroutine(MoveItem(instance.transform, moveSpeed, destinationPoint, newTag));
            }
            else
            {
                Debug.Log("Parent object not found for tag: " + newTag);
            }
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
