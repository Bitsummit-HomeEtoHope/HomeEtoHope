using StateMachine.General;
using UnityEngine;

namespace StateMachine.States
{
	public class HungryState : IState
	{
		private readonly Parameter parameter;
		private readonly FSM manager;

		public HungryState(FSM manager)
		{
			this.manager = manager;
			parameter = manager.parameter;
		}
		public void Onenter()
		{
			parameter.isHungry = true;
			Debug.Log("I'm hungry!!!");
		}

		public void OnUpdate()
		{
			
		}

		public void OnExit()
		{
			parameter.isHungry = false;
		}
	}
}
