using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SEManager : MonoBehaviour
{
    public Slider slider;
    AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        slider.onValueChanged.AddListener(value => this.audiosource.volume = value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
