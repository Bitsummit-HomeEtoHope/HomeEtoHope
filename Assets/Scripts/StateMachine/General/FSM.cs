using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Parameter
{
	//It is a class that lists the parameter you need, I recommend you declare variables here!
	public Transform[] patrolPoints;
	public Vector3 currentTarget;
	public float idleTime;
	public float idleTimer;
	public float patrolRadius;
	public float moveSpeed;
}

public enum StateType
{
	//I wrote a few state in advance，if you need create a new state don't forget to write it here!
	Idle,
	Eating,
	Working,
	GettingSick,
	Patrolling,
}

public class FSM : MonoBehaviour
{
	public Parameter parameter = new Parameter();
	
	private IState currentState;
	private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

	private void Start()
	{	//	Initialize some parameters
		parameter.idleTimer = 0;
		parameter.currentTarget = transform.position;

		//	Register the state
		//	you should register your new state here，like：
		//	states.Add(StateType.XX, new XXState(this));
		states.Add(StateType.Idle, new IdleState(this));
		states.Add(StateType.Patrolling, new PatrolState(this));
		
		//	Default State is set to be "Idle"
		TransitState(StateType.Idle);
	}
	
	/// <summary>
	/// Switch state method
	/// </summary>
	/// <param name="type"> It accepts a variable of StateType </param>
	/// !! PLEASE DON'T MODIFY !!
	public void TransitState(StateType type)
	{
		if (currentState != null)
			currentState.OnExit();
		currentState = states[type];
		currentState.Onenter();
	}

	//Where the method "XXState.OnUpdate()" run per frame;
	private void Update()
	{
		currentState.OnUpdate();
	}
}
