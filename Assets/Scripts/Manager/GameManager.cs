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

    [SerializeField] private Image fillImage;

    // Start is called before the first frame update
    void Start()
    {
        levelDataCurrent = FindObjectOfType<LevelDataCurrent>();
        isPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    void CountDown()
    {
        float fillSpeed = 1f / levelDataCurrent._levelTime; // 每秒填充的速度

        float fillAmount = fillImage.fillAmount - fillSpeed * Time.deltaTime;
        fillImage.fillAmount = Mathf.Clamp01(fillAmount); // 将填充量限制在0到1之间

        if (Time.time - currentTime >= 1)
        {
            currentTime = (int)(Time.time % levelDataCurrent._levelTime);
            isChangeAfter = (int)(Time.time / levelDataCurrent._levelTime);
        }

        if (isChangeAfter != isChangeBefore && !isPause)
        {
            isChangeBefore = isChangeAfter;
            isPause = true;
            Time.timeScale = 0;
            ShadyOpen();
        }
    }


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
