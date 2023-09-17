using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using Unity.VisualScripting;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    private float humanTime=0;
    private float foodTime=0;
    private float toolTime=0;
    private bool isData=false;
    public LevelDataCurrent levelData;
    public GetItem getItem;
    public List<GameObject> human_List;
    private GameObject levelObject;
    private int currentCount=0;
    private GameObject badFood;
    private GameObject badTool;
    private GameObject badHuman;
    public bool foodBool;
    public bool humanBool;
    public bool toolBool;

    // Start is called before the first frame update
    void Start()
    {
        
        foodBool=GameObject.Find("enerugi_food").GetComponent<enerugiScript>()._powerZeroFood;
        humanBool=GameObject.Find("enerugi_human").GetComponent<enerugiScript>()._powerZeroHuman;
        toolBool=GameObject.Find("enerugi_tool").GetComponent<enerugiScript>()._powerZeroTool;
        levelData=GameObject.FindObjectOfType<LevelDataCurrent>();
        getItem=GameObject.FindObjectOfType<GetItem>();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        getItem=GameObject.FindObjectOfType<GetItem>();
        foodBool=GameObject.Find("enerugi_food").GetComponent<enerugiScript>()._powerZeroFood;
        humanBool=GameObject.Find("enerugi_human").GetComponent<enerugiScript>()._powerZeroHuman;
        toolBool=GameObject.Find("enerugi_tool").GetComponent<enerugiScript>()._powerZeroTool;
        levelObject = GameObject.Find("-----Human_Bag-----");
        if(isData==false)
        {
            levelData=GameObject.FindObjectOfType<LevelDataCurrent>();
            if(levelData!=null)
            {
                isData=true;
            }

        }
        if(currentCount!=levelObject.transform.childCount)
        {
            for(int i =levelObject.transform.childCount-1 ; i < levelObject.transform.childCount; i++)
            {
                Transform child = levelObject.transform.GetChild(i);
                human_List.Add(child.gameObject);
            }
            currentCount=levelObject.transform.childCount;
        }
        if(foodBool)
        {
            
            foodTime+=Time.deltaTime;
            Debug.Log(foodTime+"77777");
            if(foodTime>=levelData._foodBad_Time)
            {
                foodTime=0;
                //从getItem.foodList中选择一个物体
                SelectBadFood();
               
                badFood.GetComponent<SpriteRenderer>().sprite=badFood.GetComponent<GetItem2dData>()._badGameObject;
                badFood.GetComponent<GetItem2dData>()._itemUserNum--;
                badFood.GetComponent<GetItem2dData>()._isBad=true;
                
                
            }
            
        }
        if(toolBool)
        {
            
            toolTime+=Time.deltaTime;
            
            if(toolTime>=levelData._toolBad_Time)
            {
                toolTime=0;
                //从getItem.foodList中选择一个物体
                SelectBadTool();
                badTool.GetComponent<SpriteRenderer>().sprite=badTool.GetComponent<Zuo_Tool>()._badTool;
                badTool.GetComponent<GetItem2dData>()._itemUserNum--;
                badTool.GetComponent<GetItem2dData>()._isBad=true;
                
                
            }
            
        }
        if(toolBool)
        {
            
            humanTime+=Time.deltaTime;
            
            if(humanTime>=levelData._humanBad_Time)
            {
                humanTime=0;
                //从getItem.foodList中选择一个物体
                SelectBadHuman();
                badHuman.GetComponent<FSM>().TransitState(StateType.Dying);

                
                
            }
            
        }
        

    }
    private void SelectBadFood()
    {
        // 从 getItem.foodList 中选择一个物体
        if (getItem.foodList.Count > 0)
        {
            do{
                int randomIndex = Random.Range(0, getItem.foodList.Count);
                badFood = getItem.foodList[randomIndex];
            }while(badFood==null&&badFood.GetComponent<GetItem2dData>()._isBad==true);
            

            // 处理选择的物体
            // ...
        }
    }
    private void SelectBadTool()
    {
        // 从 getItem.toolList 中选择一个物体
        if (getItem.toolList.Count > 0)
        {
            do{
                int randomIndex = Random.Range(0, getItem.toolList.Count);
                badTool = getItem.toolList[randomIndex];
            }while(badTool==null&&badTool.GetComponent<GetItem2dData>()._isBad==true);
            

            // 处理选择的物体
            // ...
        }
    }
    private void SelectBadHuman()
    {
        // 从 getItem.toolList 中选择一个物体
        if (human_List.Count > 0)
        {
            do{
                int randomIndex = Random.Range(0, human_List.Count);
                badHuman = human_List[randomIndex];
            }while(badHuman==null);
            

            // 处理选择的物体
            // ...
        }
    }
}

