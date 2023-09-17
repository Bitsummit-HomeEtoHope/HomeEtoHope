using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CodeReader : MonoBehaviour
{
    private GameObject capsuleDoor;
    private Animator openDoor;

    private GameObject theEnerugi;
    private GameObject destroyItem;
    private EnerugiScript enerugiScript;
    private string _enerugiType;

    [SerializeField] private AudioSource sePlayer;
    [SerializeField] private AudioClip maruSE;
    [SerializeField] private AudioClip humanSE;
    [SerializeField] private AudioClip batsuSE;

    private GetItem getItem;

    private void Start()
    {
        getItem = FindObjectOfType<GetItem>();
    }

    private void GetTheEnerugiCunt()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
       /*  if (other.CompareTag("Tool") || other.CompareTag("Human") || other.CompareTag("Food"))
        {
            _enerugiType = other.CompareTag("Tool") ? "tool" : (other.CompareTag("Human") ? "human" : "food");
            destroyItem = other.gameObject;
            theEnerugi = GameObject.Find("enerugi_" + _enerugiType);
            enerugiScript = theEnerugi.GetComponent<EnerugiScript>();
            if (enerugiScript.powerZero)
            {
                Destroy(destroyItem);
                return;
            }
        } */

        // Get the CodeHolder component from the collided object
        CodeHolder codeHolder = other.GetComponent<CodeHolder>();
        if (codeHolder != null)
        {
            // Get the code from the CodeHolder
            string code = codeHolder.GetCode();
            Debug.Log("Received code: " + code);

            // Get the tag of the collided object
            string tag = other.gameObject.tag;

            // Get the GetItem component from the GetItemManager object
            GetItem getItemScript = GameObject.Find("GetItemManager").GetComponent<GetItem>();
            if (getItemScript != null)
            {
                // Pass the code and tag to the GetItem component
                getItemScript.ReceiveCode(code);
                getItemScript.ReceiveTag(tag);

                // Perform specific actions if the tag is "Human"
                if (tag == "Human")
                {
                    capsuleDoor = GameObject.Find("humanDoor");
                    openDoor = capsuleDoor.GetComponent<Animator>();

                    if (capsuleDoor != null)
                    {
                        // Disable and enable the capsuleDoor object to trigger its animation
                        capsuleDoor.SetActive(false);
                        capsuleDoor.SetActive(true);

                        if (openDoor != null)
                        {
                            // Disable and enable the openDoor animator to restart its animation
                            openDoor.enabled = false;
                            openDoor.enabled = true;
                        }
                    }
                }

                bool codeMatches = false;

                if (getItem.theirList != null)
                {
                    // Check if the code contains any feature from the theirList
                    foreach (string feature in getItem.theirList)
                    {
                        if (code.Contains(feature))
                        {
                            codeMatches = true;
                            break;
                        }
                    }
                }

                // Play sound effects based on the code and codeMatches
                if (code.Contains("broken") || code.Contains("hi") || code.Contains("qaq"))
                {
                    Debug.Log("22222222222222222Code is right!!222222222222222222");
                    if (batsuSE != null) sePlayer.GetComponent<AudioSource>().PlayOneShot(batsuSE);
                }
                else if (codeMatches)
                {
                    if (humanSE != null) sePlayer.GetComponent<AudioSource>().PlayOneShot(humanSE);
                }
                else
                {
                    if (maruSE != null) sePlayer.GetComponent<AudioSource>().PlayOneShot(maruSE);
                }
            }
            else
            {
                Debug.Log("GetItem script not found!");
            }
        }
        else
        {
            Debug.Log("CodeHolder component not found!");
        }

        // Destroy the collided object
        Destroy(other.gameObject);
    }
}
