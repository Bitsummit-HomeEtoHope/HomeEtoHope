using UnityEngine;
using UnityEngine.SceneManagement;

public class newsOver : MonoBehaviour
{
    private bool sceneSwitched = false; // 是否已切换场景
    private float elapsedTime = 0f; // 经过的时间


    private void Update()
    {
        // 检测鼠标左击
        if (Input.GetMouseButtonDown(0) && !sceneSwitched)
        {
            SwitchScene();
        }

        // 累加经过的时间
        elapsedTime += Time.deltaTime;

        // 等待时间超过10秒
        if (elapsedTime >= 10f && !sceneSwitched)
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        sceneSwitched = true;
        // 切换到下一个场景，这里使用了场景索引，也可以使用场景名称
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
