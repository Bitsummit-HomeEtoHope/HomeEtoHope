using StateMachine.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealFood : MonoBehaviour
{
    public Transform foodParent;
    public List<GameObject> foodSet = new List<GameObject>();
    public Parameter parameter;
    public Vector3 targetPos;
    public GameObject targetFood;
    public float CDTime;
    public float CDTimer;
    public GetItem getItem;
    private GameObject[] foodArray;
    private int foodCount;

    private void Awake()
    {
        getItem = GameObject.Find("GetItemManager").GetComponent<GetItem>();
        targetPos = new Vector3(0, -175.9f, -203.296f);
        foodParent = GameObject.Find("-----Food_Bag-----").transform;
    }
    private void Start() {
        foodArray = GameObject.FindGameObjectsWithTag("Food_39");
    }
    private void Update()
    {
        GetChildren();
        CDTimer += Time.deltaTime;

        if (foodSet.Count > 0 && CDTimer > CDTime)
        {
            EatFood();
        }
        else
        {
            Wander();
        }
    }

    private void Wander()
    {
        if (Vector3.Distance(targetPos, transform.position) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
                Time.deltaTime * parameter.moveSpeed);
        }
        else
        {
            SwithTarget();
        }
    }

    private void SwithTarget()
    {
        float x = Random.Range(-178, 202);
        float y = Random.Range(-118, -91);
        targetPos = new Vector3(x, y, -203.296f);
    }

    private void GetChildren()
    {
        /* for (int i = 0; i < foodParent.childCount; i++)
        {
            if (foodParent.GetChild(i).CompareTag("Food") && foodParent.GetChild(i).gameObject.activeSelf)
            {
                if (!foodSet.Contains(foodParent.GetChild(i).gameObject))
                {
                    foodSet.Add(foodParent.GetChild(i).gameObject);
                }
            }
        } */
        foodCount=foodArray.Length;
        foodArray = GameObject.FindGameObjectsWithTag("Food_39");
        
        if(foodCount!=foodArray.Length)
        {
            foodSet.Clear();
            for (int i = 0; i < foodArray.Length; i++)
            {
                if (foodArray[i].activeSelf)
                {
                    foodSet.Add(foodArray[i]);
                }
            }
        }
        
        
    }

    private void EatFood()
    {
        if (targetFood != null)
        {
            if (Vector3.Distance(transform.position, targetFood.transform.position) > 0.1)
            {
                MoveToFood(targetFood);
            }
            else
            {
                //targetFood.gameObject.SetActive(false);
                foodSet.Remove(targetFood);
                targetFood = null;
                if (foodSet.Count >= 2)
                {
                    var randomIndex1 = Random.Range(0, foodSet.Count);
                    var randomIndex2 = Random.Range(0, foodSet.Count - 1);

                    if (randomIndex2 >= randomIndex1)
                    {
                        randomIndex2++;
                    }

                    var food1 = foodSet[randomIndex1];
                    var food2 = foodSet[randomIndex2];

                    food1.gameObject.SetActive(false);
                    food2.gameObject.SetActive(false);
                    
                    if(getItem.foodList.Contains(food1))
                    {
                        getItem.foodList.Remove(food1);
                    }
                    if (getItem.foodList.Contains(food2))
                    {
                        getItem.foodList.Remove(food2);
                    }
                    foodSet.Remove(food1);
                    foodSet.Remove(food2);


                    CDTimer = 0;
                }
                else if (foodSet.Count == 1)
                {
                    foodSet[0].gameObject.SetActive(false);
                    foodSet.RemoveAt(0);

                    CDTimer = 0;
                }
                
            }
        }
        else
        {
            var a = Random.Range(0, foodSet.Count - 1);
            
            targetFood = foodSet[a];
        }
    }

    private void MoveToFood(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
            Time.deltaTime * parameter.moveSpeed);
    }
}
