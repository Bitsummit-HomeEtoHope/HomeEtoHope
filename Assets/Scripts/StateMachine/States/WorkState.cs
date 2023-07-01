using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;
using System.Linq;

namespace StateMachine.States
{
    public class WorkState :IState
    {

        private float rotateBuild = -60f;
        private float scaleBuild = 20f;

        private readonly Parameter parameter;
        private readonly FSM manager;
        private bool isArriveWorkPoint;
        private GameObject tool;
        private GameObject[] buildPoints;
        private int random;


        public WorkState(FSM manager)
        {
            this.manager = manager;
            parameter = manager.parameter;
        }

        public void SetEnergyActive(Transform selfTransform, string componentName)
        {
            string[] allowedComponentNames = { "energy", "energy_build", "energy_factory", "energy_farm" };

            // 关闭所有组件
            foreach (Transform child in selfTransform)
            {
                if (allowedComponentNames.Contains(child.name))
                {
                    child.gameObject.SetActive(false);
                }
            }

            // 开启指定的组件
            if (allowedComponentNames.Contains(componentName))
            {
                Transform componentTransform = selfTransform.Find(componentName);
                if (componentTransform != null)
                {
                    componentTransform.gameObject.SetActive(true);
                }
            }
        }

            public void Onenter()
        {

            if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Agriculture")
            {
                //--- "energy", "energy_build", "energy_factory", "energy_farm" };
                SetEnergyActive(manager.transform, "energy_farm");
                //---      
            }
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Industry")
            {
                //--- "energy", "energy_build", "energy_factory", "energy_farm" };
                SetEnergyActive(manager.transform, "energy_factory");
                //---
            }
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Society")
            {
                //--- "energy", "energy_build", "energy_factory", "energy_farm" };
                SetEnergyActive(manager.transform, "energy_build");
                //---
            }

            parameter.Food_Tran.gameObject.SetActive(false);
            isArriveWorkPoint = false;
            GetWorkTarget();
            manager.parameter.isWork = true;
        }

        public void OnExit()
        {
            //清空parameter.workPoints
            parameter.workPoints.Clear();
            
        }

        public void OnUpdate()
        {
            if(isArriveWorkPoint==false)
            {
                manager.transform.position = Vector3.MoveTowards(manager.transform.position, 
                parameter.currentTarget, parameter.moveSpeed*Time.deltaTime);
                //到达后等待1s

            }
            if(Vector2.Distance(manager.transform.position, parameter.currentTarget) < 0.1f&&manager.parameter.isWork==true)
            {
                parameter.workPoints[random].gameObject.GetComponent<Check_HumanDistance>().isBuild=true;
                //manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum--;
                GameObject[] AgricultureResources=Resources.LoadAll<GameObject>("2D_set/build/Agriculture/");
                GameObject[] IndustryResources=Resources.LoadAll<GameObject>("2D_set/build/Industry/");
                GameObject[] SocietyResources=Resources.LoadAll<GameObject>("2D_set/build/Society/");
                GameObject randomAgricultureResource=AgricultureResources[Random.Range(0,AgricultureResources.Length)];
                GameObject randomIndustryResource=IndustryResources[Random.Range(0,IndustryResources.Length)];
                GameObject randomSocietyResource=SocietyResources[Random.Range(0,SocietyResources.Length)];
                Debug.Log("tool:"+manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName);
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Agriculture")
                {

                    GameObject tool1 = GameObject.Instantiate(randomAgricultureResource, manager.transform.position, Quaternion.Euler(rotateBuild, 0f, 0f));
                    tool1.transform.localScale = new Vector3(scaleBuild, scaleBuild, scaleBuild);
                }
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Industry")
                {

                    GameObject tool1 = GameObject.Instantiate(randomIndustryResource, manager.transform.position, Quaternion.Euler(rotateBuild, 0f, 0f));
                    tool1.transform.localScale = new Vector3(scaleBuild, scaleBuild, scaleBuild);
                }
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Society")
                {

                    GameObject tool1 = GameObject.Instantiate(randomSocietyResource, manager.transform.position, Quaternion.Euler(rotateBuild, 0f, 0f));
                    tool1.transform.localScale = new Vector3(scaleBuild, scaleBuild, scaleBuild);
                }


                //Debug.Log("tool:"+tool);

                isArriveWorkPoint = true;
                manager.parameter.isHungry = true;
                manager.TransitState(StateType.Idle);
                
                
            }
            
            //currentTime+=Time.deltaTime;
            
    
            if(manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum==0)
            {
                manager.parameter.isTool = false;
                manager.parameter.isWork = false;
                manager.TransitState(StateType.Patrolling);
            }
            if(isArriveWorkPoint&&(Vector2.Distance(manager.transform.position, parameter.currentTarget) < 1f))
            {
                //manager.TransitState(StateType.Working);
                
            }
            /*if(manager.parameter.isHungry==true)
            {
                manager.TransitState(StateType.Hungry);
            }*/
            

          
        }

        public void GetWorkTarget()
        {
            buildPoints=GameObject.FindGameObjectsWithTag("Build_set");
            //List<Transform> workPointsList = new List<Transform>(parameter.workPoints);//将数组转换为List
            //int randomIndex = Random.Range(0, workPointsList.Count+1);//随机获取一个索引
            //parameter.currentTarget = workPointsList[randomIndex].position;//获取随机索引对应的元素
            //workPointsList.RemoveAt(randomIndex);//移除随机索引对应的元素
            //parameter.workPoints = workPointsList.ToArray();//将List转换为数组
            foreach(GameObject buildPoint in buildPoints)
            {
                if(buildPoint.GetComponent<Check_HumanDistance>().isBuild==false)
                {
                    parameter.workPoints.Add(buildPoint.transform);
                }
            }
            int randomIndex = Random.Range(0, parameter.workPoints.Count+1);//随机获取一个索引
            parameter.currentTarget = parameter.workPoints[randomIndex].position;//获取随机索引对应的元素
            random=randomIndex;
            

        }

        
        
    
    }
   
}

