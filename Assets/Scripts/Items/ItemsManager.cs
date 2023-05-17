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
    public Transform initPosition;
    public Transform destroyPosition;
    private GameObject _go;
    private readonly Dictionary<ItemsType, string> _itemsDictionary = new Dictionary<ItemsType, string>();
    private enum ItemsType
    {
        Apple,
        AppleCore,
        Banana,
        Carrot,
    }

    private void InitializeItem(string type)
    {
        if (_go == null)
        {
            _go = GameObject.Instantiate(Resources.Load(type)) as GameObject;
            _go.transform.localScale *= 3;
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
        }
    }

    private string RandomSelectItem()
    {
        var index = UnityEngine.Random.Range(0, _itemsDictionary.Count);
        return _itemsDictionary[(ItemsType)index];
    }
    
    private void MoveItems()
    {
        if (_go.transform.position.x > destroyPosition.position.x)
        {
            _go.transform.position += new Vector3(-Time.deltaTime, 0, 0);
        }
    }

    private void DestroyItem()
    {
        if (_go.transform.position.x < destroyPosition.position.x)
        {
            Destroy(_go.gameObject);
        }
    }

    private void AddItemsDictionary()
    {
        _itemsDictionary.Add(ItemsType.Apple,"apple");
        _itemsDictionary.Add(ItemsType.AppleCore,"apple-core");
        _itemsDictionary.Add(ItemsType.Banana,"banana");
        _itemsDictionary.Add(ItemsType.Carrot,"carrot");
    }
    
    private void Start()
    {
        AddItemsDictionary();
    }

    private void Update()
    {
        InitializeItem(RandomSelectItem());
        MoveItems();
        DestroyItem();
    }
}
