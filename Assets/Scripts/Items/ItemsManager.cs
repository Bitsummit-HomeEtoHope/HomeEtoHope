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
    private GameObject[] itemsArray;
    private int itemsArrayIndex;

    private const string DisposeTag = "Dispose";

    public Vector3 itemScale = new Vector3(3f, 3f, 3f);

    private readonly Dictionary<ItemsType, string> _itemsDictionary = new Dictionary<ItemsType, string>();
    private enum ItemsType
    {
        //---bad---
        BadApple1,
        BadApple2,
        //---good---
        Apple,
        Eggplant,
        GreenPepper,
        Orange,
        Pumpkin,
        //---human---
   //     People
    }

    private void AddItemsDictionary()
    {
        ////---test---
        //_itemsDictionary.Add(ItemsType.BadApple1, "3D/food/bad/bad-apple1");
        //_itemsDictionary.Add(ItemsType.BadApple2, "3D/food/bad/bad-apple2");
        //_itemsDictionary.Add(ItemsType.Apple, "3D/food/good/apple");

        //----------------food----good-----------------------
        // _itemsDictionary.Add(ItemsType.Apple, "3D/food/bad/apple");
        _itemsDictionary.Add(ItemsType.BadApple1, "3D/food/bad/bad-apple1");
        _itemsDictionary.Add(ItemsType.BadApple2, "3D/food/bad/bad-apple2");

        //----------------food----bad------------------------
        // _itemsDictionary.Add(ItemsType.Apple, "3D/food/good/apple");
        _itemsDictionary.Add(ItemsType.Apple, "3D/food/good/apple");
        _itemsDictionary.Add(ItemsType.Eggplant, "3D/food/good/eggplant");
        _itemsDictionary.Add(ItemsType.GreenPepper, "3D/food/good/greenpepper");
        _itemsDictionary.Add(ItemsType.Orange, "3D/food/good/orange");
        _itemsDictionary.Add(ItemsType.Pumpkin, "3D/food/good/pumpkin");

        //----------------human----good---------------------
     //   _itemsDictionary.Add(ItemsType.People, "3D/human/people");
    }

    private string RandomSelectItem()
    {
        var index = UnityEngine.Random.Range(0, _itemsDictionary.Count);
        selectedItem = _itemsDictionary[(ItemsType)index];
        return selectedItem;
    }


    private void InitializeItem(string type)
    {
        if (itemsArray[itemsArrayIndex] == null)
        {
            itemsArray[itemsArrayIndex] = GameObject.Instantiate(Resources.Load(type)) as GameObject;
            itemsArray[itemsArrayIndex].transform.localScale = itemScale;

            itemsArray[itemsArrayIndex].transform.localScale *= 3;
            Debug.Log(itemsArray[itemsArrayIndex]);

            defaultRotation = itemsArray[itemsArrayIndex].transform.rotation;
            defaultHeight = itemsArray[itemsArrayIndex].transform.position.y;
            pauseHeight = defaultHeight + 0.5f;

            // ����������Z���ϵ�λ��
            itemsArray[itemsArrayIndex].transform.position = initPosition.position;

            // �����������ת�Ƕ�
            itemsArray[itemsArrayIndex].transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

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


    private void MoveItems()
    {
        for (int i = 0; i < 2; i++)
        {
            if (itemsArray[i] != null && itemsArray[i].transform.position.x < disposePosition.position.x && !itemsArray[i].CompareTag(DisposeTag))
            {
                itemsArray[i].transform.position -= new Vector3((float)(0.52 * -Time.deltaTime), 0, 0);
            }

            // ��������ǩ�Ƿ�Ϊ "Dispose"
            if (itemsArray[i] != null && itemsArray[i].CompareTag(DisposeTag))
            {
                // ִ��������� "Dispose" ״̬���߼�
                Disposed(itemsArray[i]);
            }
        }
    }


    private void Disposed(GameObject item)
    {
        _isCanRotate = false;

        float distance = 1.0f; // �ƶ�����

        Vector3 backDirection = -Vector3.forward;

        Vector3 targetPosition = item.transform.position + backDirection * distance;

        item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, 1.0f * Time.deltaTime);

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

    private void Update()
    {
        if (itemsArray[0] == null && itemsArray[1] == null)
        {
            _isCanRotate = false;
            InitializeItem(RandomSelectItem());
            MoveItems();
            spendTime = 0; // ��ʱ������Ϊ0
        }
        else if (spendTime >= 5f)
        {
            InitializeItem(RandomSelectItem());
            MoveItems();
            _isCanRotate = false;
            spendTime = 0; // ��ʱ������Ϊ0
        }
        else
        {
            spendTime += Time.deltaTime;
            MoveItems();
        }
    }
}
