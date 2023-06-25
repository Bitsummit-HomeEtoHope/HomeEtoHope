using StateMachine.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITouchYou : MonoBehaviour
{
    private Parameter parameter;
    private StateManager otherStateManager;
    private bool youAreDie = false;
    public FSM manager;

    private void Start()
    {
        manager = GetComponent<FSM>();
        parameter = GameObject.FindObjectOfType<FSM>().parameter;
    }

    private void Update()
    {
        if (youAreDie && parameter.cleanTargetList != null && parameter.cleanTargetList.Length > 0)
        {
            Transform target = parameter.cleanTargetList[0]; // 获取第一个目标

            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < 2f)
            {
                Debug.Log("--------I get you--------");
                otherStateManager.isDie = true;

                manager.TransitState(StateType.Patrolling);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("-------who are you-------");

        GameObject otherCharacter = other.gameObject;
        otherStateManager = otherCharacter.GetComponent<StateManager>();

        if (otherStateManager != null)
        {
            Debug.Log("StateManager: " + otherStateManager);
            youAreDie = true;
        }
    }
}
