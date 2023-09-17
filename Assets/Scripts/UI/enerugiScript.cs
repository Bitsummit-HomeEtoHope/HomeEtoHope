using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerugiScript : MonoBehaviour
{
    public List<GameObject> enerugiList = new List<GameObject>();

    public float powerChangeTimeTool = 5;
    public float powerChangeTimeHuman = 7;
    public float powerChangeTimeFood = 5;

    public bool powerIsZero = false;

    private Animator _animator;
    private GameObject _power;
    private int _powerCunt = 5;
    private bool _isTimerRunning;
    private float _powerChangeTime;
    
    // Start is called before the first frame update

    void Start()
    {
        //transform powertransform = transform.find("power");
        switch (this.name)
        {
            case "enerugi_tool":
                _powerChangeTime = powerChangeTimeTool;
                break;
            case "enerugi_human":
                _powerChangeTime = powerChangeTimeHuman;
                break;
            case "enerugi_food":
                _powerChangeTime = powerChangeTimeFood;
                break;
        }

        SetPower();
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_powerCunt == 0)
        {

        }
        else
        {

        }
    }

    private void SetPower()
    {
        _power = enerugiList[_powerCunt - 1];
        _animator = _power.GetComponent<Animator>();
    }

    public void PowerUp()
    {
        if (_powerCunt == 5) return;

        _powerCunt++;
        SetPower();
        _animator.SetTrigger("powerUp");
        PowerChange();
    }

    public void PowerDown()
    {
        SetPower();
        _powerCunt--;
        _animator.SetTrigger("powerDown");
        PowerChange();
    }

    public void PowerChange()
    {
        foreach (GameObject obj in enerugiList)
        {
            Animator animator = obj.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetInteger("powerCunt", _powerCunt);
                animator.SetTrigger("powerChange");
            }
        }
    }

    public void PowerZero()
    {

    }

    private void StartTimer()
    {
        if (!_isTimerRunning)
        {
            StartCoroutine(RunTimer());
        }
    }

    private void ResetTimer()
    {
        StopAllCoroutines();
        StartTimer();
    }

    private IEnumerator RunTimer()
    {
        _isTimerRunning = true;
        float currentTime = 0;

        while (currentTime < _powerChangeTime)
        {
            currentTime += Time.deltaTime;
            yield return null; // µÈ´ýÒ»Ö¡
        }

        if (0 < _powerCunt) PowerDown();

        _isTimerRunning = false;
        StartTimer();
    }
}
