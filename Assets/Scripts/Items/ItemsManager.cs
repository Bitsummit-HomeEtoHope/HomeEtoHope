using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : SingletonManager<ItemsManager>
{
    [SerializeField]
    private float spawnInterval = 5f;
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

    [Header ("Send Item")]
    [SerializeField] private bool sendFood = true;
    [SerializeField] private bool sendTool = true;
    [SerializeField] private bool sendHuman = true;

    private readonly Dictionary<ItemsType, string> _itemsDictionary = new Dictionary<ItemsType, string>();
    private enum ItemsType
    {
        //---food---good---
        Apple,
        Eggplant,
        GreenPepper,
        Orange,
        Pumpkin,

        //---food---bad---
        BadAppleHi,
        BadAppleQaq,
        EggplantQaq,
        GreenPepperQaq,
        OrangeQaq,
        PumpkinQaq,

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
        HammerLOL,
        HoeQAQ,
        IceQAQ,
        KettleQAQ,
        KnifeQAQ,
        ShovelQAQ,
        WrenchQAQ,


        //---human---
        Human1_0, Human2_0, Human3_0,
        Human1_1, Human2_1, Human3_1,
        Human1_2, Human2_2, Human3_2,
        Human1_3, Human2_3, Human3_3,
        Human1_4, Human2_4, Human3_4,
        Human1_5, Human2_5, Human3_5,
        Human1_6, Human2_6, Human3_6,
    }
    private void addFood() 
    {
        //---food---good---
        _itemsDictionary.Add(ItemsType.Apple, "3D/food/good/apple");
        _itemsDictionary.Add(ItemsType.Eggplant, "3D/food/good/eggplant");
        _itemsDictionary.Add(ItemsType.GreenPepper, "3D/food/good/greenpepper");
        _itemsDictionary.Add(ItemsType.Orange, "3D/food/good/orange");
        _itemsDictionary.Add(ItemsType.Pumpkin, "3D/food/good/pumpkin");

        //---food---bad---
        _itemsDictionary.Add(ItemsType.BadAppleHi, "3D/food/bad/applehi");
        _itemsDictionary.Add(ItemsType.BadAppleQaq, "3D/food/bad/appleqaq");
        _itemsDictionary.Add(ItemsType.EggplantQaq, "3D/food/bad/eggplantqaq");
        _itemsDictionary.Add(ItemsType.GreenPepperQaq, "3D/food/bad/greenpepperqaq");
        _itemsDictionary.Add(ItemsType.OrangeQaq, "3D/food/bad/orangeqaq");
        _itemsDictionary.Add(ItemsType.PumpkinQaq, "3D/food/bad/pumpkinqaq");
    }
    private void addTool()
    {
        //---tool---good---
        _itemsDictionary.Add(ItemsType.Burner, "3D/tool/good/burner");
        _itemsDictionary.Add(ItemsType.Chainsaw, "3D/tool/good/chainsaw");
        _itemsDictionary.Add(ItemsType.Hammer, "3D/tool/good/hammer");
        _itemsDictionary.Add(ItemsType.Hoe, "3D/tool/good/hoe");
        _itemsDictionary.Add(ItemsType.Ice, "3D/tool/good/iceax");
        _itemsDictionary.Add(ItemsType.Kettle, "3D/tool/good/kettle");
        _itemsDictionary.Add(ItemsType.Knife, "3D/tool/good/nife");
        _itemsDictionary.Add(ItemsType.Shovel, "3D/tool/good/shovel");
        _itemsDictionary.Add(ItemsType.Wrench, "3D/tool/good/wrench");

        //---tool---bad---
        _itemsDictionary.Add(ItemsType.BurnerQAQ, "3D/tool/bad/burnerbroken");
        _itemsDictionary.Add(ItemsType.ChainsawQAQ, "3D/tool/bad/chainsawbroken");
        _itemsDictionary.Add(ItemsType.HammerLOL, "3D/tool/bad/hammerbroken");
        _itemsDictionary.Add(ItemsType.HoeQAQ, "3D/tool/bad/hoebroken");
        _itemsDictionary.Add(ItemsType.IceQAQ, "3D/tool/bad/iceaxbroken");
        _itemsDictionary.Add(ItemsType.KettleQAQ, "3D/tool/bad/kettlebroken");
        _itemsDictionary.Add(ItemsType.KnifeQAQ, "3D/tool/bad/nifebroken");
        _itemsDictionary.Add(ItemsType.ShovelQAQ, "3D/tool/bad/shovelbroken");
        _itemsDictionary.Add(ItemsType.WrenchQAQ, "3D/tool/bad/wrenchbroken");

    }
    private void addHuman()
    {
        //---human-1---
        _itemsDictionary.Add(ItemsType.Human1_0, "3D/human/human1/human1_0");
        _itemsDictionary.Add(ItemsType.Human1_1, "3D/human/human1/human1_1");
        _itemsDictionary.Add(ItemsType.Human1_2, "3D/human/human1/human1_2");
        _itemsDictionary.Add(ItemsType.Human1_3, "3D/human/human1/human1_3");
        _itemsDictionary.Add(ItemsType.Human1_4, "3D/human/human1/human1_4");
        _itemsDictionary.Add(ItemsType.Human1_5, "3D/human/human1/human1_5");
        _itemsDictionary.Add(ItemsType.Human1_6, "3D/human/human1/human1_6");
        //---human-2---
        _itemsDictionary.Add(ItemsType.Human2_0, "3D/human/human2/human2_0");
        _itemsDictionary.Add(ItemsType.Human2_1, "3D/human/human2/human2_1");
        _itemsDictionary.Add(ItemsType.Human2_2, "3D/human/human2/human2_2");
        _itemsDictionary.Add(ItemsType.Human2_3, "3D/human/human2/human2_3");
        _itemsDictionary.Add(ItemsType.Human2_4, "3D/human/human2/human2_4");
        _itemsDictionary.Add(ItemsType.Human2_5, "3D/human/human2/human2_5");
        _itemsDictionary.Add(ItemsType.Human2_6, "3D/human/human2/human2_6");
        //---human-3---
        _itemsDictionary.Add(ItemsType.Human3_0, "3D/human/human3/human3_0");
        _itemsDictionary.Add(ItemsType.Human3_1, "3D/human/human3/human3_1");
        _itemsDictionary.Add(ItemsType.Human3_2, "3D/human/human3/human3_2");
        _itemsDictionary.Add(ItemsType.Human3_3, "3D/human/human3/human3_3");
        _itemsDictionary.Add(ItemsType.Human3_4, "3D/human/human3/human3_4");
        _itemsDictionary.Add(ItemsType.Human3_5, "3D/human/human3/human3_5");
        _itemsDictionary.Add(ItemsType.Human3_6, "3D/human/human3/human3_6");
    }


    private string RandomSelectItem()
    {
        var enumValues = Enum.GetValues(typeof(ItemsType));
        var index = UnityEngine.Random.Range(0, enumValues.Length);
        var selectedItemEnum = (ItemsType)enumValues.GetValue(index);

        while (!_itemsDictionary.ContainsKey(selectedItemEnum))
        {
            index = UnityEngine.Random.Range(0, enumValues.Length);
            selectedItemEnum = (ItemsType)enumValues.GetValue(index);
        }

        selectedItem = _itemsDictionary[selectedItemEnum];
        return selectedItem;
    }



    private void InitializeItem(string type)
    {
        if (itemsArray[itemsArrayIndex] == null)
        {
            itemsArray[itemsArrayIndex] = GameObject.Instantiate(Resources.Load(type)) as GameObject;
            Vector3 currentScale = itemsArray[itemsArrayIndex].transform.localScale;
            itemsArray[itemsArrayIndex].transform.localScale = new Vector3(currentScale.x * itemScale.x, currentScale.y * itemScale.y, currentScale.z * itemScale.z);
            defaultRotation = itemsArray[itemsArrayIndex].transform.rotation;
            defaultHeight = itemsArray[itemsArrayIndex].transform.position.y;
            pauseHeight = defaultHeight + 0.5f;
            itemsArray[itemsArrayIndex].transform.position = initPosition.position;
            itemsArray[itemsArrayIndex].transform.rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
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
        item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, 1.0f * Time.deltaTime);
    }

    private void Start()
    {
        _levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        moveTime = _levelDataCurrent._interval;
        Application.targetFrameRate = 120;

        if (sendFood) addFood();
        if (sendTool) addTool();
        if (sendHuman) addHuman();

        itemsArray = new GameObject[2];
        itemsArrayIndex = 0;
        for (int i = 0; i < 2; i++)
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
        if (itemsArray[0] == null && itemsArray[1] == null)
        {
            _isCanRotate = false;
            InitializeItem(RandomSelectItem());
            MoveItems();
            spendTime = 0;
        }
        else if (spendTime >= spawnInterval)
        {
            InitializeItem(RandomSelectItem());
            MoveItems();
            _isCanRotate = false;
            spendTime = 0;
        }
        else
        {
            spendTime += Time.deltaTime;
            MoveItems();
        }
    }
}
