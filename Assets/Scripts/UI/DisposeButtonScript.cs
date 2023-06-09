<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * Script to detect and process dispose button presses
 * 
 * @auther Yuichi Kawasaki
 * @date   2023/06/04
 **/
=======
using UnityEngine;

>>>>>>> dev
public class DisposeButtonScript : MonoBehaviour
{
    private static DisposeButtonScript _instance;
    public static DisposeButtonScript Instance => _instance;

<<<<<<< HEAD
    private GameObject itemsManager;
    private GameObject items = null;
    private bool _isDispose = false;

    /** 
     * Set up an instance of this split
     * 
     * @auther Yuichi Kawasaki
     * @date   2023/06/04
     **/
=======
    private GameObject clickedObject; // �����������

>>>>>>> dev
    void Awake()
    {
        _instance = this;
    }

<<<<<<< HEAD
    /** 
     * The Dispose button is clicked
     * Make it possible to change the direction of movement of items
     * 
     * @auther Yuichi Kawasaki
     * @date   2023/06/04
     **/
    public void OnClick()
    {
        if(items){
            _isDispose = true;
            ItemsManager.Instance.Pause();
        }
    }

    /** 
     * can get the "_isDispose"
     * 
     * @auther Yuichi Kawasaki
     * @date   2023/06/04
     **/
    public bool GetIsDispose()
    {
        return _isDispose;
    }

    /** 
     * can set the "_isDispose"
     * 
     * @auther Yuichi Kawasaki
     * @date   2023/06/04
     **/
    public void SetIsDispose(bool canMove)
    {
        _isDispose = canMove;
    }

    /** 
     * Update is called once per frame
     * If "_isDispose" is true, move item up
     * 
     * @auther Yuichi Kawasaki
     * @date   2023/06/04
     **/
    void Update()
    {

        if(!items){
            //Debug.Log("No items");
            items = ItemsManager.Instance.GetSelectItem();
        }else{
            // move object
            if (_isDispose)
            {
                items.transform.position += transform.forward * 1.0f * Time.deltaTime;
            }
        }

    }
=======
    public void ReceiveClickedObject(GameObject clickedObject)
    {
        this.clickedObject = clickedObject;
    }

    public void OnClick()
    {
        if (clickedObject != null)
        {
            Debug.Log("Clicked Object: " + clickedObject.name);
            // ������ִ�а�ť������ָ��������߼�

            // ������ı�ǩ����Ϊ "Dispose"
            clickedObject.tag = "Dispose";
        }
    }
>>>>>>> dev
}
