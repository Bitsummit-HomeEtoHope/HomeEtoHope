using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ITellYou : MonoBehaviour
{

    [Header("Eyes")]
    [SerializeField] public Image eyes;
    [SerializeField] public List<Sprite> eyeslist;

    [Header("Hand")]
    [SerializeField] public GameObject hand;
    [SerializeField] public float handHight = 5f;
    [SerializeField] public float handTime = 0.2f;

    [Header("Body")]
    [SerializeField] public GameObject body;

    [Header("Buble")]
    [SerializeField] public GameObject buble;
    [SerializeField] public float bubleTime = 0.3f;

    [Header("How")]
    [SerializeField] public Image How;
    [SerializeField] public List<Sprite> howlist;
    private int currentSpriteIndex = 0;

    [Header("System")]
    public Button nextButton; 
    public Button backButton; 
    public Button startButton; 
    // Start is called before the first frame update
    void OnEnable()
    {
        nextButton.onClick.AddListener(NextButtonAction);

        backButton.onClick.AddListener(BackButtonAction);

        startButton.onClick.AddListener(StartButtonAction);

        YaYa();
        StartCoroutine(DaDa());
        StartCoroutine(FaFa());
        KaKa();
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(NextButtonAction);

        backButton.onClick.RemoveListener(BackButtonAction);

        startButton.onClick.RemoveListener(StartButtonAction);

        enabled = true;
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
          //  enabled = false;


        }
    }



    public void YaYa()
    {
        if (eyeslist.Count > 0)
        {
            int randomIndex = Random.Range(0, eyeslist.Count);

            Sprite randomSprite = eyeslist[randomIndex];

            eyes.sprite = randomSprite;
        }
        else
        {
            Debug.LogError("Eyeslist is empty! Please assign sprites to the list.");
        }
    }

    private IEnumerator DaDa()
    {
        Vector3 startPosition = hand.transform.position;


        for (int i = 0; i < 2; i++)
        {
            Vector3 targetPosition = startPosition + new Vector3(0, handHight, 0);

            float elapsedTime = 0;
            while (elapsedTime < handTime)
            {
                hand.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / handTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            targetPosition = startPosition - new Vector3(0, handHight, 0);

            elapsedTime = 0;
            while (elapsedTime < handTime)
            {
                hand.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / handTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        hand.transform.position = startPosition;
    }


    private IEnumerator FaFa()
    {
        buble.transform.localScale = Vector3.zero;

        Vector3 initialScale = buble.transform.localScale;
        Vector3 targetScale = Vector3.one;

        float elapsedTime = 0f;

        while (elapsedTime < bubleTime)
        {
            float t = elapsedTime / bubleTime;
            buble.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        buble.transform.localScale = targetScale;
    }


    public void KaKa()
    {
        if (howlist.Count > 0)
        {
            Sprite currentSprite = howlist[currentSpriteIndex];

            How.sprite = currentSprite;

            currentSpriteIndex++;
            if (currentSpriteIndex >= howlist.Count)
            {
                currentSpriteIndex = 0; 
            }
        }
        else
        {
            Debug.LogError("Howlist is empty! Please assign sprites to the list.");
        }
    }

    public void NextButtonAction()
    {
        enabled = false;
    }

    public void BackButtonAction()
    {

        SceneManager.LoadScene("Title");
    }

    public void StartButtonAction()
    {

        SceneManager.LoadScene("Level1");
    }
}
