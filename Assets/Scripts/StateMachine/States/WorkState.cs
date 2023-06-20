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

        public WorkState(FSM manager)
        {
            this.manager = manager;
            parameter = manager.parameter;
        }
    
        public void Onenter()
        {
            
        }

        public void OnExit()
        {
            
        }

        public void OnUpdate()
        {
            
        }
    }
}

