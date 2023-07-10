using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonManager<GameManager>
{
    public Transform[] foodTarget; 
    public Transform[] toolTarget;
    public float stopDistance = 1f;
    public float moveSpeed = 5f;
    [SerializeField]
    private List<bool> stopFoodFlags;
    [SerializeField]
    private List<bool> stopToolFlags;
    public GetItem getItem;
    public LevelDataCurrent levelDataCurrent;
    [Tooltip("Temporarily replace the shady screen at the end of the level")]
    public GameObject shady;
    [SerializeField]
    public bool isPause;
    [SerializeField]
    
    private int foodCount=0;
    private int toolCount=0;
    public List<GameObject> treeFoodList=new List<GameObject>();
    public Transform treePoint;


    // Start is called before the first frame update
    void Start()
    {
        foodTarget=new Transform[99];
        toolTarget=new Transform[99];
        foodTarget[0]=GameObject.Find("FoodTarget").transform;
        toolTarget[0]=GameObject.Find("ToolTarget").transform;
        getItem= FindObjectOfType<GetItem>();
        
        
        // levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        if (isPause)
        {
            Time.timeScale = 0;
            // ShadyOpen();
        }
    }

    public void ShadyOpen()
    {
        shady.SetActive(true);
    }

    public void ShadyClose()
    {
        //shady.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        
        MoveFoodTarget();
        MoveToolTarget();
        treePoint=treeFoodList[0].transform;
        
    }
    private void MoveFoodTarget()
    {
        
        if(foodCount!=getItem.foodList.Count)
        {
            stopFoodFlags.Add(false);
            foodCount=getItem.foodList.Count;
        }

        
        for(int i=0;i<getItem.foodList.Count;i++)
        {
            
            GameObject currentFood= getItem.foodList[i];
            
            if(currentFood!=null)
            {
                
                Vector3 targetPos= foodTarget[i].position;
                Transform currentPos= currentFood.transform;

                if(!stopFoodFlags[i])
                {
                    Vector3 moveDir= targetPos-currentPos.position;
                    if(moveDir.magnitude>stopDistance)
                    {
                        currentFood.transform.position=Vector3.MoveTowards(currentPos.position,targetPos,moveSpeed*Time.deltaTime);
                        foodTarget[i+1]=currentPos;
                    }
                    else
                    {
                        stopFoodFlags[i]=true;
                        
                    }
                }
            }
        }
    }
     private void MoveToolTarget()
     {
        if(toolCount!=getItem.toolList.Count)
        {
            stopToolFlags.Add(false);
            toolCount=getItem.toolList.Count;
        }

        
        for(int i=0;i<getItem.toolList.Count;i++)
        {
            
            GameObject currentTool= getItem.toolList[i];
            
            if(currentTool!=null)
            {
                
                Vector3 targetPos= toolTarget[i].position;
                Transform currentPos= currentTool.transform;

                if(!stopToolFlags[i])
                {
                    Vector3 moveDir= targetPos-currentPos.position;
                    if(moveDir.magnitude>stopDistance)
                    {
                        currentTool.transform.position=Vector3.MoveTowards(currentPos.position,targetPos,moveSpeed*Time.deltaTime);
                        toolTarget[i+1]=currentPos;
                    }
                    else
                    {
                        stopToolFlags[i]=true;
                        
                    }
                }
            }
        }
     }
}
