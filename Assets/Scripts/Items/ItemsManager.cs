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
    public Transform sendPosition;
    public Transform disposePosition;
    
    private string selectedItem;
    public string konoItem;

    private float spendTime;

    private float defaultHeight;
    private float pauseHeight;
    private Quaternion defaultRotation;
    private GameObject _go;
    private GameObject[] itemsArray;
    private int itemsArrayIndex;
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
    }

    private string RandomSelectItem()
    {
        var index = UnityEngine.Random.Range(0, _itemsDictionary.Count);
        selectedItem = _itemsDictionary[(ItemsType)index];
        return selectedItem;
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

    private void AddItemsDictionary()
    {
        _itemsDictionary.Add(ItemsType.Apple,"apple");
    //    _itemsDictionary.Add(ItemsType.AppleCore,"apple-core");
        _itemsDictionary.Add(ItemsType.Banana,"banana");
        _itemsDictionary.Add(ItemsType.Carrot,"carrot");
        _itemsDictionary.Add(ItemsType.BadApple,"bad-apple");
    }
    
    private void Start()
    {
        Application.targetFrameRate = 120;
        AddItemsDictionary();
        itemsArray = new GameObject[2];
        itemsArrayIndex = 0;
        for(int i = 0; i < 2; i++)
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

    public void Pause()
    {
        _isCanRotate = false;
        _go.transform.position=new Vector3(_go.transform.position.x,defaultHeight,_go.transform.position.z);
        _go.transform.rotation = defaultRotation;
    }

    
    private void Update()
    {
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