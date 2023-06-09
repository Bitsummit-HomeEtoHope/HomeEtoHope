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
<<<<<<< HEAD
    public Transform sendPosition;
    public Transform disposePosition;
    
    private string selectedItem;
    public string konoItem;

    private float spendTime;

=======
    public Transform disposePosition;

    private string selectedItem;

    private float spendTime;


>>>>>>> dev
    private float defaultHeight;
    private float pauseHeight;
    private Quaternion defaultRotation;
    private GameObject _go;
    private GameObject[] itemsArray;
    private int itemsArrayIndex;
<<<<<<< HEAD
    // private bool _isPause=false;
    // private bool _currentIsPause=false;
    // public bool IsPause
    // {
    //     get => _isPause;
    //     set 
    //     {
    //         _isPause = value;
    //         Debug.Log("IsPause:"+IsPause);
    //         Debug.Log("_currentIsPause:"+_currentIsPause);
    //         if(_isPause&&_currentIsPause!=IsPause)
    //         {
    //             _currentIsPause=IsPause;
    //             _go.transform.position=new Vector3(_go.transform.position.x,pauseHeight,_go.transform.position.z);
                
    //             Invoke("Pause",pauseTime);
    //         }
    //     }
    // }
=======

    private const string DisposeTag = "Dispose";

    public Vector3 itemScale = new Vector3(3f, 3f, 3f);

>>>>>>> dev
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
<<<<<<< HEAD
        if (itemsArray[itemsArrayIndex] == null)
        {
            
            itemsArray[itemsArrayIndex] = GameObject.Instantiate(Resources.Load(type)) as GameObject;
            itemsArray[itemsArrayIndex].transform.localScale *= 3;
            Debug.Log(itemsArray[itemsArrayIndex]);
            
            switch (type)
            {
                case "carrot":
                {
                    itemsArray[itemsArrayIndex].transform.rotation = Quaternion.Euler(90,0,0);
                    itemsArray[itemsArrayIndex].transform.position = initPosition.position + new Vector3(0,0.2f,0);
                }
                    break;
                
                case "banana":
                {
                    
                    itemsArray[itemsArrayIndex].transform.position = initPosition.position + new Vector3(0,0.2f,0);
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
            if(itemsArrayIndex == 0)
            {
                itemsArrayIndex = 1;
            }else
            {
                itemsArrayIndex = 0;
            }

            Debug.Log(selectedItem);
            konoItem = selectedItem;
        }
=======
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
>>>>>>> dev
    }

    private string RandomSelectItem()
    {
        var index = UnityEngine.Random.Range(0, _itemsDictionary.Count);
        selectedItem = _itemsDictionary[(ItemsType)index];
        return selectedItem;
<<<<<<< HEAD
    }

    private void MoveItems()
    {
        for(int i = 0; i < 2; i++)
        {
            if (itemsArray[i] != null && itemsArray[i].transform.position.x < sendPosition.position.x && !DisposeButtonScript.Instance.GetIsDispose())
            {
                itemsArray[i].transform.position -= new Vector3((float)(0.73*-Time.deltaTime), 0, 0);
            }
        }
    }

    private void SendItem()
    {
        for(int i = 0; i < 2; i++)
        {
            if (itemsArray[i] != null && itemsArray[i].transform.position.x > sendPosition.position.x)
            {
                Debug.Log("SendItem"+itemsArray[i].transform.position.x);

                // Process to send item name

                Destroy(itemsArray[i].gameObject);
                itemsArray[i] = null;
            }
        }
    }

    /** 
     * If there is an item in the array and the condition is met, dispose the item.
     * 
     * @auther Yuichi Kawasaki
     * @date   2023/06/06
     **/
    private void DisposeItem()
    {
        for(int i = 0; i < 2; i++)
        {
            if (itemsArray[i] != null && itemsArray[i].transform.position.z > disposePosition.position.z)
            {
                Destroy(itemsArray[i].gameObject);
                itemsArray[i] = null;
                DisposeButtonScript.Instance.SetIsDispose(false);
            }
        }
    }
=======
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

            // 调整道具在Z轴上的位置
            itemsArray[itemsArrayIndex].transform.position = initPosition.position;

            // 设置随机的旋转角度
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

>>>>>>> dev

    private void Start()
    {
        Application.targetFrameRate = 120;
        AddItemsDictionary();
        itemsArray = new GameObject[2];
        itemsArrayIndex = 0;
<<<<<<< HEAD
        for(int i = 0; i < 2; i++)
=======
        for (int i = 0; i < 2; i++)
>>>>>>> dev
        {
            itemsArray[i] = null;
        }
    }

<<<<<<< HEAD
    /** 
     * can get currently selected item
     * 
=======
    /**
     * can get currently selected item
     *
>>>>>>> dev
     * @auther Yuichi Kawasaki
     * @date   2023/06/06
     **/
    public GameObject GetSelectItem()
    {
        return _go;
    }

<<<<<<< HEAD
    public void Pause()
    {
        _isCanRotate = false;
        _go.transform.position=new Vector3(_go.transform.position.x,defaultHeight,_go.transform.position.z);
        _go.transform.rotation = defaultRotation;
    }
=======

>>>>>>> dev

    private void Update()
    {
<<<<<<< HEAD
        if(itemsArray[0] == null && itemsArray[1] == null)
        {
            InitializeItem(RandomSelectItem());
            MoveItems();
            SendItem();
            DisposeItem();
        }else if(spendTime >= 5f)
        {
            InitializeItem(RandomSelectItem());
            MoveItems();
            SendItem();
            DisposeItem();
            spendTime = 0;
        }else 
        {
            spendTime += Time.deltaTime;
            MoveItems();
            SendItem();
            DisposeItem();
        }
    }
    
}
=======
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
>>>>>>> dev
