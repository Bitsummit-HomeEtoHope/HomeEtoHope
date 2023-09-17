using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnerugiScript : MonoBehaviour
{
    [SerializeField]
    private LevelDataCurrent levelDataCurrent;
    public List<GameObject> enerugiList = new List<GameObject>();

    public float powerChangeTimeTool = 5;   //Tool传送器电量扣电时间
    public float powerChangeTimeHuman = 7;  //Human传送器电量扣电时间
    public float powerChangeTimeFood = 5;   //Food传送器电量扣电时间

    public bool powerZero = false;  //这个bool变量的最终值统一“是否电量归零”

    public bool _powerZeroTool = false;
    public bool _powerZeroHuman = false;
    public bool _powerZeroFood = false;

    private Animator _animator;
    private GameObject _power;
    private int _powerCunt = 5;     //总电量数（关联animator数据，暂时写死了
    private bool _isTimerRunning;
    private float _powerChangeTime;     

    // Start is called before the first frame update


    void Start()
    {
        levelDataCurrent=GameObject.FindObjectOfType<LevelDataCurrent>();
        powerChangeTimeFood=levelDataCurrent._food_Power;
        powerChangeTimeHuman=levelDataCurrent._human_Power;
        powerChangeTimeTool=levelDataCurrent._tool_Power;
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
        PowerZero();
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
        switch (this.name)
        {
            case "enerugi_tool":
                if (_powerCunt != 0) _powerZeroTool = false;
                else _powerZeroTool = true;

                powerZero = _powerZeroTool;
                break;
            case "enerugi_human":
                if (_powerCunt != 0) _powerZeroHuman = false;
                else _powerZeroHuman = true;

                powerZero = _powerZeroHuman;
                break;
            case "enerugi_food":
                if (_powerCunt != 0) _powerZeroFood = false;
                else _powerZeroFood = true;

                powerZero = _powerZeroFood;
                break;
        }
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
            yield return null; 
        }

        if (0 < _powerCunt) PowerDown();

        _isTimerRunning = false;
        StartTimer();
    }
}
