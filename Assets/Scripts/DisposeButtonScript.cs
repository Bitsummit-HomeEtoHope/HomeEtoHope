using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeButtonScript : MonoBehaviour
{
    
    private bool _isPause = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick()
    {
        if(_isPause){
            PauseTriggle.Instance.isPause = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _isPause = ItemsManager.Instance.GetIsPause();
    }
}
