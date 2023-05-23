using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSet : MonoBehaviour
{
    //アイテムの設置位置
    //Location of items
    public Transform m_ItemSetPos;
    //現在のアイテム
    //Current Items
    public GameObject m_Item;
    //アイテムのカメラオブジェクト
    //Camera object of the item
    public GameObject m_ItemCameraObject;
    [System.Serializable]
    public struct ItemDataBase
    {
        //アイテムの名前
        //Item Name
        public string m_Name;
        //アイテムのプレハブ
        //Prefabrication of items
        public GameObject m_Items;
    };
    public List<ItemDataBase> itemDataBases = new List<ItemDataBase>();//アイテムのデータベース

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Item)
        {
            //現在のアイテムが存在しない場合
            //If the current item does not exist
            if (m_ItemCameraObject)
            {
                //アイテムのカメラオブジェクトを破棄する
                //Destroy the item's camera object
                Destroy(m_ItemCameraObject);
            }
            //”Fruit”タグの付いたアイテムを探す
            //Search for items tagged "Fruit"
            m_Item = GameObject.FindWithTag("Fruit");
        }
        else
        {
            //現在のアイテムが存在する場合
            //If the current item exists
            if (!m_ItemCameraObject)
            {
                //アイテムのカメラオブジェクトが存在しない場合
                //If the item's camera object does not exist
                foreach (ItemDataBase Dummy in itemDataBases)
                {
                    if (Dummy.m_Name == m_Item.name)
                    {
                        //アイテムのデータベースから一致するアイテムを探す
                        //Find matching items in the database of items
                        //アイテムのプレハブを生成し、設置位置に配置する
                        //Generate a prefabrication of the item and place it at the installation location
                        m_ItemCameraObject = Instantiate(Dummy.m_Items, m_ItemSetPos.position, m_ItemSetPos.rotation);
                        //アイテムの回転を設定する
                        //Set item rotation
                        m_ItemCameraObject.transform.rotation = m_Item.transform.rotation;
                        return;
                    }
                }
            }
            else
                //アイテムのカメラオブジェクトの回転を更新する
                //Update the rotation of the item's camera object
                m_ItemCameraObject.transform.rotation = m_Item.transform.rotation;
        }
    }
}
