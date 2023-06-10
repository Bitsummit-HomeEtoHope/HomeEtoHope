using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public Transform modelTransform;

    private bool _isRotate;

    private Vector3 _startPoint;

    private Vector3 _startAngle;
    
    [Range(1f,0.1f)]
    public float rotateScale = 0.1f;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(_isRotate);
        if (Input.GetMouseButtonDown(0) && !_isRotate)
        {
            _isRotate = true;
            _startPoint = Input.mousePosition;
            _startAngle = modelTransform.eulerAngles;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isRotate = false;
        }

        if (_isRotate)
        {
            var currentPoint = Input.mousePosition;
            var x = _startPoint.x - currentPoint.x;
            var y = currentPoint.y - _startPoint.y ;

            modelTransform.eulerAngles = _startAngle + new Vector3(y* rotateScale, x * rotateScale, 0);
        }
    }
}
