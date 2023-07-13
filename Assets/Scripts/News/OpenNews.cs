using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class OpenNews : MonoBehaviour
{
    public GameObject newsManager;

    public GameObject hereIsStart;
    public GameObject hereIsBack;
    public GameObject hereIsNews;
    public GameObject hereIsLight;
    public GameObject hereIsPeople;
    public GameObject hereIsShady;
    public Image hereIsUp;
    public GameObject hereIsManager;
    public GameObject hereIsYourself;
    public Image hereIsBar;
    public Image hereIsBord;
    public float opentime = 0.8f;
    public float filltime = 0.3f;
    public float movetime = 0.3f;

    private NewsManager newsScript;
    private Vector3 initialLightScale;
    private Vector3 initialNewsScale;
    private Vector3 initialPeopleScale;
    private Vector3 initialPeoplePos;

    private void Awake()
    {
        newsScript = newsManager.GetComponent<NewsManager>();
    }
    private void OnEnable()
    {
        hereIsUp.fillAmount = 0f;

        if (hereIsShady != null) if (!hereIsShady.activeSelf) hereIsShady.active = true;

        newsScript.takeThemOff();
        if (hereIsBack != null && !hereIsBack.activeSelf)
        {
            hereIsBack.SetActive(true);
        }

        StartCoroutine(OpenNewsCoroutine());
    }

    private void OnDisable()
    {
        //hereIsGone();
        //newsScript.readyone=false;
        if (hereIsBack != null && hereIsBack.activeSelf)
        {
            hereIsBack.SetActive(false);
        }
        ResetTransforms();
    }

    private IEnumerator OpenNewsCoroutine()
    {
        initialLightScale = hereIsLight.transform.localScale;
        initialNewsScale = hereIsNews.transform.localScale;
        initialPeopleScale = hereIsPeople.transform.localScale;
        initialPeoplePos = hereIsPeople.transform.position;

        //yield return StartCoroutine(hereIsOpen(hereIsLight, Vector3.one));

        yield return new WaitForSeconds(1.4f);
        if (hereIsStart != null) hereIsStart.active = false;
        StartCoroutine(hereIsOpen(hereIsNews, Vector3.one));
        StartCoroutine(hereIsFill(hereIsBar));

        StartCoroutine(hereIsFill(hereIsBord));
        StartCoroutine(hereIsFill(hereIsUp));

        yield return StartCoroutine(hereIsMove(hereIsPeople.transform));

        yield return new WaitForSeconds(0.3f);

        hereIsReady();
    }

    private void hereIsReady()
    {
        if(hereIsManager!=null)hereIsManager.active = true;
        hereIsGone();
    }

    private void hereIsGone()
    {
        if (hereIsYourself != null) hereIsYourself.active = false;
    }

    private IEnumerator hereIsOpen(GameObject hereis, Vector3 scale)
    {
        float timer = 0f;
        Vector3 initialScale = hereis.transform.localScale;

        while (timer < opentime)
        {
            timer += Time.deltaTime;
            float t = timer / opentime;
            hereis.transform.localScale = Vector3.Lerp(initialScale, scale, t);
            yield return null;
        }

        hereis.transform.localScale = scale;
    }

    private IEnumerator hereIsFill(Image hereis)
    {
        float timer = 0f;
        float initialFillAmount = hereis.fillAmount;
        float targetFillAmount = 1f;

        while (timer < filltime)
        {
            timer += Time.deltaTime;
            float t = timer / filltime;
            hereis.fillAmount = Mathf.Lerp(initialFillAmount, targetFillAmount, t);
            yield return null;
        }

        hereis.fillAmount = targetFillAmount;
    }



    private IEnumerator hereIsMove(Transform hereis)
    {
        float timer = 0f;
        float initialX = hereis.position.x;
        float targetX = -7f; // 设置目标x值

        while (timer < movetime)
        {
            timer += Time.deltaTime;
            float t = timer / movetime;
            float newX = Mathf.Lerp(initialX, targetX, t);

            Vector3 newPosition = hereis.position;
            newPosition.x = newX;
            hereis.position = newPosition;

            yield return null;
        }

        Vector3 finalPosition = hereis.position;
        finalPosition.x = targetX;
        hereis.position = finalPosition;
    }


    private void ResetTransforms()
    {
        if (hereIsStart != null) hereIsStart.active = true;
        hereIsLight.transform.localScale = initialLightScale;
        hereIsNews.transform.localScale = initialNewsScale;
        hereIsPeople.transform.localScale = initialPeopleScale;
        hereIsPeople.transform.position = initialPeoplePos;
        hereIsBar.fillAmount = 0f;
        hereIsBord.fillAmount = 0f;
    }
}
