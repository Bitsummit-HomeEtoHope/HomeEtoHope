using StateMachine.General;
using System.Linq;
using UnityEngine;

namespace StateMachine.States
{
	public class TreeState : IState
	{
		private readonly Parameter parameter;
		private readonly FSM manager;
		

		public TreeState(FSM manager)
		{
			this.manager = manager;
			parameter = manager.parameter;
		}
		public void Onenter()
		{
            manager.gameObject.GetComponent<GetItem_Human>().foodList_human.Clear();
            manager.gameObject.GetComponent<GetItem_Human>().isFood = false;
            parameter.Food_Tran.gameObject.SetActive(false);
            OffEnergyActive(manager.transform);

            parameter.currentTarget = parameter.patrolPoints[2].position;
			parameter.isWork = false;
			//parameter.isHungry = true;
			Debug.Log("I'm hungry!!!");
		}

		public void OnUpdate()
		{
			manager.transform.position = Vector3.MoveTowards(manager.transform.position, 
                parameter.currentTarget, parameter.hungrySpeed*Time.deltaTime);
			if(manager.parameter.isHungry==false)
			{
				//parameter.Food_Tran.gameObject.SetActive(true);
				manager.TransitState(StateType.Idle);
			}
		}

        public void OffEnergyActive(Transform selfTransform)
        {
            string[] componentNames = { "energy", "energy_build", "energy_factory", "energy_farm" };

            foreach (string componentName in componentNames)
            {
                Transform componentTransform = selfTransform.Find(componentName);
                if (componentTransform != null)
                {
                    componentTransform.gameObject.SetActive(false);
                }
            }
        }

        public void SetEnergyActive(Transform selfTransform, string componentName)
        {
            string[] allowedComponentNames = { "energy", "energy_build", "energy_factory", "energy_farm" };

            // 关闭所有组件
            foreach (Transform child in selfTransform)
            {
                if (allowedComponentNames.Contains(child.name))
                {
                    child.gameObject.SetActive(false);
                }
            }

            // 开启指定的组件
            if (allowedComponentNames.Contains(componentName))
            {
                Transform componentTransform = selfTransform.Find(componentName);
                if (componentTransform != null)
                {
                    componentTransform.gameObject.SetActive(true);
                }
            }
        }


        public void OnExit()
        {
            //parameter.isHungry = false;

            GameObject foodObject = manager.gameObject.GetComponent<GetItem_Human>().foodList_human[0].gameObject;
            GetItem2dData foodData = foodObject.GetComponent<GetItem2dData>();
            GameObject itemTreeSprite = foodData._itemTreeSprite;
            string itemName = foodData._itemName;

            GameObject treeObject = manager.gameObject.GetComponent<GetItem_Human>().TreeList[0];

            if (itemName == "Apple")
            {
                GameObject apple = GameObject.Instantiate(itemTreeSprite, treeObject.transform);
                apple.transform.localPosition = Vector3.zero; // 设置位置为父物体的坐标位置
                apple.transform.rotation = treeObject.transform.rotation; // 应用父物体的旋转
                apple.transform.localScale = Vector3.one; // 应用父物体的缩放
            }
            else if (itemName == "Eggplant")
            {
                GameObject eggplant = GameObject.Instantiate(itemTreeSprite, treeObject.transform);
                eggplant.transform.localPosition = Vector3.zero;  
                eggplant.transform.rotation = treeObject.transform.rotation;  
                eggplant.transform.localScale = Vector3.one; 
            }
            else if (itemName == "Greenpepper")
            {
                GameObject greenpepper = GameObject.Instantiate(itemTreeSprite, treeObject.transform);
                greenpepper.transform.localPosition = Vector3.zero; 
                greenpepper.transform.rotation = treeObject.transform.rotation;  
                greenpepper.transform.localScale = Vector3.one;  
            }
            else if (itemName == "Orange")
            {
                GameObject orange = GameObject.Instantiate(itemTreeSprite, treeObject.transform);
                orange.transform.localPosition = Vector3.zero;  
                orange.transform.rotation = treeObject.transform.rotation;  
                orange.transform.localScale = Vector3.one;  
            }
            else if (itemName == "Pumpkin")
            {
                GameObject pumpkin = GameObject.Instantiate(itemTreeSprite, treeObject.transform);
                pumpkin.transform.localPosition = Vector3.zero;  
                pumpkin.transform.rotation = treeObject.transform.rotation; 
                pumpkin.transform.localScale = Vector3.one; 
            }

        }

        private void SetObjectScaleX(GameObject obj, float scaleX)
        {
            Vector3 originalScale = obj.transform.localScale;
            obj.transform.localScale = new Vector3(scaleX, originalScale.y, originalScale.z);
        }
    }
}
