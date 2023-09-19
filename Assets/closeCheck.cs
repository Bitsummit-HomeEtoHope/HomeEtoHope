using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeCheck : MonoBehaviour
{
   private bool on1 = false;
   private bool on2 = false;
   private bool on3 = false;
   private bool on4 = false;

    private void OnEnable()
    {if (!on1 && gameObject.activeSelf) on2 = true;
     if (on2)on3 = true;
     if(on3)on4 = true;
        Debug.Log("xxx");
    }
    private void OnDisable()
    {
        if (on4)
        {
            Destroy(gameObject);
        }
    }
}
