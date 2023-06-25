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
    private int currentTime = 0;
    private bool isPause = false;
    private int isChangeBefore = 0;
    private int isChangeAfter = 0;
    public int isCheck = 0;


    // Start is called before the first frame update
    void Start()
    {
        levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
  //      CountDown();
    }

    //void CountDown()
    //{
       
    //    if (isChangeAfter != isChangeBefore && !isPause)
    //    {
    //        isChangeBefore = isChangeAfter;
    //        isPause = true;
    //        Time.timeScale = 0;
    //        ShadyOpen();
    //    }
    //}


    public void ShadyOpen()
    {
        currentTime = 0;
        shady.SetActive(true);
    }

    public void ShadyClose()
    {
        shady.SetActive(false);
        Time.timeScale = 1;
    }
}
