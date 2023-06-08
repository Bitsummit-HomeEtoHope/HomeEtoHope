using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestText : MonoBehaviour
{
    public Text m_Text;
    public string m_Text1;
    public string m_Text2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_Text.text = m_Text1;
        }
        if (Input.GetMouseButtonDown(1))
        {
            m_Text.text = m_Text2;
        }
    }
}
