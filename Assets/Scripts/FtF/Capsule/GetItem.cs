using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEditor;

public class GetItem : MonoBehaviour
{
    [SerializeField] private Transform foodPoint; // Food位置的Transform
    [SerializeField] private Transform toolPoint; // Tool位置的Transform
    [SerializeField] private Transform humanPoint; // Human位置的Transform

    [Header("Placement Settings")]
    [SerializeField] private int orderInLayer = 2; // Order in Layer

    [Header("Food")]
    [SerializeField] private Vector3 foodScale = new Vector3(0.5f, 0.5f, 0.5f); // 食物尺寸Scale
    [SerializeField] private Vector3 foodOffset = new Vector3(0f, 0f, 0f); // 食物相对偏移量

    [Header("Tool")]
    [SerializeField] private Vector3 toolScale = new Vector3(1f, 1f, 1f); // 工具尺寸Scale
    [SerializeField] private Vector3 toolOffset = new Vector3(0f, 0f, 0f); // 工具相对偏移量

    [Header("Human")]
    [SerializeField] private Vector3 humanScale = new Vector3(0.8f, 0.8f, 0.8f); // 人物尺寸Scale
    [SerializeField] private Vector3 humanOffset = new Vector3(0f, 0f, 0f); // 人物相对偏移量

    public string readytag;
    public string readycode;

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

        // 在指定文件夹内查找与 readycode 名称相同的预制件
        if (readytag == "Food")
        {
            prefab = Resources.Load<GameObject>("2d_set/food/" + readycode);
        }
        else if (readytag == "Tool")
        {
            prefab = Resources.Load<GameObject>("2d_set/tool/" + readycode);
        }
        else if (readytag == "Human")
        {
            prefab = Resources.Load<GameObject>("2d_set/human/" + readycode);
        }

        if (prefab != null)
        {
            switch (readytag)
            {
                case "Food":
                    AdjustAndPlacePrefab(prefab, foodPoint, foodScale, foodOffset);
                    break;
                case "Tool":
                    AdjustAndPlacePrefab(prefab, toolPoint, toolScale, toolOffset);
                    break;
                case "Human":
                    AdjustAndPlacePrefab(prefab, humanPoint, humanScale, humanOffset);
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

    private void AdjustAndPlacePrefab(GameObject prefab, Transform point, Vector3 scale, Vector3 offset)
    {
        if (point != null)
        {
            GameObject instance = Instantiate(prefab, point.position, point.rotation);
            instance.transform.parent = point;
            instance.GetComponent<Renderer>().sortingOrder = orderInLayer;

            // 获取道具的初始比例
            Vector3 initialScale = instance.transform.localScale;

            // 根据调整比例进行乘法运算
            Vector3 adjustedScale = new Vector3(initialScale.x * scale.x, initialScale.y * scale.y, initialScale.z * scale.z);

            instance.transform.localScale = adjustedScale;
            instance.transform.localPosition += offset;

            // 保存预制件的Z轴旋转值
            instance.transform.localRotation = Quaternion.Euler(instance.transform.localRotation.eulerAngles.x, instance.transform.localRotation.eulerAngles.y, prefab.transform.rotation.eulerAngles.z);
        }
    }
}
