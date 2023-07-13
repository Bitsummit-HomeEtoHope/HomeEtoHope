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
            // 在自己的位置生成预制件
            GameObject newPrefab = GameObject.Instantiate(parameter.flower, manager.transform.position, manager.transform.rotation);

            // 删除当前游戏对象
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
