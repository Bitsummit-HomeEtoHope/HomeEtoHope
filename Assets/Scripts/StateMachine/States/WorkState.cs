using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;
using System.Linq;
using Unity.VisualScripting;

namespace StateMachine.States
{
    public class WorkState : IState
    {

        private float rotateBuild = 0f;
        private float scaleBuild = 1f;

        private readonly Parameter parameter;
        private readonly FSM manager;
        private bool isArriveWorkPoint;
        private GameObject tool;
        private GameObject[] buildPoints;
        private int random;

        //------
        private bool isLate = false;
        //------
        public WorkState(FSM manager)
        {
            this.manager = manager;
            parameter = manager.parameter;
        }

            //SetEnergyActive(manager.transform, " CHANGE HERE ");
        public void SetEnergyActive(Transform selfTransform, string componentName)
        {
            string[] allowedComponentNames = { "energy", "energy_build", "energy_factory", "energy_farm"  };//, "human-body"

            // human effect Off
            foreach (Transform child in selfTransform)
            {
                if (allowedComponentNames.Contains(child.name))
                {
                    child.gameObject.SetActive(false);
                }
            }

            // human effect On  (choose effect in "energy(eat food __ no tool),energy_build,energy_factor,energy_farm")
            if (allowedComponentNames.Contains(componentName))
            {
                Transform componentTransform = selfTransform.Find(componentName);
                if (componentTransform != null)
                {
                    componentTransform.gameObject.SetActive(true);
                }
            }
        }




        //count down for the Hungry
        //private IEnumerator HungryLate(float delay)
        //{
        //    yield return new WaitForSeconds(delay);
        //    if (!isLate) manager.TransitState(StateType.Hungry);
        //}





        public void Onenter()
        {
            //isLate = false;
            //manager.StartCoroutine(HungryLate(parameter.levelDataCurrent._latetime));

            //if get [food] -- if get [tool] -- select the [build] and change [effect]
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

            //the food well human take is the [Game Object] named "Food_Tran"
            parameter.Food_Tran.gameObject.SetActive(false);
            isArriveWorkPoint = false;
            GetWorkTarget();
            manager.parameter.isWork = true;
        }

        public void OnExit()
        {

            //manager.StopCoroutine(HungryLate(parameter.levelDataCurrent._latetime));

            //Clear the List [parameter.workPoints]

            parameter.workPoints.Clear();

        }

        public void OnUpdate()
        {
            if (isArriveWorkPoint == false)
            {
                isLate = true;

                manager.transform.position = Vector3.MoveTowards(manager.transform.position, parameter.currentTarget, parameter.moveSpeed * Time.deltaTime);
                
            }
            if (Vector2.Distance(manager.transform.position, parameter.currentTarget) < 0.1f && manager.parameter.isWork == true)
            {
                if (parameter.workPoints.Count > 0 && random < parameter.workPoints.Count)
                {
                    parameter.workPoints[random].gameObject.GetComponent<Check_HumanDistance>().isBuild = true;
                }
                else
                {
                    Debug.LogError("[WorkState] the OnUpdate 114-130 bug .");
                }

                GameObject[] AgricultureResources = Resources.LoadAll<GameObject>("2D_set/build/Agriculture/");
                GameObject[] IndustryResources = Resources.LoadAll<GameObject>("2D_set/build/Industry/");
                GameObject[] SocietyResources = Resources.LoadAll<GameObject>("2D_set/build/Society/");
                GameObject randomAgricultureResource = AgricultureResources[Random.Range(0, AgricultureResources.Length)];
                GameObject randomIndustryResource = IndustryResources[Random.Range(0, IndustryResources.Length)];
                GameObject randomSocietyResource = SocietyResources[Random.Range(0, SocietyResources.Length)];
                Debug.Log("tool:" + manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName);

                GameObject parentObject = parameter.workPoints[random].gameObject; // creat under the target


                //choose the build prefab
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Agriculture")
                {
                    GameObject tool1 = GameObject.Instantiate(randomAgricultureResource, parentObject.transform);
                    tool1.transform.localPosition = Vector3.zero;
                    tool1.transform.localRotation = Quaternion.Euler(rotateBuild, 0f, 0f);
                    tool1.transform.localScale = new Vector3(scaleBuild, scaleBuild, scaleBuild);
                }
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Industry")
                {
                    GameObject tool1 = GameObject.Instantiate(randomIndustryResource, parentObject.transform);
                    tool1.transform.localPosition = Vector3.zero;
                    tool1.transform.localRotation = Quaternion.Euler(rotateBuild, 0f, 0f);
                    tool1.transform.localScale = new Vector3(scaleBuild, scaleBuild, scaleBuild);
                }
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName == "Society")
                {
                    GameObject tool1 = GameObject.Instantiate(randomSocietyResource, parentObject.transform);
                    tool1.transform.localPosition = Vector3.zero;
                    tool1.transform.localRotation = Quaternion.Euler(rotateBuild, 0f, 0f);
                    tool1.transform.localScale = new Vector3(scaleBuild, scaleBuild, scaleBuild);
                }

                isArriveWorkPoint = true;
                manager.parameter.isHungry = true;
                SetEnergyActive(manager.transform, "xxx");
                manager.TransitState(StateType.Idle);
                //manager.TransitState(StateType.Hungry);
            }

            if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum == 0)
            {
                manager.parameter.isTool = false;
                manager.parameter.isWork = false;
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._isBad)
                {
                    manager.parameter.isBroken = true;
                }

                manager.TransitState(StateType.Patrolling);
            }
            if (isArriveWorkPoint && (Vector2.Distance(manager.transform.position, parameter.currentTarget) < 1f))
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
            parameter.workPoints.Clear(); // clear the workpoints befor get

            buildPoints = GameObject.FindGameObjectsWithTag("Build_set");

            foreach (GameObject buildPoint in buildPoints)
            {
                if (buildPoint.GetComponent<Check_HumanDistance>().isBuild == false)
                {
                    parameter.workPoints.Add(buildPoint.transform);
                }
            }

            if (parameter.workPoints.Count > 0)
            {
                int randomIndex = Random.Range(0, parameter.workPoints.Count); 
                parameter.currentTarget = parameter.workPoints[randomIndex].position; 
                random = randomIndex;

                // change Tag from "build_set" to "Will_build"
                parameter.workPoints[randomIndex].gameObject.tag = "Will_build";
            }
            else
            {
                //if can not find work change to ,,,,
            }
        }
    }

}

