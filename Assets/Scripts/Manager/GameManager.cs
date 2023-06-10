using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : SingletonManager<GameManager>
{
    
    public LevelDataCurrent levelDataCurrent;
    [Tooltip("Temporarily replace the shady screen at the end of the level")]
    public GameObject shady;
    private Text LevelTime;
    private int _currentTime=0;
    private bool _isPause = false;
    private int isChangeBefore=0;
    private int isChangeAfter=0;
    public int isCheck=0;
    // Start is called before the first frame update
    void Start()
    {
        LevelTime = GameObject.Find("LevelTime").GetComponent<Text>();
        levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        _isPause = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CountDown();
    }

    void CountDown()
    {
        
        if (Time.time - _currentTime >= 1)
        {
            _currentTime = (int)(Time.time%levelDataCurrent._levelTime);
            isChangeAfter=(int)(Time.time/levelDataCurrent._levelTime);
            LevelTime.text = (levelDataCurrent._levelTime - _currentTime).ToString();
        }
        //TODO: Temporarily use the method of pausing and adding shady to present, and then modify it later
        if(isChangeAfter!=isChangeBefore&&!_isPause)
        {
            isChangeBefore=isChangeAfter;
            _isPause = true;
            Time.timeScale = 0;
            ShadyOpen();

        }
    }

    public void ShadyOpen()
    {
        _currentTime = 0;
        shady.SetActive(true);
        //Time.timeScale = 0;
    }
    public void ShadyClose()
    {
        shady.SetActive(false);
        Time.timeScale = 1;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
