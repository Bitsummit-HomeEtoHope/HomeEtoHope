using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using UnityEngine;

//Another state like PatrolState
public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.parameter;
    }
    public void Onenter()
    {
        Debug.Log("进入idle");
    }

    public void OnUpdate()
    {
        parameter.idleTimer += Time.deltaTime;
        if (parameter.idleTimer >= parameter.idleTime&&parameter.isWork==false)
        {
            manager.TransitState(StateType.Patrolling);
        }
        if (parameter.idleTimer >= parameter.workTimer&&parameter.isWork==true)
        {
            
            //manager.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            manager.TransitState(StateType.Working);
        }
        if(parameter.idleTimer < parameter.workTimer&&parameter.isWork==true)
        {
            parameter.BuildAnim.gameObject.SetActive(true);
        }
    }

    public void OnExit()
    {
        parameter.idleTimer = 0;
    }
}
