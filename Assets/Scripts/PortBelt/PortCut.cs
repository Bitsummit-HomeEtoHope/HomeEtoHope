using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortCut : MonoBehaviour
{
    // OnTriggerEnter is called when another Collider enters the trigger
    public Transform setpoint;
    private void OnTriggerEnter(Collider other)
    {

        // Check if the collider belongs to a GameObject you want to move
        // You can use tags, layers, or other means to identify the specific GameObjects to move
        // For example, let's move all GameObjects with the "Port" tag
        if (other.CompareTag("Port"))
        {
            // Move the GameObject to (0, 1, -2.4) position
            other.transform.position = setpoint.transform.position;
        }
    }
}
