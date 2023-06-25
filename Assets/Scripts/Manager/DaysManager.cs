using UnityEngine;
using UnityEngine.UI;

public class DaysManager : MonoBehaviour
{
    public enum Days { Day1, Day2, Day3 }

    [SerializeField] private Days currentDay = Days.Day1;

    [SerializeField] private Image Days_Image; // �ο�Ŀ�� UI ͼ��� Image ���
    [SerializeField] private Sprite Day2Image; // day1�滻����
    [SerializeField] private Sprite Day3Image; // day2�滻����


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
                // ����Ŀ�� UI ͼ���ͼƬ
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

        // ���ýű�
        enabled = false;
    }
}
