using System.Collections;
using System.Collections.Generic;
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
        
    }

    public void OnUpdate()
    {
        parameter.idleTimer += Time.deltaTime;
        if (parameter.idleTimer >= parameter.idleTime)
        {
            manager.TransitState(StateType.Patrolling);
        }
    }

    public void OnExit()
    {
        parameter.idleTimer = 0;
    }
}
