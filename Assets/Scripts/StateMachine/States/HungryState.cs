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
			parameter.isWork = false;
			parameter.isHungry = true;
			Debug.Log("I'm hungry!!!");
		}

		public void OnUpdate()
		{
			manager.transform.position = Vector3.MoveTowards(manager.transform.position, 
                parameter.currentTarget, parameter.hungrySpeed*Time.deltaTime);
			if(manager.parameter.isHungry==false)
			{
				manager.TransitState(StateType.Idle);
			}
		}

		public void OnExit()
		{
			parameter.isHungry = false;
			if(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName=="Apple")
			{
				GameObject apple=GameObject.Instantiate(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemTreeSprite,manager.gameObject.GetComponent<GetItem_Human>().TreeList[0].transform.position,Quaternion.identity);
				//Remove掉TreeList[0]
				manager.gameObject.GetComponent<GetItem_Human>().TreeList.RemoveAt(0);
			}
			if(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName=="Eggplant")
			{
				GameObject eggplant=GameObject.Instantiate(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemTreeSprite,manager.gameObject.GetComponent<GetItem_Human>().TreeList[0].transform.position,Quaternion.identity);
				manager.gameObject.GetComponent<GetItem_Human>().TreeList.RemoveAt(0);
			}
			if(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName=="Greenpepper")
			{
				GameObject greenpepper=GameObject.Instantiate(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemTreeSprite,manager.gameObject.GetComponent<GetItem_Human>().TreeList[0].transform.position,Quaternion.identity);
				manager.gameObject.GetComponent<GetItem_Human>().TreeList.RemoveAt(0);
			}
			if(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName=="Orange")
			{
				GameObject orange=GameObject.Instantiate(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemTreeSprite,manager.gameObject.GetComponent<GetItem_Human>().TreeList[0].transform.position,Quaternion.identity);
				manager.gameObject.GetComponent<GetItem_Human>().TreeList.RemoveAt(0);
			}
			if(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemName=="Pumpkin")
			{
				GameObject pumpkin=GameObject.Instantiate(manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject.GetComponent<GetItem2dData>()._itemTreeSprite,manager.gameObject.GetComponent<GetItem_Human>().TreeList[0].transform.position,Quaternion.identity);
				manager.gameObject.GetComponent<GetItem_Human>().TreeList.RemoveAt(0);
			}


		}
	}
}
