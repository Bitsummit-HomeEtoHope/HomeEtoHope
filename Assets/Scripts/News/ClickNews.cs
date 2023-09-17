using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickNews : MonoBehaviour
{
    public void MyButtonEvent()
    {
        NewsManager newsManager = FindObjectOfType<NewsManager>();
        if (newsManager != null)
        {
//            newsManager.enabled = false;
            newsManager.NewsClick();

        }
        else
        {
            Debug.LogWarning("NewsManager component not found in the scene!");
        }
    }
}
