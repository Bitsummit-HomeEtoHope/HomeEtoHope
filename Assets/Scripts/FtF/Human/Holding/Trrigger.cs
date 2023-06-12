using UnityEngine;

public class Trrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player进入了触发器！");
            // 在这里执行您想要的操作
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player离开了触发器！");
            // 在这里执行您想要的操作
        }
    }
}
