using System.Threading;
using UnityEngine;

public class CodeReader : MonoBehaviour
{
    private GameObject capsuleDoor;
    private Animator openDoor;


    [SerializeField] private AudioSource sePlayer;

    [SerializeField] private AudioClip maruSE;
    [SerializeField] private AudioClip humanSE;
    [SerializeField] private AudioClip batsuSE;

    private GetItem getItem;

    private void Start()
    {
        getItem = FindObjectOfType<GetItem>();
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

                if (tag == "Human" )
                {
                    capsuleDoor = GameObject.Find("humanDoor");
                    openDoor = capsuleDoor.GetComponent<Animator>();

                    if (capsuleDoor != null)
                    {
                        capsuleDoor.SetActive(false);
                        capsuleDoor.SetActive(true);

                        if (openDoor != null)
                        {
                            openDoor.enabled = false;
                            openDoor.enabled = true;
                        }
                    }
                }


                bool codeMatches = false;

                if (getItem.theirList != null)
                {
                    foreach (string feature in getItem.theirList)
                    {
                        if (code.Contains(feature))
                        {
                            codeMatches = true;
                            break;
                        }
                    }
                }


                if (code.Contains("broken") || code.Contains("hi") || code.Contains("qaq"))// || codeMatches
                {
                    Debug.Log("22222222222222222Code is right!!222222222222222222");
                   if(batsuSE!=null) sePlayer.GetComponent<AudioSource>().PlayOneShot(batsuSE);
                }
                else if (codeMatches)
                {
                    if(humanSE!=null)sePlayer.GetComponent<AudioSource>().PlayOneShot(humanSE);
                }
                else
                {
                    if(maruSE!=null)sePlayer.GetComponent<AudioSource>().PlayOneShot(maruSE);
                }



            }
            else
            {
                Debug.Log("GetItem script not found!");
            }
        }
        else
        {
            Debug.Log("CodeHolder component not found!");
        }

        Destroy(other.gameObject); // 删除碰撞到的物体
    }
}
