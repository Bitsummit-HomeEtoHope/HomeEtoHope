using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Analytics;

public class ItemSet : MonoBehaviour
{
    public ItemsManager m_itemMng;
    private bool m_isFoundObject = false;

    public criminal m_Criminal;

    //Location of items
    public Transform m_ItemSetPos;

    //Current Items
    public GameObject m_Item;

    //The Tag
    public string m_tag;

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
        m_tag = CodeHolder.selectedObject.tag;


        //if (!m_Item)
        if (m_Item == null )
        {
            //If the current item does not exist
            if (m_ItemCameraObject != null)
            {
                //Destroy the item's camera object
                Destroy(m_ItemCameraObject);
                // m_Criminal.ResetTextPosition();
            }

            if (CodeHolder.selectedObject != null)
            {
                //Search for items tagged "Fruit"
                //m_Item = GameObject.FindWithTag("Food");
                //m_Item = GameObject.FindWithTag("Tool");
                m_Item = CodeHolder.selectedObject.gameObject;
                m_isFoundObject = false;
            }
        }
        else if(m_Item != null)
        {
            //If the current item exists
            if (!m_ItemCameraObject )
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
                        }else if (m_tag == "Human")
                        {
                            m_Item.name = "human";

                        }

                    }
                }
            }
            else
            {
                //m_Criminal.MoveTextLeft();
                //Update the rotation of the item's camera object
                if (m_Item == CodeHolder.selectedObject.gameObject )
                {
                    m_ItemCameraObject.transform.rotation = m_Item.transform.rotation;
                }
                else
                {
                    m_Item = null;
                }
            }
        }

        //if (m_tag == "Human")
        //{
        //    if (m_ItemCameraObject != null)
        //    {
        //        //Destroy the item's camera object
        //        Destroy(m_ItemCameraObject);
        //        // m_Criminal.ResetTextPosition();
        //    }

        //    if (CodeHolder.selectedObject != null)
        //    {
        //        //Search for items tagged "Fruit"
        //        //m_Item = GameObject.FindWithTag("Food");
        //        //m_Item = GameObject.FindWithTag("Tool");
        //        m_Item = CodeHolder.selectedObject.gameObject;
        //        m_isFoundObject = false;
        //    }
        //    Destroy(m_ItemCameraObject);    
        //}
    }
   
}
