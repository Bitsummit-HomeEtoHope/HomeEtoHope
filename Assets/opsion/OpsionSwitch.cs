using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpsionSwitch : MonoBehaviour
{
    public GameObject system;

    public GameObject sound;
    
    public void GOSystem()
    {
        system.SetActive(true);
        sound.SetActive(false);
    }
    
    public void GoSound()
    {
        system.SetActive(false);
        sound.SetActive(true);
    }
}
