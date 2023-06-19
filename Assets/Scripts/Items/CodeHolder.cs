using UnityEngine;

public class CodeHolder : MonoBehaviour
{
    public string code; // Stores the code of the object
    public string tag; // Stores the tag of the object

    private ShowList_MRYM showList; // Reference to the ShowList_MRYM script

    private void Start()
    {
        // Add the object's name to the code variable when the object is instantiated and remove "(Clone)"
        code = gameObject.name.Replace("(Clone)", "");

        // Get the object's tag
        tag = gameObject.tag;
        Debug.Log("Object Tag: " + tag);

        if (tag == "Human")
        {
            showList = FindObjectOfType<ShowList_MRYM>();
            if (showList != null)
            {
                showList.OpenList();
            }
        }
    }

    public string GetCode()
    {
        return code;
    }

    private bool isClicked = false; // State of whether the object has been clicked

    private void OnMouseDown()
    {
        isClicked = true;
        // Write code logic here to handle object click

        Debug.Log("Object Clicked");

        ItemsReading.Instance.ReceiveClickedObject(gameObject); // Send clicked object information to ItemsReading
        DisposeButtonScript.Instance.ReceiveClickedObject(gameObject); // Send clicked object information to DisposeButtonScript
        ItemsManager.Instance._isCanRotate = true; // Change _isCanRotate to true

    }

    private void Update()
    {
        if (isClicked)
        {
            // Logic after the object has been clicked
            isClicked = false;
        }
    }

    private void OnDestroy()
    {
        if (showList != null)
        {
            showList.OffList();
        }
    }
}
