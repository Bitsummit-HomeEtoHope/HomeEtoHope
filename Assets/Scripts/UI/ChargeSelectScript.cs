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
                theEnerugi = GameObject.Find("enerugi_tool");
                thekanban = GameObject.Find("kanban_tool");
                _buttonTransform = GetComponent<Transform>().Find("ToolSelectButton");
                break;
            case "Human":
                IconReset();
                _isSelected = true;
                theEnerugi = GameObject.Find("enerugi_human");
                thekanban = GameObject.Find("kanban_human");
                _buttonTransform = GetComponent<Transform>().Find("HumanSelectButton");
                break;
            case "Food":
                IconReset();
                _isSelected = true;
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
        _buttonImage.color = Color.green;
        _iconImage.color = new Color(0.5f, 0, 0); // 0, 0.5, 0
    }

    private void IconReset()
    {
        if (_isSelected)
        {
            _kanbanIcon.color = Color.white;
            _buttonImage.color = new Color(0, 0.5f, 0);
            _iconImage.color = new Color(0.3f,0.2f,0.1f);
        }
    }

    private void EnerugiCharge()
    {
        EnerugiScript enerugiScript = theEnerugi.GetComponent<EnerugiScript>();
        enerugiScript.PowerUp();
    }
}
