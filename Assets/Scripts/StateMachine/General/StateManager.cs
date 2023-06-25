using System;
using System.Collections;
using UnityEngine;

namespace StateMachine.General
{
    public class StateManager : MonoBehaviour
    {
        public float timeToBeHungry;
        public FSM manager;
        private int a = 0;

        public bool isDie = false;
        public bool isClear = false;

        private void Start()
        {
            manager = GetComponent<FSM>();

            //manager.parameter.isRed = false;
            //manager.parameter.isDie = false;

            StartCoroutine(GetHungry());
            StartCoroutine(StartClearingTimer());
        }

        private void Update()
        {
            if (isDie)
            {
                manager.TransitState(StateType.Dying);
            }
            if (isClear)
            {
                manager.TransitState(StateType.Cleaning);
            }
        }

        private IEnumerator GetHungry()
        {
            yield return new WaitForSeconds(timeToBeHungry);
            manager.TransitState(StateType.Hungry);
            a++;
        }

        private IEnumerator StartClearingTimer()
        {
            yield return new WaitForSeconds(20f);
            manager.TransitState(StateType.Cleaning);
        }
    }
}
