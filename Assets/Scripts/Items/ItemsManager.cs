using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : SingletonManager<ItemsManager>
{
    [SerializeField]
    private float spawnInterval = 5f;
    [SerializeField]
    private float moveTime = 6f;
    private LevelDataCurrent _levelDataCurrent;
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

    public Vector3 itemScale = new Vector3(3f, 3f, 3f);

    private readonly Dictionary<ItemsType, string> _itemsDictionary = new Dictionary<ItemsType, string>();
    private enum ItemsType
    {
        //---bad---
        BadApple1,
        BadApple2,
        BadAppleHi,
        BadAppleQaq,
        EggplantQaq,
        GreenPepperQaq,
        OrangeQaq,
        PumpkinQaq,
        //---good---
        Apple,
        Eggplant,
        GreenPepper,
        Orange,
        Pumpkin,
        //---human---
        //   People

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

        //---human---
        Human,
        Human1,
        Human2,
        Human3,
        Human4,
        Human5
    }

    private void AddItemsDictionary()
    {
        //---food---bad---
        _itemsDictionary.Add(ItemsType.BadApple1, "3D/food/bad/appleqaq");
        _itemsDictionary.Add(ItemsType.BadApple2, "3D/food/bad/applehi");
        _itemsDictionary.Add(ItemsType.BadAppleHi, "3D/food/bad/applehi");
        _itemsDictionary.Add(ItemsType.BadAppleQaq, "3D/food/bad/appleqaq");
        _itemsDictionary.Add(ItemsType.EggplantQaq, "3D/food/bad/eggplantqaq");
        _itemsDictionary.Add(ItemsType.GreenPepperQaq, "3D/food/bad/greenpepperqaq");
        _itemsDictionary.Add(ItemsType.OrangeQaq, "3D/food/bad/orangeqaq");
        _itemsDictionary.Add(ItemsType.PumpkinQaq, "3D/food/bad/pumpkinqaq");

        //---food---good---
        _itemsDictionary.Add(ItemsType.Apple, "3D/food/good/apple");
        _itemsDictionary.Add(ItemsType.Eggplant, "3D/food/good/eggplant");
        _itemsDictionary.Add(ItemsType.GreenPepper, "3D/food/good/greenpepper");
        _itemsDictionary.Add(ItemsType.Orange, "3D/food/good/orange");
        _itemsDictionary.Add(ItemsType.Pumpkin, "3D/food/good/pumpkin");

        //---tool---good---
        _itemsDictionary.Add(ItemsType.Burner, "3D/tool/good/burner");
        _itemsDictionary.Add(ItemsType.Chainsaw, "3D/tool/good/chainsaw");
        _itemsDictionary.Add(ItemsType.Hammer, "3D/tool/good/hammer");
        _itemsDictionary.Add(ItemsType.Hoe, "3D/tool/good/hoe");
        _itemsDictionary.Add(ItemsType.Ice, "3D/tool/good/iceaxe");
        _itemsDictionary.Add(ItemsType.Kettle, "3D/tool/good/kettle");
        _itemsDictionary.Add(ItemsType.Knife, "3D/tool/good/nife");
        _itemsDictionary.Add(ItemsType.Shovel, "3D/tool/good/shovel");
        _itemsDictionary.Add(ItemsType.Wrench, "3D/tool/good/wrench");

        //---human---good---
        _itemsDictionary.Add(ItemsType.Human, "3D/human/human");
        _itemsDictionary.Add(ItemsType.Human1, "3D/human/human1");
        _itemsDictionary.Add(ItemsType.Human2, "3D/human/human2");
        _itemsDictionary.Add(ItemsType.Human3, "3D/human/human3");
        _itemsDictionary.Add(ItemsType.Human4, "3D/human/human4");
        _itemsDictionary.Add(ItemsType.Human5, "3D/human/human5");
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

            // 获取物体当前的缩放值
            Vector3 currentScale = itemsArray[itemsArrayIndex].transform.localScale;

            // 根据物体当前的缩放值调整缩放倍率
            itemsArray[itemsArrayIndex].transform.localScale = new Vector3(currentScale.x * itemScale.x, currentScale.y * itemScale.y, currentScale.z * itemScale.z);

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
            float moveSpeed = (disposePosition.position.x - initPosition.position.x) / moveTime;
            itemsArray[i].transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }

        if (itemsArray[i] != null && itemsArray[i].transform.position.x >= disposePosition.position.x && itemsArray[i].layer != LayerMask.NameToLayer("Send"))
        {
            itemsArray[i].layer = LayerMask.NameToLayer("Send"); // 更改物体的图层为"Send"
            //itemsArray[i].tag = DisposeTag; // 将物体的标签更改为"DisposeTag"
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

        float distance = 1.0f; // 移动距离
        Vector3 backDirection = -Vector3.forward;
        Vector3 targetPosition = item.transform.position + backDirection * distance;

        item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, 1.0f * Time.deltaTime);
    }

    private void Start()
    {
        _levelDataCurrent=FindObjectOfType<LevelDataCurrent>();
        moveTime = _levelDataCurrent._interval;
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
        else if (spendTime >= spawnInterval)
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
