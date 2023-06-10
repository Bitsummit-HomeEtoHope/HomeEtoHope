using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float xSpeed = 0.1f;
    public float ySpeed = 0.1f;
    [Range(0.1f, 1f)]
    public float rotateScale = 1f;
    private float xRotate;
    private float yRotate;
    private Vector3 initialMousePosition;
    private Quaternion previousRotation;
    
    private bool canRotate = false;

    private void Start()
    {
        previousRotation = transform.rotation;
    }

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
                xRotate = -delta.y * ySpeed * rotateScale;
                yRotate = -delta.x * xSpeed * rotateScale;

                transform.Rotate(Vector3.right, -xRotate, Space.World);
                transform.Rotate(Vector3.up, yRotate, Space.World);
                initialMousePosition = Input.mousePosition;
            }
            if (transform.rotation != previousRotation)
            {
                // オブジェクトが回転した瞬間に効果音を再生
                GetComponent<AudioSource>().Play();
            }

            previousRotation = transform.rotation;
        }
    }

    private void OnMouseDown()
    {
        canRotate = true;
        string objectTag = GetTag();
        if (objectTag == "Food")
        {
            ShowList showList = FindObjectOfType<ShowList>();
          
                showList.OpenList();
            
        }
    }

    private void OnMouseUp()
    {
        canRotate = false;
        string objectTag = GetTag();
        if (objectTag == "Human")
        {
            ShowList showList = FindObjectOfType<ShowList>();
           
                showList.OffList();
            
        }
        ItemsManager.Instance._isCanRotate = false;
    }


    private string GetTag()
    {
        return gameObject.tag;
    }
}
