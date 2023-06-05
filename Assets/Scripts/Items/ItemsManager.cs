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

    private float defaultHeight;
    private float pauseHeight;
    private Quaternion defaultRotation;
    private GameObject _go;
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
        if (_go == null)
        {
            
            _go = GameObject.Instantiate(Resources.Load(type)) as GameObject;
            _go.transform.localScale *= 3;
            Debug.Log(_go);
            
            switch (type)
            {
                case "carrot":
                {
                    _go.transform.rotation = Quaternion.Euler(90,0,0);
                    _go.transform.position = initPosition.position + new Vector3(0,0.2f,0);
                }
                    break;
                
                case "banana":
                {
                    
                    _go.transform.position = initPosition.position + new Vector3(0,0.2f,0);
                }
                    break;
                
                default:
                {
                    _go.transform.position = initPosition.position;
                }
                    break;
            }
            defaultRotation = _go.transform.rotation;
            defaultHeight = _go.transform.position.y;
            pauseHeight = defaultHeight + 0.5f;

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
        if (_go.transform.position.x < sendPosition.position.x && !DisposeButtonScript.Instance.GetIsDispose())
        {
            _go.transform.position -= new Vector3((float)(0.7*-Time.deltaTime), 0, 0);
        }
    }

    private void SendItem()
    {
        if (_go.transform.position.x > sendPosition.position.x)
        {
            Debug.Log("SendPosition"+sendPosition.position.x);
            Debug.Log("SendItem"+_go.transform.position.x);
            Destroy(_go.gameObject);
        }
    }

    private void DisposeItem()
    {
        if (_go.transform.position.z > disposePosition.position.z)
        {
            Destroy(_go.gameObject);
            DisposeButtonScript.Instance.SetIsDispose(false);
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
    }

    public GameObject GetGo()
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
        InitializeItem(RandomSelectItem());
        MoveItems();
        SendItem();
        DisposeItem();
    }
    
}