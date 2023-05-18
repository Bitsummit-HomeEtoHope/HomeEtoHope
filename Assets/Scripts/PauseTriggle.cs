using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTriggle : MonoBehaviour
{
    public static PauseTriggle Instance;
    private void Awake() {
        Instance = this;
    }  
    public bool isPause=false;
    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter");
        if (other.gameObject.tag == "Fruit")
        {
            isPause = true;
        }
    }
}
