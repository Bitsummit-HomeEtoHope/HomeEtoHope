using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : SingletonManager<ItemsManager>
{
    [SerializeField]
    private float spawnInterval;
    [SerializeField]
    private float moveTime = 6f;

    public bool _isCanRotate = false;

    public Transform initPosition;
    public Transform disposePosition;

    private string selectedItem;
    private LevelDataCurrent _levelDataCurrent;

    private float spendTime;

    private float defaultHeight;
    private float pauseHeight;
    private Quaternion defaultRotation;
    private GameObject _go;
    public GameObject[] itemsArray;
    public int itemsArrayIndex;
    public Vector3 itemScale = new Vector3(3f, 3f, 3f);

    private const string DisposeTag = "Dispose";

    [Header("Desipose Item")]
    [SerializeField] private float dushSpeed = 2.0f;

    [Header ("Send Item")]
    [SerializeField] private bool sendFood = true;
    [SerializeField] private bool sendTool = true;
    [SerializeField] private bool sendHuman = true;

    [Header("Send ")]
    private bool startGame = true;
    [SerializeField, Range(0, 100)]
    private int food_goodWeight = 100 / 5; 

    [SerializeField, Range(0, 100)]
    private int food_badWeight = 100 / 5;

    [SerializeField, Range(0, 100)]
    private int tool_goodWeight = 100 / 9; 

    [SerializeField, Range(0, 100)]
    private int tool_badWeight = 100 / 9; 

    [SerializeField, Range(0, 100)]
    private int humanWeight = 100 / 21;

    public class WeightedItem<T>
    {
        public T Item { get; set; }
        public int Weight { get; set; }

        public WeightedItem(T item, int weight)
        {
            Item = item;
            Weight = weight;
        }
    }
    private Dictionary<ItemsType, WeightedItem<string>> _itemsDictionary = new Dictionary<ItemsType, WeightedItem<string>>();
    private enum ItemsType
    {
        //---food---good---
        Apple,
        Eggplant,
        GreenPepper,
        Orange,
        Pumpkin,

        //---food---bad---
        AppleQAQ,
        EggplantQAQ,
        GreenPepperQAQ,
        OrangeQAQ,
        PumpkinQAQ,

        //---food---hi---
        AppleHI,
        EggplantHI,
        GreenPepperHI,
        OrangeHI,
        PumpkinHI,

        //---tool---good---
        Burner,
        Chainsaw,
        Hammer,
        Hoe,
        Ice,
        Kettle,
        Knife,
        Shovel,
        Wrench,

        //---tool---bad---
        BurnerQAQ,
        ChainsawQAQ,
        HammerQAQ,
        HoeQAQ,
        IceQAQ,
        KettleQAQ,
        KnifeQAQ,
        ShovelQAQ,
        WrenchQAQ,

        //---tool---2---
        _BurnerQAQ,
        _ChainsawQAQ,
        _HammerQAQ,
        _HoeQAQ,
        _IceQAQ,
        _KettleQAQ,
        _KnifeQAQ,
        _ShovelQAQ,
        _WrenchQAQ,


        //---human---
        Human1_0, Human2_0, Human3_0,
        Human1_1, Human2_1, Human3_1,
        Human1_2, Human2_2, Human3_2,
        Human1_3, Human2_3, Human3_3,
        Human1_4, Human2_4, Human3_4,
        Human1_5, Human2_5, Human3_5,
        Human1_6, Human2_6, Human3_6,

      //  Human0_0,
    }


    private void addFood()
    {
        //---food---good---
        _itemsDictionary.Add(ItemsType.Apple,           new WeightedItem<string>("3D/food/good/apple", food_goodWeight));
        _itemsDictionary.Add(ItemsType.Eggplant,        new WeightedItem<string>("3D/food/good/eggplant", food_goodWeight));
        _itemsDictionary.Add(ItemsType.GreenPepper,     new WeightedItem<string>("3D/food/good/greenpepper", food_goodWeight));
        _itemsDictionary.Add(ItemsType.Orange,          new WeightedItem<string>("3D/food/good/orange", food_goodWeight));
        _itemsDictionary.Add(ItemsType.Pumpkin,         new WeightedItem<string>("3D/food/good/pumpkin", food_goodWeight));

        //---food---bad---
        _itemsDictionary.Add(ItemsType.AppleQAQ,        new WeightedItem<string>("3D/food/bad/appleqaq", food_badWeight));
        _itemsDictionary.Add(ItemsType.EggplantQAQ,     new WeightedItem<string>("3D/food/bad/eggplantqaq", food_badWeight));
        _itemsDictionary.Add(ItemsType.GreenPepperQAQ,  new WeightedItem<string>("3D/food/bad/greenpepperqaq", food_badWeight));
        _itemsDictionary.Add(ItemsType.OrangeQAQ,       new WeightedItem<string>("3D/food/bad/orangeqaq", food_badWeight));
        _itemsDictionary.Add(ItemsType.PumpkinQAQ,      new WeightedItem<string>("3D/food/bad/pumpkinqaq", food_badWeight));


        //---food---hi---
        _itemsDictionary.Add(ItemsType.AppleHI,         new WeightedItem<string>("3D/food/bad/applehi", food_badWeight));
        _itemsDictionary.Add(ItemsType.EggplantHI,      new WeightedItem<string>("3D/food/bad/eggplanthi", food_badWeight));
        _itemsDictionary.Add(ItemsType.GreenPepperHI,   new WeightedItem<string>("3D/food/bad/greenpepperhi", food_badWeight));
        _itemsDictionary.Add(ItemsType.OrangeHI,        new WeightedItem<string>("3D/food/bad/orangehi", food_badWeight));
        _itemsDictionary.Add(ItemsType.PumpkinHI,       new WeightedItem<string>("3D/food/bad/pumpkinhi", food_badWeight));
    }

    private void addTool()
    {
        //---tool---good---
        _itemsDictionary.Add(ItemsType.Burner,      new WeightedItem<string>("3D/tool/good/burner", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Chainsaw,    new WeightedItem<string>("3D/tool/good/chainsaw", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Hammer,      new WeightedItem<string>("3D/tool/good/hammer", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Hoe,         new WeightedItem<string>("3D/tool/good/hoe", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Ice,         new WeightedItem<string>("3D/tool/good/iceax", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Kettle,      new WeightedItem<string>("3D/tool/good/kettle", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Knife,       new WeightedItem<string>("3D/tool/good/nife", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Shovel,      new WeightedItem<string>("3D/tool/good/shovel", tool_goodWeight));
        _itemsDictionary.Add(ItemsType.Wrench,      new WeightedItem<string>("3D/tool/good/wrench", tool_goodWeight));

        //---tool---bad---
        _itemsDictionary.Add(ItemsType.BurnerQAQ,   new WeightedItem<string>("3D/tool/bad/burnerbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.ChainsawQAQ, new WeightedItem<string>("3D/tool/bad/chainsawbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.HammerQAQ,   new WeightedItem<string>("3D/tool/bad/hammerbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.HoeQAQ,      new WeightedItem<string>("3D/tool/bad/hoebroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.IceQAQ,      new WeightedItem<string>("3D/tool/bad/iceaxbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.KettleQAQ,   new WeightedItem<string>("3D/tool/bad/kettlebroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.KnifeQAQ,    new WeightedItem<string>("3D/tool/bad/nifebroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.ShovelQAQ,   new WeightedItem<string>("3D/tool/bad/shovelbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType.WrenchQAQ,   new WeightedItem<string>("3D/tool/bad/wrenchbroken", tool_badWeight));

        //---tool---bad---
        _itemsDictionary.Add(ItemsType._BurnerQAQ,      new WeightedItem<string>("3D/tool/bad/_burnerbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._ChainsawQAQ,    new WeightedItem<string>("3D/tool/bad/_chainsawbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._HammerQAQ,      new WeightedItem<string>("3D/tool/bad/_hammerbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._HoeQAQ,         new WeightedItem<string>("3D/tool/bad/_hoebroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._IceQAQ,         new WeightedItem<string>("3D/tool/bad/_iceaxbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._KettleQAQ,      new WeightedItem<string>("3D/tool/bad/_kettlebroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._KnifeQAQ,       new WeightedItem<string>("3D/tool/bad/_nifebroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._ShovelQAQ,      new WeightedItem<string>("3D/tool/bad/_shovelbroken", tool_badWeight));
        _itemsDictionary.Add(ItemsType._WrenchQAQ,      new WeightedItem<string>("3D/tool/bad/_wrenchbroken", tool_badWeight));
    }

    private void addHuman()
    {
        //---human-1---
        _itemsDictionary.Add(ItemsType.Human1_0, new WeightedItem<string>("3D/human/human1/human1_0", humanWeight));
        _itemsDictionary.Add(ItemsType.Human1_1, new WeightedItem<string>("3D/human/human1/human1_1", humanWeight));
        _itemsDictionary.Add(ItemsType.Human1_2, new WeightedItem<string>("3D/human/human1/human1_2", humanWeight));
        _itemsDictionary.Add(ItemsType.Human1_3, new WeightedItem<string>("3D/human/human1/human1_3", humanWeight));
        _itemsDictionary.Add(ItemsType.Human1_4, new WeightedItem<string>("3D/human/human1/human1_4", humanWeight));
        _itemsDictionary.Add(ItemsType.Human1_5, new WeightedItem<string>("3D/human/human1/human1_5", humanWeight));
        _itemsDictionary.Add(ItemsType.Human1_6, new WeightedItem<string>("3D/human/human1/human1_6", humanWeight));

        //---human-2---
        _itemsDictionary.Add(ItemsType.Human2_0, new WeightedItem<string>("3D/human/human2/human2_0", humanWeight));
        _itemsDictionary.Add(ItemsType.Human2_1, new WeightedItem<string>("3D/human/human2/human2_1", humanWeight));
        _itemsDictionary.Add(ItemsType.Human2_2, new WeightedItem<string>("3D/human/human2/human2_2", humanWeight));
        _itemsDictionary.Add(ItemsType.Human2_3, new WeightedItem<string>("3D/human/human2/human2_3", humanWeight));
        _itemsDictionary.Add(ItemsType.Human2_4, new WeightedItem<string>("3D/human/human2/human2_4", humanWeight));
        _itemsDictionary.Add(ItemsType.Human2_5, new WeightedItem<string>("3D/human/human2/human2_5", humanWeight));
        _itemsDictionary.Add(ItemsType.Human2_6, new WeightedItem<string>("3D/human/human2/human2_6", humanWeight));

        //---human-3---
        _itemsDictionary.Add(ItemsType.Human3_0, new WeightedItem<string>("3D/human/human3/human3_0", humanWeight));
        _itemsDictionary.Add(ItemsType.Human3_1, new WeightedItem<string>("3D/human/human3/human3_1", humanWeight));
        _itemsDictionary.Add(ItemsType.Human3_2, new WeightedItem<string>("3D/human/human3/human3_2", humanWeight));
        _itemsDictionary.Add(ItemsType.Human3_3, new WeightedItem<string>("3D/human/human3/human3_3", humanWeight));
        _itemsDictionary.Add(ItemsType.Human3_4, new WeightedItem<string>("3D/human/human3/human3_4", humanWeight));
        _itemsDictionary.Add(ItemsType.Human3_5, new WeightedItem<string>("3D/human/human3/human3_5", humanWeight));
        _itemsDictionary.Add(ItemsType.Human3_6, new WeightedItem<string>("3D/human/human3/human3_6", humanWeight));

        //--------------
        //_itemsDictionary.Add(ItemsType.Human0_0, new WeightedItem<string>("3D/human/human3/human0_0", 0));
    }

    //---------

    private string RandomSelectItem()
    {
        int totalWeight = CalculateTotalWeight(_itemsDictionary);

        int randomValue = UnityEngine.Random.Range(0, totalWeight);

        foreach (var item in _itemsDictionary)
        {
            randomValue -= item.Value.Weight;

            if (randomValue < 0)
            {
                selectedItem = item.Value.Item;
                return selectedItem;
            }
        }

        // 如果未找到目标，重置计时器并进行下一次物品随机与生成
        spendTime = spawnInterval;
        return null;
    }

    private int CalculateTotalWeight(Dictionary<ItemsType, WeightedItem<string>> items)
    {
        int totalWeight = 0;

        foreach (var item in items.Values)
        {
            totalWeight += item.Weight;
        }

        return totalWeight;
    }

    //-----------

    private void InitializeItem(string type)
    {
        if (itemsArray[itemsArrayIndex] == null)
        {
            GameObject parentObject = GameObject.Find("---items---");

            if (parentObject != null)
            {
                GameObject itemPrefab = Resources.Load(type) as GameObject;

                if (itemPrefab != null)
                {
                    itemsArray[itemsArrayIndex] = GameObject.Instantiate(itemPrefab, parentObject.transform);
                    itemsArray[itemsArrayIndex].transform.localScale = Vector3.Scale(itemsArray[itemsArrayIndex].transform.localScale, itemScale);

                    defaultRotation = itemsArray[itemsArrayIndex].transform.rotation;
                    defaultHeight = itemsArray[itemsArrayIndex].transform.position.y;
                    pauseHeight = defaultHeight + 0.5f;

                    itemsArray[itemsArrayIndex].transform.position = initPosition.position;
                    itemsArray[itemsArrayIndex].transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);

                    itemsArrayIndex = (itemsArrayIndex + 1) % itemsArray.Length;
                }
                else
                {
                    // 如果加载的对象为空，直接进行下一次物品随机与生成
                    itemsArrayIndex = (itemsArrayIndex + 1) % itemsArray.Length;
                    InitializeNextItem();
                }
            }
        }
    }

    private void InitializeNextItem()
    {
        string itemType = RandomSelectItem();
        if (itemType != null)
        {
            InitializeItem(itemType);
        }
        else
        {
            // 如果未找到目标，重置计时器并进行下一次物品随机与生成
            spendTime = 4;

            // 打印信息
            Debug.Log("---yyyyyyyyyyyyyyyyyyyyyyyyyy--- " + itemType);
        }
    }





    private void MoveItems()
    {
        for (int i = 0; i < 4; i++)
        {
            if (itemsArray[i] != null && itemsArray[i].transform.position.x < disposePosition.position.x && !itemsArray[i].CompareTag(DisposeTag))
            {
                float moveSpeed = (disposePosition.position.x - initPosition.position.x) / moveTime;
                itemsArray[i].transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }

            if (itemsArray[i] != null && itemsArray[i].transform.position.x >= disposePosition.position.x && itemsArray[i].layer != LayerMask.NameToLayer("Send"))
            {
                itemsArray[i].layer = LayerMask.NameToLayer("Send");
                Debug.Log("Object layer changed to Send: " + itemsArray[i].name);
            }

            if (itemsArray[i] != null && itemsArray[i].CompareTag(DisposeTag))
            {
                Disposed(itemsArray[i]);
            }
        }
    }

    private void Disposed(GameObject item)
    {
        float distance = 1.0f;
        Vector3 backDirection = -Vector3.forward;
        Vector3 targetPosition = item.transform.position + backDirection * distance;
        item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, dushSpeed * Time.deltaTime);
    }


    private void OnEnable()
    {
        GetComponent<AudioSource>().Play();

        Array.Clear(itemsArray, 0, itemsArray.Length);
        spendTime = 4;

        GameObject parentObject = GameObject.Find("---items---"); 

        if (parentObject != null)
        {
            int childCount = parentObject.transform.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                Transform child = parentObject.transform.GetChild(i);
                Destroy(child.gameObject);
            }
        }

        _levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        food_goodWeight = _levelDataCurrent._food_goodWeight;
        food_badWeight = _levelDataCurrent._food_badWeight;
        tool_goodWeight = _levelDataCurrent._tool_goodweight;
        tool_badWeight = _levelDataCurrent._tool_badweight;
        humanWeight = _levelDataCurrent._human_weight;
        moveTime = _levelDataCurrent._interval;
        spawnInterval = _levelDataCurrent._spawnInterval;
    }

    private void Start()
    {

        Application.targetFrameRate = 120;

        if (sendFood) addFood();
        if (sendTool) addTool();
        if (sendHuman) addHuman();

        itemsArray = new GameObject[4];
        itemsArrayIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            itemsArray[i] = null;
        }
    }

    public GameObject GetSelectItem()
    {
        return _go;
    }


    private void Update()
    {
        //if (startGame)
        //{
        //    spendTime = 4;
        //    if (spendTime >= spawnInterval)
        //    {
        //        InitializeItem("3D/human/human3/human0_0");
        //        MoveItems();
        //    }
        //    spendTime += Time.deltaTime;
        //    MoveItems();

        //    startGame = false;
        //}

        if (spendTime >= spawnInterval)
        {
            InitializeItem(RandomSelectItem());
            MoveItems();
            spendTime = 0;
        }
        spendTime += Time.deltaTime;
        MoveItems();

        //if (spendTime >= spawnInterval)
        //{
        //    Debug.Log("spendTime >= spawnInterval");
        //    InitializeItem(RandomSelectItem());
        //    MoveItems();
        //   // _isCanRotate = false;
        //    spendTime = 0;
        //}
        //spendTime += Time.deltaTime;
        //MoveItems();
    }
}
