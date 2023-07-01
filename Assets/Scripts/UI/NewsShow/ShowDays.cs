using UnityEngine;
using UnityEngine.UI;

public class ShowDays : MonoBehaviour
{
    public Image image;
    public bool startOnAwake = true;
    public bool scaleEnabled = false;
    public float scaleTime = 1.0f;
    public Vector3 targetScale = Vector3.one;
    public bool oscillateEnabled = false;
    public float oscillationAmount = 0.1f;
    public int oscillationCount = 5;
    public bool moveEnabled = false;
    public Vector3 targetPosition = Vector3.zero;
    public float moveTime = 1.0f;
    public bool switchSpriteOnComplete = false;
    public Sprite targetSprite;
    public bool closeOnComplete = false;
    public bool enableOtherImageObject = false;
    public GameObject otherImageObject;
    public bool enableOtherScript = false;
    public MonoBehaviour otherScript;

    private bool isRunning = false;

    private void Awake()
    {
        if (startOnAwake)
        {
            StartAnimation();
        }
    }

    public void StartAnimation()
    {
        if (isRunning)
        {
            return;
        }

        isRunning = true;

        if (scaleEnabled)
        {
            StartCoroutine(ScaleAnimation());
        }

        if (oscillateEnabled)
        {
            StartCoroutine(OscillateAnimation());
        }

        if (moveEnabled)
        {
            StartCoroutine(MoveAnimation());
        }
    }

    private System.Collections.IEnumerator ScaleAnimation()
    {
        Vector3 initialScale = image.transform.localScale;
        Vector3 targetScale = this.targetScale;

        float elapsedTime = 0.0f;
        while (elapsedTime < scaleTime)
        {
            float t = elapsedTime / scaleTime;
            image.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.transform.localScale = targetScale;

        if (switchSpriteOnComplete && targetSprite != null)
        {
            image.sprite = targetSprite;
        }

        if (closeOnComplete)
        {
            gameObject.SetActive(false);
        }

        isRunning = false;
    }

    private System.Collections.IEnumerator OscillateAnimation()
    {
        Vector3 initialPosition = image.transform.position;
        float halfAmount = oscillationAmount / 2.0f;

        for (int i = 0; i < oscillationCount; i++)
        {
            float elapsedTime = 0.0f;
            Vector3 targetPosition = initialPosition + Vector3.right * halfAmount;

            while (elapsedTime < moveTime)
            {
                float t = elapsedTime / moveTime;
                image.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0.0f;
            targetPosition = initialPosition - Vector3.right * halfAmount;

            while (elapsedTime < moveTime)
            {
                float t = elapsedTime / moveTime;
                image.transform.position = Vector3.Lerp(targetPosition, initialPosition, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        image.transform.position = initialPosition;

        if (switchSpriteOnComplete && targetSprite != null)
        {
            image.sprite = targetSprite;
        }

        if (closeOnComplete)
        {
            gameObject.SetActive(false);
        }

        isRunning = false;
    }

    private System.Collections.IEnumerator MoveAnimation()
    {
        Vector3 initialPosition = image.transform.position;
        Vector3 targetPosition = this.targetPosition;

        float elapsedTime = 0.0f;
        while (elapsedTime < moveTime)
        {
            float t = elapsedTime / moveTime;
            image.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.transform.position = targetPosition;

        if (switchSpriteOnComplete && targetSprite != null)
        {
            image.sprite = targetSprite;
        }

        if (closeOnComplete)
        {
            gameObject.SetActive(false);
        }

        isRunning = false;
    }

    // 在适当的地方调用以下方法来触发其他图像对象和脚本的启用
    private void EnableOtherImageObject()
    {
        if (otherImageObject != null)
        {
            otherImageObject.SetActive(true);
        }
    }

    private void EnableOtherScript()
    {
        if (otherScript != null)
        {
            otherScript.enabled = true;
        }
    }
}
