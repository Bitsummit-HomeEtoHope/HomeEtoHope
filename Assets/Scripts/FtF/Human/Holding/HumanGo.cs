using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGo : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�

    // Update is called once per frame
    void Update()
    {
        // ͨ�����������ȡ�ƶ�����
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // �����ƶ�����
        Vector3 moveDirection = new Vector3(moveX, moveY, 0f).normalized;

        // ���½�ɫλ��
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
