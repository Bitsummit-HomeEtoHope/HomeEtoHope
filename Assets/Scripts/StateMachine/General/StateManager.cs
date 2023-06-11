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
		private void Start()
		{
			StartCoroutine(GetHungry());
		}

		private IEnumerator GetHungry()
		{
			yield return new WaitForSeconds(timeToBeHungry);
			manager.TransitState(StateType.Hungry);
			a++;
		}
	}
}
