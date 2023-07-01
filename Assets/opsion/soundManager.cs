using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class soundManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    private float ConvertVolume2dB(float volume)
    {
        return Mathf.Clamp(20f * Mathf.Log10(Mathf.Clamp(volume, 0f, 1f)), -80f, 0f);
    }
    public void setBGMvolume(float volume)
    {
        audioMixer.SetFloat("BGMVolume",ConvertVolume2dB(volume*0.01f));
    }
    public void setSEvolume(float volume)
    {
        audioMixer.SetFloat("SEVolume", ConvertVolume2dB(volume * 0.01f));
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
