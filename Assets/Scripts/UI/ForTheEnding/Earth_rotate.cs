using UnityEngine;
using UnityEngine.UI;

public class Earth_rotate : MonoBehaviour
{
    public float rotationSpeed = -360f; // 每分钟逆时针旋转360度的速度

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
