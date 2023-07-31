using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StateMachine.General;
using UnityEngine;

//Another state like PatrolState
public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    private Sprite defaultSprite;
    private bool isAnim=false;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.parameter;
    }
    public void Onenter()
    {
        //---------------------------

       // TurnOffEffect();
 

        //
        
        defaultSprite =manager.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite;
        //parameter.Food_Tran.gameObject.SetActive(false);
        parameter.Build_FinsihAnim.gameObject.SetActive(false);
        isAnim=false;
        Debug.Log("==========idle==========");
    }


    public void TurnOffEffect()
    {
        string[] allowedComponentNames = { "energy", "energy_build", "energy_factory", "energy_farm" , "smoke_front 1" };

        // human effect Off
        foreach (Transform child in manager.transform)
        {
            if (allowedComponentNames.Contains(child.name))
            {
                child.gameObject.SetActive(false);
            }
        }       
    }

    public void TurnOffAll()
    {
        string[] allowedComponentNames = { "smoke_front 1", "finish-building" };

        // human effect Off
        foreach (Transform child in manager.transform)
        {
            if (!allowedComponentNames.Contains(child.name))
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void TurnOnAll()
    {
        string[] allowedComponentNames = { "smoke_front 1", "finish-building" };

        // human effect Off
        foreach (Transform child in manager.transform)
        {
            if (!allowedComponentNames.Contains(child.name))
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void TurnOnEffect(Transform selfTransform, string componentName)
    {
        string[] allowedComponentNames = { componentName };

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



    public void OnUpdate()
    {
        parameter.idleTimer += Time.deltaTime;

        if (parameter.idleTimer >= parameter.idleTime && parameter.isWork == false && manager.gameObject.GetComponent<GetItem_Human>().foodList_human.Count > 0 && manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._isBad == false)
        {

            manager.TransitState(StateType.Patrolling);
        }

        if (parameter.isWork == false && manager.gameObject.GetComponent<GetItem_Human>().foodList_human.Count > 0 && manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._isBad == true)
        {
            Transform child = manager.gameObject.transform.GetChild(1);
            child.gameObject.GetComponent<SpriteRenderer>().sprite = parameter.HungryFace;
            if (parameter.idleTimer >= parameter.levelDataCurrent._future_Data.human_BadFood_Time)
            {
                manager.TransitState(StateType.Hungry);
            }

        }

        if (parameter.idleTimer >= parameter.levelDataCurrent._future_Data.build_Time && parameter.isWork == true && manager.parameter.isHungry == false)
        {
            parameter.BuildAnim.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            manager.TransitState(StateType.Working);
        }

        if (parameter.idleTimer < parameter.workTimer && parameter.isWork == true && isAnim == false&&parameter.isNotWorkPoint==false)
        {
            parameter.BuildAnim.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            parameter.BuildAnim.gameObject.SetActive(true);
            isAnim = true;
        }
        if (parameter.idleTimer >= 0 && parameter.isWork == true && manager.parameter.isHungry == true)
        {
            parameter.Tool_Tran.gameObject.SetActive(false);
            TurnOffAll();
        }
        if (parameter.idleTimer >= parameter.levelDataCurrent._future_Data.build_Time && parameter.isWork == true && manager.parameter.isHungry == true)
        {
            TurnOnAll();
            parameter.Tool_Tran.gameObject.SetActive(true);
            var foodList = manager.gameObject.GetComponent<GetItem_Human>().foodList_human;
            if (foodList.Count > 0)
            {
                manager.gameObject.GetComponent<GetItem_Human>().isFood = false;
                foodList.RemoveAt(0);
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human.Count > 0)
                {
                    manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum--;
                }
                manager.TransitState(StateType.Hungry);
            }
        }
    }

    public void OnExit()
    {
        TurnOffEffect();
        parameter.Food_Tran.SetActive(false);

        // manager.TransitState(StateType.Hungry);
        manager.gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite=parameter.defaultSprite;
        parameter.idleTimer = 0;
    }
}
