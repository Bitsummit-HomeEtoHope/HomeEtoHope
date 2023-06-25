using System.Collections;
using System.Collections.Generic;
using StateMachine.States;
using UnityEngine;

namespace StateMachine.General
{
	public enum StateType
	{
		//I wrote a few state in advance，if you need create a new state don't forget to write it here!
		Idle, Eating, Working, GettingSick, Patrolling, Hungry,Cleaning,Dying,
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
			states.Add(StateType.Hungry, new HungryState(this));
			states.Add(StateType.Working, new WorkState(this));
			states.Add(StateType.Cleaning, new CleanState(this));
			states.Add(StateType.Dying, new DieState(this));
		
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

		public void StartWaitForSeconds(float time)
		{
			StartCoroutine(WaitForSecondsCoroutine(time));
		}

		private IEnumerator WaitForSecondsCoroutine(float time)
		{
			yield return new WaitForSeconds(time);
			// 执行暂停后的逻辑
			//Time.timeScale = 1;
		}
		
	}
}