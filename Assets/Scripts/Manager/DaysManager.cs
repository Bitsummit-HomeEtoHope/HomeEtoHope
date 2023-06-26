using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaysManager : MonoBehaviour
{
    public enum Days { Day0,Day1, Day2, Day3 }

    [SerializeField] private Days currentDay = Days.Day0;
    [Header("DayImage")]
    [SerializeField] private Image Days_Image; // 参考目标 UI 图像的 Image 组件
    [SerializeField] private Sprite Day2Image; // day1替换对象
    [SerializeField] private Sprite Day3Image; // day2替换对象
    [SerializeField] public List<Sprite> humanlist;

    [Header("List_Day1")]
    [SerializeField] private Image List_Day1;
    [SerializeField] public Image megane_1;
    [SerializeField] public Image humanday_1;

    [Header("List_Day2")]
    [SerializeField] private Image List_Day2; 
    [SerializeField] private Sprite day2_Day2; 
    [SerializeField] public Image megane_2;
    [SerializeField] public Image humanday_2;

    [Header("List_Day3")]
    [SerializeField] private Image List_Day3; 
    [SerializeField] private Sprite day3_Day3; 
    [SerializeField] public Image megane_3;
    [SerializeField] public Image humanday_3;


    private void Start()
    {
    }

    private void OnEnable()
    {
        daysChange();
    }

    public void daysChange()
    {
        switch (currentDay)
        {
            case Days.Day0:
                if ( megane_1 != null && humanlist.Count > 0)
                {
                    int randomIndex = Random.Range(0, humanlist.Count);
                    Sprite randomSprite = humanlist[randomIndex];
                    megane_1.sprite = randomSprite;
                    humanday_1.transform.localScale = Vector3.one;

                    //删掉
                    humanlist.RemoveAt(randomIndex);
                }
                currentDay = Days.Day1;
                break;

            case Days.Day1:
                // 更改目标 UI 图像的图片
                if (Days_Image != null)
                {
                    Days_Image.sprite = Day2Image;
                }

                List_Day2.sprite = day2_Day2;

                if (megane_2 != null && humanlist.Count > 0)
                {
                    int randomIndex = Random.Range(0, humanlist.Count);
                    Sprite randomSprite = humanlist[randomIndex];
                    megane_2.sprite = randomSprite;
                    humanday_2.transform.localScale = Vector3.one;

                    humanlist.RemoveAt(randomIndex);
                }

                currentDay = Days.Day2;
                break;

            case Days.Day2:
                if (Days_Image != null)
                {
                    Days_Image.sprite = Day3Image;
                }

                // 将 humanday_1 的比例设置为 1
                if (humanday_3 != null)
                {
                    humanday_3.transform.localScale = Vector3.one;
                }

                if (megane_3 != null && humanlist.Count > 0)
                {
                    int randomIndex = Random.Range(0, humanlist.Count);
                    Sprite randomSprite = humanlist[randomIndex];
                    megane_3.sprite = randomSprite;
                    humanday_3.transform.localScale = Vector3.one;

                    humanlist.RemoveAt(randomIndex);
                }
                List_Day3.sprite = day3_Day3;

                currentDay = Days.Day3;
                break;


                case Days.Day3:
                //TODO: 结束游戏转为结束场景。
                break;

            default:
                Debug.LogError("Invalid day!");
                break;
        }

        // 禁用脚本
        enabled = false;
    }
}
