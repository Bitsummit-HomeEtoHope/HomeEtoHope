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

        Destroy(other.gameObject); // ɾ����ײ��������
    }



}
