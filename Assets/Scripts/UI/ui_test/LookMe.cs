using UnityEngine;

public class LookMe : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("AlwaysOnTop script requires a Canvas component!");
            return;
        }

        canvas.sortingOrder = int.MaxValue; // 设置Canvas的sortingOrder为最大值
    }

    private void LateUpdate()
    {
        // 检查Canvas的sortingOrder是否仍然为最大值
        if (canvas.sortingOrder != int.MaxValue)
        {
            canvas.sortingOrder = int.MaxValue;
        }
    }
}
