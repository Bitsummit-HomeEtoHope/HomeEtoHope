using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotate : MonoBehaviour
{
    public float xSpeed = 1.5f;
    public float ySpeed = 1.5f;
    [Range(0.1f, 1f)]
    public float rotateScale = 1f;
    private float xRotate;
    private float yRotate;
    private Vector3 initialMousePosition;

    private bool canRotate = false;

    private void Update()
    {
        if (ItemsManager.Instance._isCanRotate && canRotate)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - initialMousePosition;
                xRotate = -delta.y * xSpeed * rotateScale;
                yRotate = -delta.x * ySpeed * rotateScale;

                transform.Rotate(Vector3.right, -xRotate, Space.World);
                transform.localRotation *= Quaternion.Euler(0f, yRotate, 0f); // 限制 y 轴旋转在物体自身上
                initialMousePosition = Input.mousePosition;
            }
        }
    }

    private void OnMouseDown()
    {
        canRotate = true;
    }

    private void OnMouseUp()
    {
        canRotate = false;
        ItemsManager.Instance._isCanRotate = false;
    }
}
