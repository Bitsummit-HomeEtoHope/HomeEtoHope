using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DaysManager : MonoBehaviour
{
    public enum Days { Day0,Day1, Day2, Day3 }

    [SerializeField] private Days currentDay = Days.Day0;

    public Days GetCurrentDay()
    {
        return currentDay;
    }

    [Header("DayImage")]
    [SerializeField] public List<Sprite> humanlist;
    [SerializeField] public List<Sprite> wholist;
    [SerializeField] public List<Sprite> namelist;
    [SerializeField] public Image Daysnubmer;
    [Header("what people")]
    [SerializeField] public Sprite today1;
    [SerializeField] public Sprite today2;
    [SerializeField] public Sprite today3;

    [Header("News Day1")]
    [SerializeField] public Image Day1_what;
    [SerializeField] public Image Day1_who;
    [SerializeField] public Image Day1_name;
    [SerializeField] public Sprite Day1_when;

    [Header("News Day2")]
    [SerializeField] public Image Day2_what;
    [SerializeField] public Image Day2_who;
    [SerializeField] public Image Day2_name;
    [SerializeField] public Sprite Day2_when;

    [Header("News Day3")]
    [SerializeField] public Image Day3_what;
    [SerializeField] public Image Day3_who;
    [SerializeField] public Image Day3_name;
    [SerializeField] public Sprite Day3_when;

    [Header("List_Day1")]
    [SerializeField] private Image List_Day1;
    [SerializeField] public Image humanday_1;
    [SerializeField] public Image megane_1;

    [Header("List_Day2")]
    [SerializeField] private Image List_Day2; 
    [SerializeField] private Sprite day2_Day2;
    [SerializeField] public Image humanday_2;
    [SerializeField] public Image megane_2;

    [Header("List_Day3")]
    [SerializeField] private Image List_Day3; 
    [SerializeField] private Sprite day3_Day3;
    [SerializeField] public Image humanday_3;
    [SerializeField] public Image megane_3;

    [Header("HumanTell")]
    [SerializeField] public string selectedHumanName;
    public event Action<string> OnHumanSelected; // 定义事件
    public static string SelectedHumanName { get; private set; }

    [Header("News")]
    [SerializeField] public GameObject NewsContruler;//NewsManager
    [Header("Ending")]
    [SerializeField]public string EndingScene;

    private void Start()
    {
    }

    private void OnEnable()
    {
        daysChange();
    }

    private void OnDisable()
    {

    }

    public void daysChange()
    {
        switch (currentDay)
        {
            case Days.Day0:

                if ( humanlist.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, humanlist.Count);
                    Sprite whoSprite = wholist[randomIndex];
                    Sprite nameSprite = namelist[randomIndex];
                    Sprite listSprite = humanlist[randomIndex];

                    //天数
                    if (Daysnubmer != null && Day1_when != null) Daysnubmer.sprite = Day1_when;
                    //类型
                    if (Day1_what!=null)Day1_what.sprite = today1;
                    //新闻
                    if(Day1_who!=null)Day1_who.sprite = whoSprite;
                    //名字
                    if(Day1_name!=null)Day1_name.sprite = nameSprite;
                    //名单
                    if(megane_1!=null)megane_1.sprite = listSprite;

                    humanday_1.transform.localScale = Vector3.one;

                    // 获取选定人物的名称
                    SelectedHumanName = humanlist[randomIndex].name;

                    // 触发事件并传递选定人物的名称
                    OnHumanSelected?.Invoke(SelectedHumanName);

                    // 删除已选择
                    wholist.RemoveAt(randomIndex);
                    namelist.RemoveAt(randomIndex);
                    humanlist.RemoveAt(randomIndex);
                }

                if (NewsContruler != null && !NewsContruler.activeSelf) NewsContruler.active = true;

                currentDay = Days.Day1;

                break;

            case Days.Day1:

                if (megane_2 != null && humanlist.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, humanlist.Count);
                    Sprite whoSprite = wholist[randomIndex];
                    Sprite nameSprite = namelist[randomIndex];
                    Sprite listSprite = humanlist[randomIndex];

                    if (Daysnubmer != null && Day1_when != null) Daysnubmer.sprite = Day2_when;

                    if (Day2_what != null) Day2_what.sprite = today2;
                    
                    if (Day2_who != null) Day2_who.sprite = whoSprite;

                    if (Day2_name != null) Day2_name.sprite = nameSprite;

                    if (megane_2 != null) megane_2.sprite = listSprite;

                    humanday_2.transform.localScale = Vector3.one;

                    SelectedHumanName = humanlist[randomIndex].name;
                    OnHumanSelected?.Invoke(SelectedHumanName);

                    wholist.RemoveAt(randomIndex);
                    namelist.RemoveAt(randomIndex);
                    humanlist.RemoveAt(randomIndex);
                }


                if (NewsContruler != null && !NewsContruler.activeSelf) NewsContruler.active = true;

                List_Day2.sprite = day2_Day2;

                currentDay = Days.Day2;
                break;

            case Days.Day2:

                if (megane_3 != null && humanlist.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, humanlist.Count);
                    Sprite whoSprite = wholist[randomIndex];
                    Sprite nameSprite = namelist[randomIndex];
                    Sprite listSprite = humanlist[randomIndex];

                    //---------------------------
                    //
                    if (Daysnubmer != null && Day1_when != null) Daysnubmer.sprite = Day3_when;

                    if (Day3_what != null) Day3_what.sprite = today3;
                    //
                    if (Day3_who != null) Day3_who.sprite = whoSprite;
                    //
                    if (Day3_name != null) Day3_name.sprite = nameSprite;
                    //
                    if (megane_3 != null) megane_3.sprite = listSprite;
                    //----------------------------

                    humanday_3.transform.localScale = Vector3.one;

                    SelectedHumanName = humanlist[randomIndex].name;
                    OnHumanSelected?.Invoke(SelectedHumanName);

                    wholist.RemoveAt(randomIndex);
                    namelist.RemoveAt(randomIndex);
                    humanlist.RemoveAt(randomIndex);
                }


                if (NewsContruler != null && !NewsContruler.activeSelf) NewsContruler.active = true;

                List_Day3.sprite = day3_Day3;

                currentDay = Days.Day3;
                break;


                case Days.Day3:
                //TODO: 结束游戏转为结束场景。

                SceneManager.LoadScene(EndingScene);

                break;

            default:
                Debug.LogError("Invalid day!");
                break;
        }
    }
}
