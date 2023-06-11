using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeIteam : MonoBehaviour
{
private void OnTriggerEnter(Collider other)
    {     
            Destroy(other.gameObject); // ɾ����ײ��������
            // play sound
            GetComponent<AudioSource>().Play();

          //  Debug.Log("goodby");
       
    }
}
