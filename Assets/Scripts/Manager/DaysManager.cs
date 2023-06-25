using UnityEngine;
using UnityEngine.UI;

public class DaysManager : MonoBehaviour
{
    public enum Days { Day1, Day2, Day3 }

    [SerializeField] private Days currentDay = Days.Day1;

    [SerializeField] private Image Days_Image; // 参考目标 UI 图像的 Image 组件
    [SerializeField] private Sprite Day2Image; // day1替换对象
    [SerializeField] private Sprite Day3Image; // day2替换对象


    private void OnEnable()
    {
        daysChange();
    }

    public void daysChange()
    {
        switch (currentDay)
        {
            case Days.Day1:
                Debug.Log("Day1 functionality executed!");
                // 更改目标 UI 图像的图片
                Days_Image.sprite = Day2Image;

                currentDay = Days.Day2;
                break;

            case Days.Day2:
                Debug.Log("Day2 functionality executed!");

                Days_Image.sprite = Day3Image;

                currentDay = Days.Day3;
                break;

            case Days.Day3:


                Debug.Log("Day3 functionality executed!");

                break;

            default:
                Debug.LogError("Invalid day!");
                break;
        }

        // 禁用脚本
        enabled = false;
    }
}
