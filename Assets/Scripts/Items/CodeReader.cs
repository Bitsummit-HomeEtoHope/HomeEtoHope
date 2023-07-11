// CodeReader.cs
using UnityEngine;

public class CodeReader : MonoBehaviour
{
    private GameObject capsuleDoor;
    private Animator openDoor;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        CodeHolder codeHolder = other.GetComponent<CodeHolder>(); // 获取碰撞到的物体上的CodeHolder组件
        if (codeHolder != null)
        {
            string code = codeHolder.GetCode(); // 从CodeHolder中获取code
            Debug.Log("Received code: " + code);

            string tag = other.gameObject.tag; // 获取碰撞物体的标签
            GetItem getItemScript = GameObject.Find("GetItemManager").GetComponent<GetItem>(); // 获取GetItem脚本的实例
            if (getItemScript != null)
            {
                getItemScript.ReceiveCode(code); // 将代码传递给GetItem脚本中的接收方法
                getItemScript.ReceiveTag(tag); // 将标签信息传递给GetItem脚本中的接收方法

                if(tag == "Human")
                {
                    capsuleDoor = GameObject.Find("humanDoor");
                    openDoor = capsuleDoor.GetComponent<Animator>();

                  //  Debug.Log("%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                    if (capsuleDoor != null) 
                    {

                        capsuleDoor.active = false;
                        capsuleDoor.active = true;
                       // Debug.Log("77777777777777777");
                        if (openDoor != null)
                        {
                           // Debug.Log("XXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                         openDoor.enabled = false;
                         openDoor.enabled = true;

                        }
                    }
                }

            }
            else
            {
                Debug.Log("GetItem script not found!");
            }
        }
        else
        {
            Debug.Log("no");
        }

        Destroy(other.gameObject); // 删除碰撞到的物体
    }



}
