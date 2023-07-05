using UnityEngine;

public class ResetTime : MonoBehaviour
{
    private float originalTimeScale;
    private Rigidbody[] childRigidbodies;

    private void Start()
    {
        // 获取游戏物体及其子物体上的所有刚体组件
        childRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    private void Update()
    {
        Resume();
    }

    public void Pause()
    {
        // 保存原始的时间尺度值
        originalTimeScale = Time.timeScale;
        // 设置时间尺度为0，暂停游戏物体及其子物体
        Time.timeScale = 0f;

        // 如果有刚体组件，设置它们的时间尺度为0，停止它们的运动
        if (childRigidbodies != null)
        {
            foreach (var rb in childRigidbodies)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    public void Resume()
    {
        // 恢复原始的时间尺度值
        Time.timeScale = 1f;

        // 如果有刚体组件，设置它们的时间尺度为1，恢复它们的运动
        if (childRigidbodies != null)
        {
            foreach (var rb in childRigidbodies)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
