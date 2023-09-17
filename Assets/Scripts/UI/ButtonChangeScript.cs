using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChangeScript : MonoBehaviour
{

    private GameObject buttonsets;
    private Animator changeAnime;

    private bool _isChanged;
    // Start is called before the first frame update
    void OnEnable()
    {
        buttonsets = GameObject.Find("sousakuButton");
        changeAnime = buttonsets.GetComponent<Animator>();

        _isChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonChangeClick()
    {
        changeAnime.SetTrigger("ButtonChange");
        if (!_isChanged)
        {
            _isChanged = true;
            changeAnime.SetBool("willChange", true);
        }
        else
        {
            _isChanged = false;
            changeAnime.SetBool("willChange", false);
        }
    }
}
