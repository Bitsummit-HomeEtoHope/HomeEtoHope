using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeIteam : MonoBehaviour
{
private void OnTriggerEnter(Collider other)
    {     
            Destroy(other.gameObject); // 删除碰撞到的物体
    
          //  Debug.Log("goodby");
       
    }
}
