using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

namespace StateMachine.States
{
    public class WorkState : IState
    {
        private readonly Parameter parameter;
        private readonly FSM manager;
        private bool isArriveWorkPoint;
        private GameObject tool;

        public WorkState(FSM manager)
        {
            this.manager = manager;
            parameter = manager.parameter;
        }
    
        public void Onenter()
        {
            isArriveWorkPoint = false;
            GetWorkTarget();
            manager.parameter.isWork = true;
        }

        public void OnExit()
        {
            
        }

        public void OnUpdate()
        {
            if(isArriveWorkPoint==false)
            {
                manager.transform.position = Vector3.MoveTowards(manager.transform.position, 
                parameter.currentTarget, parameter.moveSpeed*Time.deltaTime);
            }
            if(Vector2.Distance(manager.transform.position, parameter.currentTarget) < 0.1f&&manager.parameter.isWork==true)
            {
                manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum--;
                tool=Resources.Load<GameObject>("2D_set/build/Agriculture/farm1");
                //Debug.Log("tool:"+tool);
                GameObject tool1=GameObject.Instantiate(tool,manager.transform.position,Quaternion.identity);
                isArriveWorkPoint = true;
                
            }
    
            if(manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum==0)
            {
                manager.parameter.isTool = false;
                manager.parameter.isWork = false;
                manager.TransitState(StateType.Patrolling);
            }
            if(isArriveWorkPoint&&(Vector2.Distance(manager.transform.position, parameter.currentTarget) < 1f))
            {
                manager.TransitState(StateType.Working);
            }
            if(manager.parameter.isHungry==true)
            {
                manager.TransitState(StateType.Hungry);
            }
          
        }

        public void GetWorkTarget()
        {
            List<Transform> workPointsList = new List<Transform>(parameter.workPoints);//将数组转换为List
            Debug.Log("数组长度workPointsList:"+workPointsList.Count);
            int randomIndex = Random.Range(0, workPointsList.Count+1);//随机获取一个索引
            parameter.currentTarget = workPointsList[randomIndex].position;//获取随机索引对应的元素
            workPointsList.RemoveAt(randomIndex);//移除随机索引对应的元素
            parameter.workPoints = workPointsList.ToArray();//将List转换为数组
            

        }
    }
}

