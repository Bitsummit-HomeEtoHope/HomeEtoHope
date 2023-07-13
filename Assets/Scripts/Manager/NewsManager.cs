using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> news_move;
    [SerializeField] public List<GameObject> news_zoom;
    [SerializeField] public List<Image> news_open;//<---- open!!!
    [SerializeField] public bool isover = false;

    [Header("Ready")]
    // [SerializeField] private bool isPaused = false; // 控制暂停状态
    // [SerializeField] public List<GameObject> targetObjects; // 多个游戏对象
    public bool readyone = false;
    private bool readytwo = false;
    //----------------------
    private GameObject DayNumber;
    private Dictionary<Image, float> defaultFillAmounts;
    private Dictionary<GameObject, Vector3> defaultPositions;
    private Dictionary<GameObject, Vector3> defaultScales;
    private float zoom_1;
    private float zoom_2;
    private Image NewsOpen;

    //-----------------------

    [Header("Move")]
    [SerializeField] private float move_x; // Distance to move along the positive x-axis
    [SerializeField] private float move_time; // Time taken for the movement

    [Header("Zoom")]
    [SerializeField] private float zoom_a; // First scale change
    [SerializeField] private float time_zoom_a; // Time taken for the first scale change
    [SerializeField] private float zoom_b; // Second scale change
    [SerializeField] private float time_zoom_b; // Time taken for the second scale change

    [Header("Open")]
    [SerializeField] private float fillDuration = 1.0f;
    [SerializeField] public GameObject OpenNews;
    [SerializeField] public Image NewsTitle;
    [SerializeField] public Image OpenUp;
    [SerializeField] public Image OpenDown;

    [Header("--One")]
    [SerializeField] public Sprite Open_up_a;
    [SerializeField] public Sprite Open_down_a;
    [SerializeField] public Sprite OpenOne;
    [SerializeField] public Sprite TitleOne;

    [Header("--Two")]
    [SerializeField] public Sprite Open_up_b;
    [SerializeField] public Sprite Open_down_b;
    [SerializeField] public Sprite OpenTwo;
    [SerializeField] public Sprite TitleTwo;

    //[Header("Level")]
    //[SerializeField] public GameObject LevelData;

    [Header("Count TEN")]
    public string targetSceneName; // 目标场景的名称
    public bool startTimer = false; // 开始计时的开关
    public float delay = 10f; // 延迟时间

    [Header("-----------------------------------------")]
    private GameObject Bar;
    private GameObject Back;
    public GameObject Shady;
    public GameObject readyWhat;
    public GameObject readyWho;
    public GameObject TheNews;
    public List<GameObject> ShutDownList;
    //public GameObject Items;
    //public GameObject PortBelt;
    //public GameObject Moniter;
    //public GameObject HumanList;
    [Header("-----------------------------------------")]
    //public float duration = 0.5f;
    private float timer = 0f; // 计时器
    private bool canClick = false; // 计时器


    private void Start()
    {
        if (readyWhat != null && readyWho != null) Debug.Log("----lest go !----");
        Bar = GameObject.Find("news_bar");
        Back = GameObject.Find("news_back");
        TheNews = GameObject.Find("Canvas_News");
       // Shady = GameObject.Find("--Shady--");
        //Items = GameObject.Find("ItemsManager");
        //PortBelt = GameObject.Find("PortBeltManager");
        //Moniter = GameObject.Find("Canvas_Moniter_Button");
        //HumanList = GameObject.Find("--List--Timmer");

        //---------
        NewsOpen = OpenNews.GetComponent<Image>();
        DayNumber = GameObject.Find("point_days_number");
        //---------

        //Move
        defaultPositions = new Dictionary<GameObject, Vector3>();

        foreach (GameObject newsObject in news_move)
        {
            defaultPositions.Add(newsObject, newsObject.transform.position);
        }

        //Zoom
        defaultScales = new Dictionary<GameObject, Vector3>();

        foreach (GameObject newsObject in news_zoom)
        {
            defaultScales.Add(newsObject, newsObject.transform.localScale);
        }

        //Open
        defaultFillAmounts = new Dictionary<Image, float>();

        foreach (Image image in news_open)
        {
            defaultFillAmounts.Add(image, image.fillAmount);
        }
    }

    private void OnEnable()
    {
        canClick = false;
        timer = 0f;
        startTimer = true;

        if (TheNews != null && !TheNews.activeSelf) TheNews.active = true;

        if (Bar != null)if (!Bar.activeSelf)Bar.active = true;
        //if(Items!=null)Items.active = false;
        //if(PortBelt!=null)PortBelt.active = false;
        //if(Moniter!=null)Moniter.active = false;
        //if(HumanList!=null)HumanList.active = false;
        if (DayNumber != null) DayNumber.active = true;
        //isover = false;
        if (readyone)
        {
            //-----------------------
            if (readyWhat != null && readyWhat.activeSelf) readyWhat.active = false;
            if (readyWho != null && readyWho.activeSelf) readyWho.active = false;
            //-----------------------
            NewsOne();
            isover = false;
            readyone = false;
            NewsReady_Move();
            NewsReady_Open();
            NewsReady_Zoom();
        }
        zoom_1 = zoom_a;
        zoom_2 = zoom_b;
        //Pause();


        if (readytwo)
        {
            //readytwo = false;
            NewsTwo();
            if (DayNumber != null) DayNumber.active = false;
            NewsReady_Open();
            StartCoroutine(OpenBord());
            if (!readyone) readyone = true;
        }

        StartCoroutine(StartAfterDelay());        
    }


    public void takeThemOn()
    {
        foreach (GameObject obj in ShutDownList)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }


    public void takeThemOff()
    {
        foreach (GameObject obj in ShutDownList)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        // readytwo = true;
         startTimer = false;
         enabled = true;

    }

    private void NewsOne() 
    {
        NewsTitle.sprite = TitleOne;
        OpenUp.sprite = Open_up_a;
        OpenDown.sprite = Open_down_a;
        if(NewsOpen!=null)NewsOpen.sprite = OpenOne;
    }
    private void NewsTwo() 
    {
        NewsTitle.sprite = TitleTwo;
        OpenUp.sprite = Open_up_b;
        OpenDown.sprite = Open_down_b;
        NewsOpen.sprite = OpenTwo;
    }


    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(MoveNewsObjects());
        StartCoroutine(OpenBord());
    }

    private void Update()
    {


        //SetTimeScale(1f);
        if (isover)
        StartCoroutine(UpdateAfterDelay());

        if (startTimer)
        {
            timer += Time.deltaTime; // 增加计时器

            if (timer >= delay)
            {
                if (readytwo)
                {
                    timer = 0;
                    readytwo = false;
                    takeThemOn();

                    if (Shady != null) if (Shady.activeSelf) Shady.active = false;
                    TheNews.active = false;
                    gameObject.SetActive(false);
                }
                else
                {
                    timer = 0;
                    readytwo = true;

                    if (readyWhat != null && !readyWhat.activeSelf)readyWhat.active = true;
                    if (readyWho != null && !readyWho.activeSelf)readyWho.active = true;
                    enabled = false;
                }
            }
        }

      

    }


    public void NewsClick()
    {
        // 检测鼠标左击   && Input.GetMouseButtonDown(0)
        if (canClick) 
        {
            if (readytwo)
            {
                timer = 0;
                readytwo = false;
                takeThemOn();
                if (Shady != null) if (Shady.activeSelf) Shady.active = false;
                TheNews.active = false;
                gameObject.SetActive(false);
                //Bar.SetActive(false);
            }
            else
            {
                GetComponent<AudioSource>().Play();

                timer = 0;
                readytwo = true;

                if (readyWhat != null && !readyWhat.activeSelf) readyWhat.active = true;
                if (readyWho != null && !readyWho.activeSelf) readyWho.active = true;
                enabled = false;
            }
        }

    }
   


    private void SwitchScene()
    {
        SceneManager.LoadScene(targetSceneName); // 加载目标场景
    }

    /*

     public void OnButtonClick()
    {
        scaleController.NewsZoom(0f,"news_back");     // 将比例缩小到0
    }


    private void SetTimeScale(float timeScale)
    {
        // 检查是否指定了游戏对象
        if (targetObjects != null && targetObjects.Count > 0)
        {
            foreach (GameObject obj in targetObjects)
            {
                // 设置游戏对象的时间缩放值
                obj.GetComponent<Rigidbody>().velocity = Vector3.zero; // 重置刚体速度（可选）
            }
            Time.timeScale = timeScale;
        }
    }

    */

    private void NewsReady_Zoom()
    {
        zoom_1 = 0;
        zoom_2 = 0;

        foreach (GameObject newsObject in news_zoom)
        {
            // 获取默认缩放
            Vector3 defaultScale = defaultScales[newsObject];

            // 将游戏物体缩放设置为默认缩放
            newsObject.transform.localScale = defaultScale;
        }
    }

    private void NewsReady_Move()
    {
        foreach (GameObject newsObject in news_move)
        {
            // 获取默认位置
            Vector3 defaultPosition = defaultPositions[newsObject];

            // 将游戏物体位置设置为默认位置
            newsObject.transform.position = defaultPosition;
        }
    }

    private void NewsReady_Open()
    {
        foreach (Image image in news_open)
        {
            // 获取默认FillAmount值
            float defaultFillAmount = defaultFillAmounts[image];

            // 设置Image组件的FillAmount为默认值
            image.fillAmount = defaultFillAmount;
        }
    }

    private IEnumerator UpdateAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (isover)
        {
            StartCoroutine(ZoomNewsObjects());
        }
    }

    private IEnumerator OpenBord()
    {
      //  OpenNews.SetActive(false);

        float elapsedTime = 0f;

        List<float> startFillAmounts = new List<float>();
        List<float> targetFillAmounts = new List<float>();

        foreach (Image bordImage in news_open)
        {
            startFillAmounts.Add(bordImage.fillAmount);
            targetFillAmounts.Add(1f);
        }

        while (elapsedTime < fillDuration)
        {
            float t = elapsedTime / fillDuration;

            for (int i = 0; i < news_open.Count; i++)
            {
                Image bordImage = news_open[i];
                float startFillAmount = startFillAmounts[i];
                float targetFillAmount = targetFillAmounts[i];

                bordImage.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < news_open.Count; i++)
        {
            Image bordImage = news_open[i];
            bordImage.fillAmount = targetFillAmounts[i];
        }

        yield return new WaitForSeconds(0.1f);
      //  OpenNews.SetActive(true);
    }

    private IEnumerator ZoomNewsObjects()
    {
        if (isover)
        {
            isover = false;
        }

        foreach (GameObject newsObject in news_zoom)
        {
            yield return StartCoroutine(Zoom(newsObject.transform, zoom_1, time_zoom_a));
            yield return StartCoroutine(Zoom(newsObject.transform, zoom_2, time_zoom_b));
        }

        yield return new WaitForSeconds(1f);
        if (!isover)
        {
            isover = true;
        }

        startTimer = true;
    }

    private IEnumerator Zoom(Transform targetTransform, float targetScale, float duration)
    {
        Vector3 initialScale = targetTransform.localScale;
        Vector3 targetScaleVector = Vector3.one * targetScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
                float t = elapsedTime / duration;
                targetTransform.localScale = Vector3.Lerp(initialScale, targetScaleVector, t);
                elapsedTime += Time.deltaTime;

            yield return null;
        }

        targetTransform.localScale = targetScaleVector;
    }

    private IEnumerator MoveNewsObjects()
    {
        foreach (GameObject newsObject in news_move)
        {
            Move(newsObject.transform);
        }

        yield return new WaitForSeconds(move_time);

        canClick = true;

        StartCoroutine(ZoomNewsObjects());

    }

    private void Move(Transform targetTransform)
    {
        StartCoroutine(MoveNews(targetTransform));
    }

    private IEnumerator MoveNews(Transform targetTransform)
    {
        float elapsedTime = 0f;
        float moveDuration = move_time;
        float moveDistance = move_x;
        Vector3 startPosition = targetTransform.localPosition;
        Vector3 targetPosition = targetTransform.localPosition + new Vector3(moveDistance, 0f, 0f);

        while (elapsedTime < moveDuration)
        {           
                float t = elapsedTime / moveDuration;
                targetTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;           

            yield return null;
        }

        targetTransform.localPosition = targetPosition;
    }



}

    /*

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f; // 暂停时间缩放
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f; // 恢复时间缩放
    }

    */


