using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public void NewsZoom(float targetScale, string targetObjectName)
    {
        StartCoroutine(ScaleObject(targetScale, targetObjectName));
    }

    private System.Collections.IEnumerator ScaleObject(float targetScale, string targetObjectName)
    {
        GameObject targetObject = GameObject.Find(targetObjectName);
        if (targetObject == null)
        {
            Debug.LogError("Cannot find target object: " + targetObjectName);
            yield break;
        }

        Transform targetTransform = targetObject.transform;
        Vector3 initialScale = targetTransform.localScale;
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, targetScale);
        float duration = 1.0f; // 缩放的总时间
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            targetTransform.localScale = Vector3.Lerp(initialScale, targetScaleVector, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终缩放为目标值
        targetTransform.localScale = targetScaleVector;
    }
}
