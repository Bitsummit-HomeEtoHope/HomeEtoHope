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
    public float pauseTime = 5f;
    public Transform initPosition;
    public Transform destroyPosition;
    
    private float defaultHeight;
    private GameObject pauseTriggle;
    private float pauseHeight;
    private Quaternion defaultRotation;
    private GameObject _go;
    private bool _isPause=false;
    private bool _currentIsPause=false;
    public bool IsPause
    {
        get => _isPause;
        set 
        {
            _isPause = value;
            Debug.Log("IsPause:"+IsPause);
            Debug.Log("_currentIsPause:"+_currentIsPause);
            if(_isPause&&_currentIsPause!=IsPause)
            {
                _currentIsPause=IsPause;
                pauseTriggle.SetActive(false);
                _go.transform.position=new Vector3(_go.transform.position.x,pauseHeight,_go.transform.position.z);
                
                
                Invoke("Pause",pauseTime);
            }

            
        }
    }
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
            defaultRotation = _go.transform.rotation;
            defaultHeight = _go.transform.position.y;
            pauseHeight = defaultHeight + 0.5f;
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
            pauseTriggle.SetActive(true);
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
        pauseTriggle=GameObject.FindObjectOfType<PauseTriggle>().gameObject;
        Application.targetFrameRate = 120;
        AddItemsDictionary();
    }

    private void Pause()
    {
        _isCanRotate = false;
        _go.transform.position=new Vector3(_go.transform.position.x,defaultHeight,_go.transform.position.z);
        _go.transform.rotation = defaultRotation;
        PauseTriggle.Instance.isPause=false;
         _currentIsPause=false;
    }

    
    private void Update()
    {
        IsPause = PauseTriggle.Instance.isPause;
        if(!IsPause)
        {
            InitializeItem(RandomSelectItem());
            MoveItems();
            DestroyItem();
        }
        if(IsPause)
        {
            _isCanRotate = true;
        }
        
    }
    
}
