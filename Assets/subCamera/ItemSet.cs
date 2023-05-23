using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSet : MonoBehaviour
{
    //�A�C�e���̐ݒu�ʒu
    //Location of items
    public Transform m_ItemSetPos;
    //���݂̃A�C�e��
    //Current Items
    public GameObject m_Item;
    //�A�C�e���̃J�����I�u�W�F�N�g
    //Camera object of the item
    public GameObject m_ItemCameraObject;
    [System.Serializable]
    public struct ItemDataBase
    {
        //�A�C�e���̖��O
        //Item Name
        public string m_Name;
        //�A�C�e���̃v���n�u
        //Prefabrication of items
        public GameObject m_Items;
    };
    public List<ItemDataBase> itemDataBases = new List<ItemDataBase>();//�A�C�e���̃f�[�^�x�[�X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Item)
        {
            //���݂̃A�C�e�������݂��Ȃ��ꍇ
            //If the current item does not exist
            if (m_ItemCameraObject)
            {
                //�A�C�e���̃J�����I�u�W�F�N�g��j������
                //Destroy the item's camera object
                Destroy(m_ItemCameraObject);
            }
            //�hFruit�h�^�O�̕t�����A�C�e����T��
            //Search for items tagged "Fruit"
            m_Item = GameObject.FindWithTag("Fruit");
        }
        else
        {
            //���݂̃A�C�e�������݂���ꍇ
            //If the current item exists
            if (!m_ItemCameraObject)
            {
                //�A�C�e���̃J�����I�u�W�F�N�g�����݂��Ȃ��ꍇ
                //If the item's camera object does not exist
                foreach (ItemDataBase Dummy in itemDataBases)
                {
                    if (Dummy.m_Name == m_Item.name)
                    {
                        //�A�C�e���̃f�[�^�x�[�X�����v����A�C�e����T��
                        //Find matching items in the database of items
                        //�A�C�e���̃v���n�u�𐶐����A�ݒu�ʒu�ɔz�u����
                        //Generate a prefabrication of the item and place it at the installation location
                        m_ItemCameraObject = Instantiate(Dummy.m_Items, m_ItemSetPos.position, m_ItemSetPos.rotation);
                        //�A�C�e���̉�]��ݒ肷��
                        //Set item rotation
                        m_ItemCameraObject.transform.rotation = m_Item.transform.rotation;
                        return;
                    }
                }
            }
            else
                //�A�C�e���̃J�����I�u�W�F�N�g�̉�]���X�V����
                //Update the rotation of the item's camera object
                m_ItemCameraObject.transform.rotation = m_Item.transform.rotation;
        }
    }
}
