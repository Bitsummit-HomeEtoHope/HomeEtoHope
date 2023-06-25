using StateMachine.General;
using System.Collections.Generic;
using UnityEngine;

public class CleanState : IState
{
    private readonly Parameter parameter;
    private readonly FSM manager;

    private float moveSpeed = 70f;
    public bool isComming = false;
    public bool listOk = false;
    private GameObject tool;
    private GetItem_Human getItem;

    public CleanState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.parameter;
        getItem = manager.gameObject.GetComponent<GetItem_Human>(); // 获取 GetItem_Human 脚本组件
    }

    public void Onenter()
    {
        parameter.CleanWanter.SetActive(true);
        Debug.Log("------I am coming!------");
        manager.gameObject.GetComponent<StateManager>().enabled = false;
        GetCleanTarget(); // 调用 GetCleanTarget() 方法以更新 CleanTargetList
    }

    public void OnExit()
    {
      //  manager.parameter.isHungry = true;
      //  manager.parameter.isClean = false;
      //  getItem.isFood = false;       
        getItem.toolList_human.Clear();

        ITouchYou touchScript = manager.gameObject.GetComponent<ITouchYou>();
        if (touchScript != null)
        {
            touchScript.enabled = false;
      //     parameter.CleanWanter.SetActive(false);
        }
        manager.gameObject.GetComponent<StateManager>().enabled = true;

        // 清空 CleanTargetList 列表
        parameter.cleanTargetList = new Transform[0];
    }

    public void OnUpdate()
    {
        if (listOk)
        {
            if (isComming && parameter.cleanTargetList != null && parameter.cleanTargetList.Length > 0)
            {
                Transform target = parameter.cleanTargetList[0];

                float distance = Vector3.Distance(manager.transform.position, target.position);

                if (distance < 0.5f)
                {
                    // 启用 ITouchYou 脚本
                    ITouchYou touchScript = manager.gameObject.GetComponent<ITouchYou>();
                    if (touchScript != null)
                    {
                        touchScript.enabled = true;
                    }
                }

                manager.transform.position = Vector3.MoveTowards(manager.transform.position, target.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void GetCleanTarget()
    {
        // Find GameObjects with the "Player" tag in the scene and store their transforms in CleanTargetList
        GameObject[] humanCharacters = GameObject.FindGameObjectsWithTag("Player");
        List<Transform> CleanPointsList = new List<Transform>();

        foreach (GameObject character in humanCharacters)
        {
            if (character != manager.gameObject)
            {
                CleanPointsList.Add(character.transform);
            }
        }

        if (CleanPointsList.Count > 0)
        {
            // Shuffle the list using Fisher-Yates algorithm
            System.Random random = new System.Random();
            int n = CleanPointsList.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                Transform temp = CleanPointsList[k];
                CleanPointsList[k] = CleanPointsList[n];
                CleanPointsList[n] = temp;
            }

            parameter.cleanTargetList = CleanPointsList.ToArray();

            // Set isComming to true after finding the target
            isComming = true;
        }
        listOk = true;
    }
}
