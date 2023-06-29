using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using UnityEngine;

//Another state like PatrolState
public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;
    private bool isAnim=false;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.parameter;
    }
    public void Onenter()
    {
        //parameter.Food_Tran.gameObject.SetActive(false);
        parameter.Build_FinsihAnim.gameObject.SetActive(false);
        isAnim=false;
        Debug.Log("进入idle");
    }

    public void OnUpdate()
    {
        parameter.idleTimer += Time.deltaTime;
        if (parameter.idleTimer >= parameter.idleTime&&parameter.isWork==false&&manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._isBad==false)
        {
            manager.TransitState(StateType.Patrolling);
        }
        if (parameter.idleTimer >= parameter.idleTime*5&&parameter.isWork==false&&manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._isBad==true)
        {
            manager.TransitState(StateType.Patrolling);
        }
        
        
        if (parameter.idleTimer >= parameter.workTimer&&parameter.isWork==true&&manager.parameter.isHungry==false)
        {
            
            parameter.BuildAnim.gameObject.GetComponent<SpriteRenderer>().enabled=false;
            manager.TransitState(StateType.Working);
        }
        if(parameter.idleTimer < parameter.workTimer&&parameter.isWork==true&&isAnim==false)
        {
            parameter.BuildAnim.gameObject.GetComponent<SpriteRenderer>().enabled=true;
            parameter.BuildAnim.gameObject.SetActive(true);
            isAnim=true;
            
        }
        if(parameter.idleTimer >= parameter.workTimer&&parameter.isWork==true&&manager.parameter.isHungry==true)
        {
            manager.gameObject.GetComponent<GetItem_Human>().isFood=false;
            manager.gameObject.GetComponent<GetItem_Human>().foodList_human.RemoveAt(0);
            manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum--;
            manager.TransitState(StateType.Hungry);
        }
        
        
    }

    public void OnExit()
    {
        
        parameter.idleTimer = 0;
    }
}
