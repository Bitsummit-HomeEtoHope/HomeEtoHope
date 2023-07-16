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
        CodeHolder codeHolder = other.GetComponent<CodeHolder>(); // ��ȡ��ײ���������ϵ�CodeHolder���
        if (codeHolder != null)
        {
            string code = codeHolder.GetCode(); // ��CodeHolder�л�ȡcode
            Debug.Log("Received code: " + code);

            string tag = other.gameObject.tag; // ��ȡ��ײ����ı�ǩ
            GetItem getItemScript = GameObject.Find("GetItemManager").GetComponent<GetItem>(); // ��ȡGetItem�ű���ʵ��
            if (getItemScript != null)
            {
                getItemScript.ReceiveCode(code); // �����봫�ݸ�GetItem�ű��еĽ��շ���
                getItemScript.ReceiveTag(tag); // ����ǩ��Ϣ���ݸ�GetItem�ű��еĽ��շ���

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

        Destroy(other.gameObject); // ɾ����ײ��������
    }
}
