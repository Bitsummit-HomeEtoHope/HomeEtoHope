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

        canvas.sortingOrder = int.MaxValue; // ����Canvas��sortingOrderΪ���ֵ
    }

    private void LateUpdate()
    {
        // ���Canvas��sortingOrder�Ƿ���ȻΪ���ֵ
        if (canvas.sortingOrder != int.MaxValue)
        {
            canvas.sortingOrder = int.MaxValue;
        }
    }
}
