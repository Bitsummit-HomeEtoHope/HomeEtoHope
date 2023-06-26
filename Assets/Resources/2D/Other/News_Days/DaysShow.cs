using UnityEngine;

public class DaysShow : MonoBehaviour
{
    private bool isClosed = false; // 标记游戏物体是否被关闭
    private float timer = 0f; // 计时器
    private float delayTime = 1f; // 延迟关闭的时间

    private void OnMouseDown()
    {
        if (!isClosed)
        {
            Time.timeScale = 0f; // 暂停游戏
            timer = 0f;
            isClosed = true;
        }
    }

    private void Update()
    {
        if (isClosed)
        {
            timer += Time.unscaledDeltaTime; // 使用unscaledDeltaTime计时，不受Time.timeScale影响

            if (timer >= delayTime)
            {
                Time.timeScale = 1f; // 恢复游戏时间流逝
                gameObject.SetActive(false); // 关闭游戏物体
            }
            else
            {
                float scale = Mathf.Lerp(1f, 0f, timer / delayTime); // 计算比例数值
                transform.localScale = new Vector3(scale, scale, 1f); // 设置比例数值
            }
        }
    }
}
