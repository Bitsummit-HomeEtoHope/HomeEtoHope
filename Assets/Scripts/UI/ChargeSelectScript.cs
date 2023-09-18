using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeSelectScript : MonoBehaviour
{
    private string _SelectKanban;
    private Image _iconImage;
    private Image _buttonImage;
    private Transform _buttonTransform;
    private Transform _iconTransform;

    private bool _isSelected = false;
    private CircleSlider circleSlider;

    public bool isFood = false;
    public bool isHuman = false;
    public bool isTool = false;

    [SerializeField] private GameObject thekanban;
    [SerializeField] private GameObject theEnerugi;
    private SpriteRenderer _kanbanIcon;
    

    private void Start()
    {
        SelectEnerugi("No");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EnerugiCharge();
        }
    }

    public void SelectEnerugi(string toKanban)
    {
        _SelectKanban = toKanban;
        SelectChange();
        IconChange();
    }

    private void SelectChange()
    {
        switch (_SelectKanban)
        {
            case "No":
                break;
            case "Tool":
                IconReset();
                _isSelected = true;

                isTool = true;
                isHuman = false;
                isFood = false;

                circleSlider = GameObject.Find("CircleSlider").GetComponent<CircleSlider>();

                circleSlider.energy = 0;
                theEnerugi = GameObject.Find("enerugi_tool");
                thekanban = GameObject.Find("kanban_tool");
                _buttonTransform = GetComponent<Transform>().Find("ToolSelectButton");
                break;
            case "Human":
                IconReset();
                _isSelected = true;

                isTool = false;
                isHuman = true;
                isFood = false;

                theEnerugi = GameObject.Find("enerugi_human");
                thekanban = GameObject.Find("kanban_human");
                _buttonTransform = GetComponent<Transform>().Find("HumanSelectButton");
                break;
            case "Food":
                IconReset();
                _isSelected = true;

                isTool = false;
                isHuman = false;
                isFood = true;

                theEnerugi = GameObject.Find("enerugi_food");
                thekanban = GameObject.Find("kanban_food");
                _buttonTransform = GetComponent<Transform>().Find("FoodSelectButton");
                break;
        }
        _iconTransform = _buttonTransform.GetComponent<Transform>().Find("Image");

        _kanbanIcon = thekanban.GetComponent<SpriteRenderer>();
        _buttonImage = _buttonTransform.GetComponent<Image>();
        _iconImage = _iconTransform.GetComponent<Image>();

    }

    private void IconChange()
    {
        _kanbanIcon.color = Color.white;
        _buttonImage.color = Color.white;
        _iconImage.color = new Color(0.4f,0.4f,0.4f);
    }

    private void IconReset()
    {
        if (_isSelected)
        {
            _kanbanIcon.color = new Color(0.3f, 0.2f, 0.1f);
            _buttonImage.color = new Color(0.4f,0.4f,0.4f);
            _iconImage.color = Color.white;
        }
    }

    public void EnerugiCharge()
    {
        enerugiScript enerugiScript = theEnerugi.GetComponent<enerugiScript>();
        enerugiScript.PowerUp();
    }
}
