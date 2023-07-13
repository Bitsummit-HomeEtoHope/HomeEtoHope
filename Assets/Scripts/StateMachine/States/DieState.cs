using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

namespace StateMachine.States
{
    public class DieState : IState
    {
        private readonly Parameter parameter;
        private readonly FSM manager;
        private GameObject tool;

        public DieState(FSM manager)
        {
            this.manager = manager;
            parameter = manager.parameter;
        }

        public void Onenter()
        {
            Debug.Log("ddddddddddddddddddddddddddddddd");

            GameObject gardenObject = GameObject.Find("-----Garden-----");
            if (gardenObject != null)
            {
                GameObject newPrefab = GameObject.Instantiate(parameter.flower, manager.transform.position, manager.transform.rotation);
                newPrefab.transform.SetParent(gardenObject.transform, worldPositionStays: true);
            }

            GameObject.Destroy(manager.gameObject);
        }

        public void OnExit()
        {
        }

        public void OnUpdate()
        {
        }
    }
}
