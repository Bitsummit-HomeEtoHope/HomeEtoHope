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
			parameter.currentTarget = parameter.patrolPoints[2].position;
			parameter.isHungry = true;
			Debug.Log("I'm hungry!!!");
		}

		public void OnUpdate()
		{
			manager.transform.position = Vector3.MoveTowards(manager.transform.position, 
                parameter.currentTarget, parameter.hungrySpeed*Time.deltaTime);
		}

		public void OnExit()
		{
			parameter.isHungry = false;
		}
	}
}
