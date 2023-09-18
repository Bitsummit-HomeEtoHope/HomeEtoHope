using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public float energy;

    [SerializeField]
    float targetEnergy;

    private ChargeSelectScript chargeSelectScript;
    private bool isDragging = false;
    [SerializeField] private GameObject theBattery;


    float angle;
    Vector3 mousePos;

    private float rotationSpeed = 15f;

    public void OnHandleDrag()
    {
        isDragging = true;

        mousePos = Input.mousePosition;
        var dir = mousePos - circleSlider.position;
        angle = Vector2.Angle(dir, Vector2.up);
        angle = (mousePos.x > transform.position.x) ? angle : 360 - angle;
        transform.rotation = Quaternion.Euler(0, 0, -angle);
    }

    public void OnHandleDrop()
    {
        isDragging = false;
    }

    private void Start()
    {

        chargeSelectScript = GameObject.Find("ChargeSelectButton").GetComponent<ChargeSelectScript>();
    }

    public void Update()
    {
        if (isDragging)
        {
            increament = angle - lastAngle;
            if (increament >= 0 && increament < 300)
            {
                energy += increament / 360; 
            }
        }
        else
        {
            energy -= increament / 360;
            if (transform.rotation.eulerAngles.z > 0.5f)
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                energy = 0;
            }
        }

        if (energy >= targetEnergy)
        {
            chargeSelectScript.EnerugiCharge();
            energy = 0;
        }

        lastAngle = angle;
        ShowBattery();
    }

    private void ShowBattery()
    {
        if (chargeSelectScript.isTool)
        {
            theBattery = GameObject.Find("battery_tool");
        }
        else if (chargeSelectScript.isHuman)
        {
            theBattery = GameObject.Find("battery_human");
        }
        else if (chargeSelectScript.isFood)
        {
            theBattery = GameObject.Find("battery_food");
        }
        SpriteRenderer batteryRenderer = theBattery.GetComponent<SpriteRenderer>();

        Color batteryColor = batteryRenderer.color;
        batteryColor.a = energy / 3;
        batteryRenderer.color = batteryColor;
    }
}
