using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using Unity.VisualScripting;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    private bool isData=false;
    public LevelDataCurrent levelData;
    public GetItem getItem;
    public List<GameObject> human_List;
    private GameObject levelObject;
    private int currentCount=0;
    private GameObject badFood;

    // Start is called before the first frame update
    void Start()
    {
        
        levelData=GameObject.FindObjectOfType<LevelDataCurrent>();
        getItem=GameObject.FindObjectOfType<GetItem>();
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if(levelData._food_Power<=0)
        {
            float foodTime=0;
            foodTime+=Time.deltaTime;
            if(foodTime>=levelData._foodBad_Time)
            {
                foodTime=0;
                //从getItem.foodList中选择一个物体
                SelectBadFood();
                badFood.GetComponent<SpriteRenderer>().sprite=badFood.GetComponent<GetItem2dData>()._badGameObject;
                badFood.GetComponent<GetItem2dData>()._itemUserNum--;
                
                
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
            }while(badFood==null);
            

            // 处理选择的物体
            // ...
        }
    }
}

