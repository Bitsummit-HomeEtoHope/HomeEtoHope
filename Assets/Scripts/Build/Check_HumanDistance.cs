using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_HumanDistance : MonoBehaviour
{
    public float check_Distance=1f;
    public float check_Timer=1f;
    public float check_Time=0f;
    public float isBuildTimer=0.15f;
    public float isBuildTime=0f;
    [SerializeField]
    public bool isBuild=false;

    private void Update() {
        if(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position,transform.position)<check_Distance)
        {
            isBuildTime+=Time.deltaTime;
            if(isBuildTime>isBuildTimer)
            {
                isBuild=true;
            }
            //isBuild=true;
        }
        if(isBuild)
        {
            check_Time+=Time.deltaTime;
            if(check_Time>check_Timer)
            {
                this.GetComponent<SpriteRenderer>().enabled=false;
            }
            
        }
        if(!isBuild)
        {
            check_Time=0f;
        }

    }

}
