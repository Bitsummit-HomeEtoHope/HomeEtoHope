using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Manager : MonoBehaviour
{
    public Camera camera2d;
    public List<GameObject> buildList=new List<GameObject>();
    public bool isAllBuild=false;
    private void Update() 
    {
       isAllBuild=IsAllBuild();
       if(isAllBuild)
       {
        //camera2d的fieldOfView平滑过渡到75
        //camera2d的position平滑过渡到(0,275,0)
        camera2d.fieldOfView=Mathf.Lerp(camera2d.fieldOfView,75,Time.deltaTime*2);
        camera2d.gameObject.transform.position=Vector3.Lerp(camera2d.gameObject.transform.position,new Vector3(camera2d.gameObject.transform.position.x,275,camera2d.gameObject.transform.position.z),Time.deltaTime*2);
        
        //camera2d.fieldOfView=75;
        //camera2d.gameObject.transform.position=new Vector3(camera2d.gameObject.transform.position.x,275,camera2d.gameObject.transform.position.z);
        //bulidList所有物体SetActive(true)
        foreach (var item in buildList)
        {
            item.SetActive(true);
        }
           
           
       }


    }
    public bool IsAllBuild()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
           var childIsBuild=child.GetComponent<Check_HumanDistance>();
           if(childIsBuild!=null&&!childIsBuild.isBuild)
           {
               return false;
           }
        }
        return true;
    }

}
