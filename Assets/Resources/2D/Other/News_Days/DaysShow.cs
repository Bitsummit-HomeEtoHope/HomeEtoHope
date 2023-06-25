using UnityEngine;

public class DaysShow : MonoBehaviour
{
    public float maxAngle = 30f;
    public float swingSpeed = 1f;
    public float minScale = 1f;
    public float maxScale = 1.2f;

    private float t = 0f;

    private void Update()
    {
        t += Time.deltaTime * swingSpeed;

        float angle = Mathf.Sin(t * Mathf.PI) * maxAngle;
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.Abs(Mathf.Sin(t * Mathf.PI)));

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
