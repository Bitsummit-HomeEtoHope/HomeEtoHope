using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    
    public float xSpeed=0.1f;
    public float ySpeed=0.1f;
    [Range(0.1f,1f)]
    public float rotateScale = 1f;
    private float xRotate;
    private float yRotate;
    private Vector3 initiaMousePosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemsManager.Instance._isCanRotate)
        {
            if(Input.GetMouseButtonDown(0))
            {
                initiaMousePosition=Input.mousePosition;
            }
            if(Input.GetMouseButton(0))
            {
                Vector3 delta=Input.mousePosition-initiaMousePosition;
                xRotate=-delta.y*ySpeed*rotateScale;
                yRotate=-delta.x*xSpeed*rotateScale;
                
                transform.Rotate(Vector3.right,-xRotate,Space.World);
                transform.Rotate(Vector3.up,yRotate,Space.World);
                initiaMousePosition=Input.mousePosition;
                
                
                
            }
        }
    }
    

}
