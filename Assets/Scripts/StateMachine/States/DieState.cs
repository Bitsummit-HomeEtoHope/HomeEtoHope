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
            // ���Լ���λ������Ԥ�Ƽ�
            GameObject newPrefab = GameObject.Instantiate(parameter.flower, manager.transform.position, manager.transform.rotation);

            // ɾ����ǰ��Ϸ����
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
