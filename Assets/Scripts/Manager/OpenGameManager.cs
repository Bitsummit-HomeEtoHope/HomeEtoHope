using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class OpenGameManager : MonoBehaviour
{
    public Button Single;
    public Button BeFired;
    // Start is called before the first frame update
    void Start()
    {
        if (Single != null) Single.onClick.AddListener(() => selectScreen("Title_single"));
        if (BeFired != null) BeFired.onClick.AddListener(() => selectScreen("Title"));
    }

    private void selectScreen(string select)
    {
        SceneManager.LoadScene(select);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
