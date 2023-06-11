using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standingby : MonoBehaviour
{
    public Transform[] capsules; // �洢����capsule��Transform���
    public float yOffset = 0f; // Y��ƫ����

    private int playerLayer; // Player�������
    private float capsuleY; // capsule��Yֵ

    // Start is called before the first frame update
    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player"); // ��ȡPlayer�������
        capsuleY = capsules[0].position.y; // ��ȡcapsule��Yֵ
    }

    // Update is called once per frame
    void Update()
    {
        // ����ɫ�Ƿ���capsules�ص�
        bool overlapping = false;
        foreach (Transform capsule in capsules)
        {
            // ����ɫ����������ת��Ϊ����ڳ����ı�������
            Vector3 localPosition = transform.InverseTransformPoint(capsule.position);

            // ʹ�ñ���������бȽ�
            if (localPosition == Vector3.zero)
            {
                overlapping = true;
                break;
            }
        }

        if (overlapping)
        {
            // ����ɫ���ص�capsule��λ�ù�ϵ
            if (transform.position.y > capsuleY)
            {
                // ��ɫ��capsule���棬���ý�ɫ��order in layerΪPlayer���������1
                GetComponent<Renderer>().sortingOrder = playerLayer - 1;
            }
            else
            {
                // ��ɫ��capsuleǰ�棬���ý�ɫ��order in layerΪPlayer���������1
                GetComponent<Renderer>().sortingOrder = playerLayer + 1;
            }

            // �Խ�ɫ��Yֵ���е���
            float adjustedY = capsuleY + yOffset;
            transform.position = new Vector3(transform.position.x, adjustedY, transform.position.z);
        }
    }
}
