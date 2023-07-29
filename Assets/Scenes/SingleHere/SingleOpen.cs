using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleOpen : MonoBehaviour
{
    public Image changeImage;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(WelcomeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator WelcomeCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        yield return StartCoroutine(AdjustImageA(0.5f, 100f));
        GameObject shoutDown = GameObject.Find("Title Image Canvas");
      //  GameObject openStage = GameObject.Find("Tap to Start Canvas");
        shoutDown.SetActive(false);
      //  openStage.SetActive(true);
        yield return StartCoroutine(AdjustImageA(0.5f, 0f));
    }

    private IEnumerator AdjustImageA(float duration, float targetA)
    {
        Color startColor = changeImage.color;
        float startA = startColor.a;

        float timer = 0f;
        while (timer < duration)
        {
            float currentA = Mathf.Lerp(startA, targetA / 100f, timer / duration);
            Color color = changeImage.color;
            color = new Color(color.r, color.g, color.b, currentA);
            changeImage.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        // Set the final value to ensure accuracy
        changeImage.color = new Color(changeImage.color.r, changeImage.color.g, changeImage.color.b, targetA / 100f);
    }
}
