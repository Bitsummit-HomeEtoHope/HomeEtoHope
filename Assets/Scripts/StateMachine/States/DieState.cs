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
        private bool isArriveWorkPoint;
        private GameObject tool;

        public DieState(FSM manager)
        {
            this.manager = manager;
            parameter = manager.parameter;
        }

        public void Onenter()
        {
            Debug.Log("------------i am dying-----------");
            manager.parameter.isDie = true;
            // ���������ͼ���� did
            manager.gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(parameter.did, new Rect(0, 0, parameter.did.width, parameter.did.height), Vector2.one * 0.5f);
            manager.gameObject.GetComponent<StateManager>().enabled = false;

            // ����ǩ����Ϊ"Flower"
            manager.gameObject.tag = "Flower";
        }

        public void OnExit()
        {
        }

        public void OnUpdate()
        {
        }
    }
}
