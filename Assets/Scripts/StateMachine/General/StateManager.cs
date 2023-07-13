using System;
using System.Collections;
using UnityEngine;

namespace StateMachine.General
{
    public class StateManager : MonoBehaviour
    {
        public float timeToBeHungry;
        public FSM manager;
        private int time = 0;

        private void Start()
        {

            manager = GetComponent<FSM>();
            manager.parameter.levelDataCurrent=GameObject.Find("LevelData").GetComponent<LevelDataCurrent>();
            GameObject[] WorkWithTag = GameObject.FindGameObjectsWithTag("Build_set");
            foreach(GameObject work in WorkWithTag)
            {
                if(work.activeSelf)
                {
                    manager.parameter.workPoints.Add(work.transform);
                }
            }
            GameObject[] PartolWithTag = GameObject.FindGameObjectsWithTag("Partol");
            for(int i=0;i<PartolWithTag.Length;i++)
            {
                manager.parameter.patrolPoints[i] = PartolWithTag[i].transform;
            }


            //manager.parameter.isRed = false;
            //manager.parameter.isDie = false;

            StartCoroutine(GetHungry());
            //StartCoroutine(StartClearingTimer());
        }

        private void Update()
        {

        }

        private IEnumerator GetHungry()
        {
            yield return new WaitForSeconds(timeToBeHungry);
            manager.TransitState(StateType.Hungry);
            time++;
        }

        /*private IEnumerator StartClearingTimer()
        {
            yield return new WaitForSeconds(20f);
            manager.TransitState(StateType.Cleaning);
        }*/
    }
}
