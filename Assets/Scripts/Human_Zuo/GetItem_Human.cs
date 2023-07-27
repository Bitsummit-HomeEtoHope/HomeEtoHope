using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

namespace StateMachine.General
{
    public class GetItem_Human : MonoBehaviour
    {
        public GameObject GetItemManger;
        public List<GameObject> foodList_human = new List<GameObject>();
        public List<GameObject> toolList_human = new List<GameObject>();

        public List<GameObject> TreeList = new List<GameObject>();
        [SerializeField]
        private float treeDistance;
        
        public bool isFood = false;
        public bool isTool = false;
        public FSM manager;

        private void Awake() 
        {
            manager = GetComponent<FSM>();
            GetItemManger=GameObject.Find("GetItemManager");
            //获得子物体名为ToolTran的物体
            manager.parameter.Tool_Tran = transform.Find("ToolTran").gameObject;
            //-----------------GetTree在这里---------------------
            //GetTree();
        }
        private void Update()
        {
            if(isFood==false)
            {
                manager.parameter.Food_Tran.SetActive(false);
            }
            GetFood();
            GetTool();
            //-----------------原本没有GetTree--------------------
        }
        public void GetFood()
        {
            //计算当前位置与食物的距离
            float distance = Vector3.Distance(transform.position, manager.parameter.patrolPoints[2].position);
            
            if(GameManager.Instance.treeFoodList.Count!=0)
            {
                treeDistance = Vector3.Distance(transform.position, GameManager.Instance.treePoint.position);
            }
            //Debug.Log("GetItemManger.GetComponent<GetItem>().foodList.Count"+GetItemManger.GetComponent<GetItem>().foodList.Count);
            if(distance>1&&treeDistance>1&&isFood==false)
            {
                manager.parameter.Food_Tran.SetActive(false);
            }
            if(isFood==false && distance<1 && GetItemManger.GetComponent<GetItem>().foodList.Count!=0)
            {
                isFood = true;
                GameObject foodToGet=GetItemManger.GetComponent<GetItem>().foodList[0];
                foodList_human.Add(foodToGet);
                foodToGet.SetActive(false);
                GetItemManger.GetComponent<GetItem>().foodList.RemoveAt(0);//移除食物

                GetTree();
                
                manager.parameter.isHungry = false;
            }
            
            if(isFood==false && treeDistance<1 && GameManager.Instance.treeFoodList.Count!=0)
            {
                isFood = true;
                GameObject foodToGet=GameManager.Instance.treeFoodList[0];
                foodList_human.Add(foodToGet);
                foodToGet.SetActive(false);
                GameManager.Instance.treeFoodList.RemoveAt(0);//移除食物

                GetTree();

                manager.parameter.isHungry = false;
            }

            
        }

        public void GetTool()
        {
            float distance = Vector2.Distance(transform.position, manager.parameter.patrolPoints[0].position);
            if (isTool == false && distance < manager.parameter.patrolOuterRadius && GetItemManger.GetComponent<GetItem>().toolList.Count != 0)
            {
                isTool = true;
                GameObject toolToGet = GetItemManger.GetComponent<GetItem>().toolList[0];
                toolList_human.Add(toolToGet);
                toolToGet.SetActive(false);
                GetItemManger.GetComponent<GetItem>().toolList.RemoveAt(0); //移除工具
                if (toolToGet.gameObject.GetComponent<GetItem2dData>()._itemUserNum != 0)
                {
                    //TODO:还需要判断工具的种类，之后再做
                    manager.parameter.isTool = true;
                }
            }

            if (toolList_human.Count > 0 && toolList_human[0].gameObject.GetComponent<GetItem2dData>()._itemUserNum == 0)
            {
                Debug.Log("tool zero");
                manager.parameter.Tool_Tran.gameObject.SetActive(false);
                isTool = false;
                //manager.parameter.isBroken = true;
                if (manager.gameObject.GetComponent<GetItem_Human>().toolList_human[0].gameObject.GetComponent<GetItem2dData>()._isBad)
                {
                    manager.parameter.isBroken = true;
                }
                manager.parameter.isTool = false;
                manager.parameter.isWork = false;
                toolList_human.RemoveAt(0);
            }
        }
        public void GetTree()
        {
            TreeList.Clear();

            //获得场景内所有名称(其实是Tag？)为Tree的物体并加入到TreeList中
            // GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");

            {
                GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree_Set");
                foreach (GameObject tree in trees)
                {                  
                    TreeList.Add(tree);
                }
            }
        }
    }
}

