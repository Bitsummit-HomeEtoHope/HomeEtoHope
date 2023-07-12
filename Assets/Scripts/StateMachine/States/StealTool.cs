using StateMachine.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealTool : MonoBehaviour
{
    public Transform toolParent;
    public List<GameObject> toolSet = new List<GameObject>();
    public Parameter parameter;
    public Vector3 targetPos;
    public GameObject targetTool;
    public float CDTime;
    public float CDTimer;

    private void Awake()
    {
        targetPos = new Vector3(0, 0, -203.296f);
        toolParent = GameObject.Find("-----Tool_Bag-----").transform;
    }
    private void Update()
    {
        GetChildren();
        CDTimer += Time.deltaTime;

        if (toolSet.Count > 0 && CDTimer > CDTime)
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
        for (int i = 0; i < toolParent.childCount; i++)
        {
            if (toolParent.GetChild(i).CompareTag("Tool") && toolParent.GetChild(i).gameObject.activeSelf)
            {
                if (!toolSet.Contains(toolParent.GetChild(i).gameObject))
                {
                    toolSet.Add(toolParent.GetChild(i).gameObject);
                }
            }
        }
    }

    private void EatFood()
    {
        if (targetTool != null)
        {
            if (Vector3.Distance(transform.position, targetTool.transform.position) > 0.1)
            {
                MoveToFood(targetTool);
            }
            else
            {
                targetTool.gameObject.SetActive(false);
                toolSet.Remove(targetTool);
                targetTool = null;
                CDTimer = 0;
            }
        }
        else
        {
            var a = Random.Range(0, toolSet.Count - 1);
            targetTool = toolSet[a];
        }
    }

    private void MoveToFood(GameObject target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
            Time.deltaTime * parameter.moveSpeed);
    }
}
