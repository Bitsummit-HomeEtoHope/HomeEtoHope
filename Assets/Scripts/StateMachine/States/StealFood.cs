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

    private void Awake()
    {
        targetPos = new Vector3(0, 0, -203.296f);
        foodParent = GameObject.Find("-----Food_Bag-----").transform;
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
        for (int i = 0; i < foodParent.childCount; i++)
        {
            if (foodParent.GetChild(i).CompareTag("Food") && foodParent.GetChild(i).gameObject.activeSelf)
            {
                if (!foodSet.Contains(foodParent.GetChild(i).gameObject))
                {
                    foodSet.Add(foodParent.GetChild(i).gameObject);
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
                targetFood.gameObject.SetActive(false);
                foodSet.Remove(targetFood);
                targetFood = null;
                CDTimer = 0;
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
