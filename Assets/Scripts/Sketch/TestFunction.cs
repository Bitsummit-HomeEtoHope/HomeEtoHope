using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestFunction : MonoBehaviour
{
	public GameObject food;
	public Parameter parameter;
	public FSM manager;

	private void OnEnable()
	{
		EventManager.TransportFoodEvent += manager.TransitState;
	}

	private void OnDisable()
	{
		EventManager.TransportFoodEvent -= manager.TransitState;
	}

	public void GenerateFood()
	{
		var go = Instantiate(food).transform;
		var generatePos = parameter.patrolPoints[Random.Range(0, parameter.patrolPoints.Length)].position;
		go.transform.position = generatePos;
		EventManager.TransportFoodEvent.Invoke(StateType.Hungry);
	}
}
