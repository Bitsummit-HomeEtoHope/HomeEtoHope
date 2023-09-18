using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CircleSlider : MonoBehaviour
{
    [SerializeField]
    Transform circleSlider;

    [SerializeField]
    float lastAngle;

    [SerializeField]
    float currentAngle;

    [SerializeField]
    float increament;

    [SerializeField]
    float energy;

    [SerializeField]
    float targetEnergy;

    float angle;
    Vector3 mousePos;
    public void OnHandleDrag()
    {
        mousePos = Input.mousePosition;
        var dir = mousePos - circleSlider.position;
        angle = Vector2.Angle(dir, Vector2.up);
        angle = (mousePos.x > transform.position.x) ? angle : 360 - angle;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    public void Update()
    {
        increament = angle - lastAngle;
        if(increament >= 0 && increament <300)
            energy += increament/360;
        if (energy >= targetEnergy)
            Debug.Log("Yeahhhhhhhhh!");
    }

    private void LateUpdate()
    {
        lastAngle = angle;
    }
}
