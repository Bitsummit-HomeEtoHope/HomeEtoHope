using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemsManager : SingletonManager<ItemsManager>
{
    public bool _isCanRotate = false;
    public Transform initPosition;
    public Transform disposePosition;

    private string selectedItem;

    private float spendTime;


    private float defaultHeight;
    private float pauseHeight;
    private Quaternion defaultRotation;
    private GameObject _go;
    private GameObject[] itemsArray;
    private int itemsArrayIndex;

    private const string DisposeTag = "Dispose";


    private readonly Dictionary<ItemsType, string> _itemsDictionary = new Dictionary<ItemsType, string>();
    private enum ItemsType
    {
        Apple,
        // AppleCore,
        Banana,
        Carrot,
        BadApple,
    }

    private void InitializeItem(string type)
    {
        if (itemsArray[itemsArrayIndex] == null)
        {

            itemsArray[itemsArrayIndex] = GameObject.Instantiate(Resources.Load(type)) as GameObject;
            itemsArray[itemsArrayIndex].transform.localScale *= 3;
            Debug.Log(itemsArray[itemsArrayIndex]);

            switch (type)
            {
                case "carrot":
                    {
                        itemsArray[itemsArrayIndex].transform.rotation = Quaternion.Euler(90, 0, 0);
                        itemsArray[itemsArrayIndex].transform.position = initPosition.position + new Vector3(0, 0.2f, 0);
                    }
                    break;

                case "banana":
                    {

                        itemsArray[itemsArrayIndex].transform.position = initPosition.position + new Vector3(0, 0.2f, 0);
                    }
                    break;

                default:
                    {
                        itemsArray[itemsArrayIndex].transform.position = initPosition.position;
                    }
                    break;
            }
            defaultRotation = itemsArray[itemsArrayIndex].transform.rotation;
            defaultHeight = itemsArray[itemsArrayIndex].transform.position.y;
            pauseHeight = defaultHeight + 0.5f;

            // change itemsArrayIndex
            if (itemsArrayIndex == 0)
            {
                itemsArrayIndex = 1;
            }
            else
            {
                itemsArrayIndex = 0;
            }
        }
    }

    private string RandomSelectItem()
    {
        var index = UnityEngine.Random.Range(0, _itemsDictionary.Count);
        selectedItem = _itemsDictionary[(ItemsType)index];
        return selectedItem;
    }

    private void MoveItems()
    {
        for (int i = 0; i < 2; i++)
        {
            if (itemsArray[i] != null && itemsArray[i].transform.position.x < disposePosition.position.x && !itemsArray[i].CompareTag(DisposeTag))
            {
                itemsArray[i].transform.position -= new Vector3((float)(0.73 * -Time.deltaTime), 0, 0);
            }

            // 检测物体标签是否为 "Dispose"
            if (itemsArray[i] != null && itemsArray[i].CompareTag(DisposeTag))
            {
                // 执行物体进入 "Dispose" 状态的逻辑
                Disposed(itemsArray[i]);
            }
        }
    }


    private void Disposed(GameObject item)
    {
        _isCanRotate = false; 
       
        float distance = 1.0f; // 移动距离

        Vector3 backDirection = -Vector3.forward;

        Vector3 targetPosition = item.transform.position + backDirection * distance;

        item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, 1.0f * Time.deltaTime);

    }

    private void AddItemsDictionary()
    {
        _itemsDictionary.Add(ItemsType.Apple, "apple");
        //    _itemsDictionary.Add(ItemsType.AppleCore,"apple-core");
        _itemsDictionary.Add(ItemsType.Banana, "banana");
        _itemsDictionary.Add(ItemsType.Carrot, "carrot");
        _itemsDictionary.Add(ItemsType.BadApple, "bad-apple");
    }

    private void Start()
    {
        Application.targetFrameRate = 120;
        AddItemsDictionary();
        itemsArray = new GameObject[2];
        itemsArrayIndex = 0;
        for (int i = 0; i < 2; i++)
        {
            itemsArray[i] = null;
        }
    }

    /** 
     * can get currently selected item
     * 
     * @auther Yuichi Kawasaki
     * @date   2023/06/06
     **/
    public GameObject GetSelectItem()
    {
        return _go;
    }



    private void Update()
    {
        if (itemsArray[0] == null && itemsArray[1] == null)
        {
            _isCanRotate = false;
            InitializeItem(RandomSelectItem());
            MoveItems();
            spendTime = 0; // 将时间重置为0
        }
        else if (spendTime >= 5f)
        {          
            InitializeItem(RandomSelectItem());
            MoveItems();
            _isCanRotate = false;
            spendTime = 0; // 将时间重置为0
        }
        else
        {
            spendTime += Time.deltaTime;
            MoveItems();
        }
    }


}