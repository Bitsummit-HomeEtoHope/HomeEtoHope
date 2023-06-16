using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Analytics;

public class ItemSet : MonoBehaviour
{
    public ItemsManager m_itemMng;
    private int m_lastItemIndex = 0;
    private bool m_isFoundObject = false;

    public criminal m_Criminal;

    //Location of items
    public Transform m_ItemSetPos;

    //Current Items
    public GameObject m_Item;

    //Camera object of the item
    public GameObject m_ItemCameraObject;
    [System.Serializable]
    public struct ItemDataBase
    {
        //Item Name
        public string m_Name;
        //Prefabrication of items
        public GameObject m_Items;
    };
    public List<ItemDataBase> itemDataBases = new List<ItemDataBase>();
  
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //if (!m_Item)
        if(m_lastItemIndex != m_itemMng.itemsArrayIndex)
        {
            //If the current item does not exist
            if (m_ItemCameraObject)
            {
                //Destroy the item's camera object
                Destroy(m_ItemCameraObject);
                // m_Criminal.ResetTextPosition();
            }

            if (m_itemMng._isCanRotate)
            {
                //Search for items tagged "Fruit"
                //m_Item = GameObject.FindWithTag("Food");
                //m_Item = GameObject.FindWithTag("Tool");
                m_Item = m_itemMng.itemsArray[m_lastItemIndex];
                m_lastItemIndex = m_itemMng.itemsArrayIndex;
                m_isFoundObject = false;
            }
        }
        else if(m_Item != null)
        {
            //If the current item exists
            if (!m_ItemCameraObject)
            {
                if (!m_isFoundObject)
                {
                    m_isFoundObject = true;
                    //If the item's camera object does not exist
                    foreach (ItemDataBase Dummy in itemDataBases)
                    {
                        if (Dummy.m_Name == m_Item.name)
                        {
                            //Find matching items in the database of items
                            //Generate a prefabrication of the item and place it at the installation location
                            m_ItemCameraObject = Instantiate(Dummy.m_Items, m_ItemSetPos.position, m_ItemSetPos.rotation);
                            //Set item rotation
                            m_ItemCameraObject.transform.rotation = m_Item.transform.rotation;
                            return;
                        }
                    }
                }
            }
            else
            {
                //m_Criminal.MoveTextLeft();
                //Update the rotation of the item's camera object
                m_ItemCameraObject.transform.rotation = m_Item.transform.rotation;
            }
        }
    }
   
}
