using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeIteam : MonoBehaviour
{
private void OnTriggerEnter(Collider other)
    {     
            // play sound
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject); // ɾ����ײ��������
    
          //  Debug.Log("goodby");
       
    }
}
