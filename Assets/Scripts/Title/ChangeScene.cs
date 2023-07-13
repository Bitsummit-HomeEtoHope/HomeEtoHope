using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    [Header("System")]
    public Button playButton;
    public Button exitButton;
    public Button howButton;
    public Button listButton;

    void Awake()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null) IN();
    }

    public void PlayGame()

    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null) OUT();

        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null) OUT();

        EndGame();
        // 或者：SceneManager.LoadScene(nextSceneIndex);
    }


    public void HowToPlay()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null) OUT();

        SceneManager.LoadScene("TellYou");
    }


    public void OurList()
    {
        if (playButton != null && exitButton != null && howButton != null && listButton != null) OUT();

        SceneManager.LoadScene("OurList");
    }




    void EndGame()
    {
        // 在编辑器中停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在游戏中退出应用程序
        QuitGame();
#endif
    }

    void QuitGame()
    {
        // 退出应用程序
        Application.Quit();
    }


    private void IN()
    {
        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        howButton.onClick.AddListener(HowToPlay);
        listButton.onClick.AddListener(OurList);
    }
    private void OUT()
    {
        playButton.onClick.RemoveListener(PlayGame);
        exitButton.onClick.RemoveListener(ExitGame);
        howButton.onClick.RemoveListener(HowToPlay);
        listButton.onClick.RemoveListener(OurList);
    }

}

