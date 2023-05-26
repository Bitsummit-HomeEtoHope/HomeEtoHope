using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendButtonScript : MonoBehaviour
{
    private GameObject itemsManager;
    private GameObject items = null;
    private bool _sendItems = false;
    private bool _isPause = false;

    void Start()
    {

    }

    // if push the 'Send'Button, item can be move
    public void OnClick()
    {
        if(items && _isPause){
            Debug.Log("push SendButton");
            _sendItems = true;
        }
    }

    void Update()
    {
        _isPause = ItemsManager.Instance.GetIsPause();
        if(!items){
            Debug.Log("No items");
            items = ItemsManager.Instance.GetGo();
        }else{
            // move object
            if (_sendItems && _isPause)
            {
                items.transform.position += transform.forward * 1.0f * Time.deltaTime;
            }
        }
        if(!_isPause){
            _sendItems = false;
        }
    }
}

