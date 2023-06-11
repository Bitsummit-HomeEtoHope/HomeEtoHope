using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryState : IState
{
    private Parameter parameter;
    private FSM manager;

    public HungryState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.parameter;
    }

    public void Onenter()
    {
        Debug.Log("I'm hungry!");
    }

    public void OnUpdate()
    {
        
    }

    public void OnExit()
    {
        
    }
}
