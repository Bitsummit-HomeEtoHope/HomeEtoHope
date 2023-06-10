using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class GetItem : MonoBehaviour
{
    [SerializeField] private Transform foodPoint; // Food位置的Transform
    [SerializeField] private Transform toolPoint; // Tool位置的Transform
    [SerializeField] private Transform humanPoint; // Human位置的Transform

    [Header("Placement Settings")]
    [SerializeField] private int orderInLayer = 2; // Order in Layer
    [SerializeField] private Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f); // 尺寸Scale
    [SerializeField] private Vector3 localOffset = new Vector3(0f, 0f, 0f); // 相对偏移量


   // public string prefabPath;
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

    private  void CallItem()
    {
        Debug.Log("Ready----: " + readytag);
        Debug.Log("Ready----: " + readycode);

        // 在指定文件夹内查找与 readycode 名称相同的预制件
        GameObject prefab = Resources.Load<GameObject>("2d_set/food/" + readycode);

        if (prefab != null)
        {
            switch (readytag)
            {
                case "Food":
                    PlacePrefab(prefab, foodPoint);
                    break;
                case "Tool":
                    PlacePrefab(prefab, toolPoint);
                    break;
                case "Human":
                    PlacePrefab(prefab, humanPoint);
                    break;
                default:
                    Debug.Log("Invalid tag.");
                    break;
            }
        }
        else
        {
            Debug.Log("Prefab not found for tag: " + readytag);
        }

    }

    private void PlacePrefab(GameObject prefab, Transform point)
    {
        if (point != null)
        {
            GameObject instance = Instantiate(prefab, point.position, point.rotation);
            instance.transform.parent = point;
            instance.GetComponent<Renderer>().sortingOrder = orderInLayer;
            instance.transform.localScale = scale;
            instance.transform.localPosition += localOffset;
        }
    }
}
