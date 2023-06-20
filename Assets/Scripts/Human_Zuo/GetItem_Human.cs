using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

namespace StateMachine.General
{
    public class GetItem_Human : MonoBehaviour
    {
        public GameObject GetItemManger;
        public List<GameObject> foodList_human = new List<GameObject>();
        public List<GameObject> toolList_human = new List<GameObject>();
        
        public bool isFood = false;
        public bool isTool = false;
        public FSM manager;

        private void Awake() 
        {
            manager = GetComponent<FSM>();
        }
        private void Update()
        {
            GetFood();
        }
        public void GetFood()
        {
            //计算当前位置与食物的距离
            float distance = Vector3.Distance(transform.position, manager.parameter.patrolPoints[2].position);
            if(isFood==false && distance<1 && GetItemManger.GetComponent<GetItem>().foodList.Count!=0)
            {
                isFood = true;
                GameObject foodToGet=GetItemManger.GetComponent<GetItem>().foodList[0];
                foodList_human.Add(foodToGet);
                foodToGet.SetActive(false);
                GetItemManger.GetComponent<GetItem>().foodList.RemoveAt(0);//移除食物
                
            }
        }


    }
}

