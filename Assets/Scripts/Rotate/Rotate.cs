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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemsManager.Instance._isCanRotate)
        {
            if(Input.GetMouseButton(0))
            {
                xRotate+=Input.GetAxis("Mouse X")*xSpeed;
                yRotate-=Input.GetAxis("Mouse Y")*ySpeed;
            
                transform.rotation=Quaternion.Euler(yRotate*rotateScale,-xRotate*rotateScale,0);
                
                
            }
        }
    }
}
