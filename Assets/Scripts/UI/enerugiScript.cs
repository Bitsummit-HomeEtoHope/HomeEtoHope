using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enerugiScript : MonoBehaviour
{
    public List<GameObject> enerugiList = new List<GameObject>();


    public float powerChangeTime = 3;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            powerUp();
        }     
    }

    public void powerUp()
    {

    }

    public void powerDown()
    {

    }

    public void powerChange()
    {

    }

    public void powerZero()
    {

    }
}
