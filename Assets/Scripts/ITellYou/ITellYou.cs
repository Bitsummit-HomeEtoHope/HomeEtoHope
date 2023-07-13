using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ITellYou : MonoBehaviour
{

    [Header("Eyes")]
    [SerializeField] public Image eyes;
    [SerializeField] public List<Sprite> eyeslist;
    
    [Header("Hand")]
    [SerializeField] public GameObject hand;
    [SerializeField]public float handHight = 5f;
    [SerializeField]public float handTime = 0.2f;

    [Header("Body")]
    [SerializeField] public GameObject body;
    
    [Header("Buble")]
    [SerializeField] public GameObject buble;
    
    [Header("How")]
    [SerializeField] public Image How;
    [SerializeField] public List<Sprite> howlist;

    // Start is called before the first frame update
    void OnEnable()
    {
        YaYa();
        StartCoroutine(DaDa());
    }

    // Update is called once per frame
    void Update()
    {
        
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


}
