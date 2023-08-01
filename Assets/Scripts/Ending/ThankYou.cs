using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThankYou : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] public GameObject fin;
    [SerializeField] public GameObject curtain;
    [SerializeField] public GameObject Rest;
    [SerializeField] public GameObject theSkip;
    private GameObject _button;


    private Image curtainX;
    private Image finX;
    public bool bgm = true;

    private bool isMeto = true;
    private Coroutine theFIN;

    // Start is called before the first frame update
    void Start()
    {
        _button = GameObject.Find("Button");


        audioSource = GetComponent<AudioSource>();
        curtainX = curtain.GetComponent<Image>();
        finX = fin.GetComponent<Image>();

        theFIN = StartCoroutine(CurtainFalls(8f));


        if (isMeto)
        {
            if (_button != null)
            {
                if (_button.activeSelf)
                {
                    _button.SetActive(false);
                }
            }
        }


    }

    public void Skip()
    {
    
            StopCoroutine(theFIN);
            if (bgm)
            {
                StartCoroutine(CurtainFalls(0.1f));
            }
        
    }


    private IEnumerator CurtainFalls(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bgm)
        {
            if (!isMeto)
            {
                if (_button.activeSelf) _button.SetActive(false);
            }

            if (theSkip.activeSelf)theSkip.SetActive(false);
            Rest.SetActive(true);
            curtain.active=true;
            fin.active=true;
            audioSource.Play();
            bgm = false;
        }


        yield return StartCoroutine(AdjustImageV(1f, 100f));
        yield return StartCoroutine(AdjustImageA(1f, 0f));


    }


    private IEnumerator AdjustImageV(float duration, float targetV)
    {
        Color startColor = curtainX.color;
        float startV = 0f;
        Color.RGBToHSV(startColor, out startV, out _, out _);

        float timer = 0f;
        while (timer < duration)
        {
            float currentV = Mathf.Lerp(startV, targetV / 100f, timer / duration);
            Color color = curtainX.color;
            Color.RGBToHSV(color, out _, out float currentS, out float currentH);
            color = Color.HSVToRGB(currentH, currentS, currentV);
            curtainX.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        // Set the final value to ensure accuracy
        Color finalColor = curtainX.color;
        Color.RGBToHSV(finalColor, out _, out float finalS, out float finalH);
        finalColor = Color.HSVToRGB(finalH, finalS, targetV / 100f);
        curtainX.color = finalColor;
    }


    private IEnumerator AdjustImageA(float duration, float targetA)
    {
        Color startColor = curtainX.color;
        float startA = startColor.a;

        float timer = 0f;
        while (timer < duration)
        {
            float currentA = Mathf.Lerp(startA, targetA, timer / duration);
            Color color = curtainX.color;
            color = new Color(color.r, color.g, color.b, currentA);
            curtainX.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        // Set the final value to ensure accuracy
        curtainX.color = new Color(curtainX.color.r, curtainX.color.g, curtainX.color.b, targetA);
    }


}

/*-------RGB-------
 协程套协程，就是花zuo哨si
        
        private IEnumerator CurtainFalls(float delay)
    {
        yield return new WaitForSeconds(delay);

        StartCoroutine(CurtainFalls(8f));

        yield return StartCoroutine(AdjustImageV(1f, 100f));
        yield return StartCoroutine(AdjustImageA(1f, 0f));


    }

    


    private IEnumerator AdjustImageV(float duration, float targetV)
    {
        float startV = curtainX.color.maxColorComponent;

        float timer = 0f;
        while (timer < duration)
        {
            float currentV = Mathf.Lerp(startV, targetV, timer / duration);
            Color color = curtainX.color;
            color = Color.HSVToRGB(color.r, color.g, currentV / 100f);
            curtainX.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        // Set the final value to ensure accuracy
        curtainX.color = Color.HSVToRGB(curtainX.color.r, curtainX.color.g, targetV / 100f);
    }

    private IEnumerator AdjustImageA(float duration, float targetA)
    {
        float startA = curtainX.color.a;

        float timer = 0f;
        while (timer < duration)
        {
            float currentA = Mathf.Lerp(startA, targetA, timer / duration);
            Color color = curtainX.color;
            color = new Color(color.r, color.g, color.b, currentA);
            curtainX.color = color;

            timer += Time.deltaTime;
            yield return null;
        }

        // Set the final value to ensure accuracy
        curtainX.color = new Color(curtainX.color.r, curtainX.color.g, curtainX .color.b, targetA);
 
 */