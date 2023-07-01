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
				parameter.Food_Tran.gameObject.SetActive(true);
				manager.TransitState(StateType.Idle);
			}
		}

        public void OnExit()
        {
            parameter.isHungry = false;

            GameObject foodObject = manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject;
            GetItem2dData foodData = foodObject.GetComponent<GetItem2dData>();
            GameObject itemTreeSprite = foodData._itemTreeSprite;
            string itemName = foodData._itemName;

            GameObject treeObject = manager.gameObject.GetComponent<GetItem_Human>().TreeList[0];

            if (itemName == "Apple")
            {
                GameObject apple = GameObject.Instantiate(itemTreeSprite, Vector3.zero, Quaternion.identity);
                apple.transform.SetParent(treeObject.transform);
                apple.transform.localPosition = Vector3.zero; // 设置位置为父物体的坐标位置
                Vector3 scale = apple.transform.localScale;
                scale.x = 1f; // 将 X 缩放值设置为 1
                apple.transform.localScale = scale; // 更新缩放值
            }
            else if (itemName == "Eggplant")
            {
                GameObject eggplant = GameObject.Instantiate(itemTreeSprite, Vector3.zero, Quaternion.identity);
                eggplant.transform.SetParent(treeObject.transform);
                eggplant.transform.localPosition = Vector3.zero; // 设置位置为父物体的坐标位置
                Vector3 scale = eggplant.transform.localScale;
                scale.x = 1f; // 将 X 缩放值设置为 1
                eggplant.transform.localScale = scale; // 更新缩放值
            }
            else if (itemName == "Greenpepper")
            {
                GameObject greenpepper = GameObject.Instantiate(itemTreeSprite, Vector3.zero, Quaternion.identity);
                greenpepper.transform.SetParent(treeObject.transform);
                greenpepper.transform.localPosition = Vector3.zero; // 设置位置为父物体的坐标位置
                Vector3 scale = greenpepper.transform.localScale;
                scale.x = 1f; // 将 X 缩放值设置为 1
                greenpepper.transform.localScale = scale; // 更新缩放值
            }
            else if (itemName == "Orange")
            {
                GameObject orange = GameObject.Instantiate(itemTreeSprite, Vector3.zero, Quaternion.identity);
                orange.transform.SetParent(treeObject.transform);
                orange.transform.localPosition = Vector3.zero; // 设置位置为父物体的坐标位置
                Vector3 scale = orange.transform.localScale;
                scale.x = 1f; // 将 X 缩放值设置为 1
                orange.transform.localScale = scale; // 更新缩放值
            }
            else if (itemName == "Pumpkin")
            {
                GameObject pumpkin = GameObject.Instantiate(itemTreeSprite, Vector3.zero, Quaternion.identity);
                pumpkin.transform.SetParent(treeObject.transform);
                pumpkin.transform.localPosition = Vector3.zero; // 设置位置为父物体的坐标位置
                Vector3 scale = pumpkin.transform.localScale;
                scale.x = 1f; // 将 X 缩放值设置为 1
                pumpkin.transform.localScale = scale; // 更新缩放值
            }
        }

        private void SetObjectScaleX(GameObject obj, float scaleX)
        {
            Vector3 originalScale = obj.transform.localScale;
            obj.transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
        }
    }
}
