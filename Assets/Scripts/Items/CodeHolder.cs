using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CodeHolder : MonoBehaviour
{
    public string code; // Stores the code of the object
    public string tag; // Stores the tag of the object

    public Material defaultMaterial; // Default material
    public Material highlightMaterial; // Highlight material
    private MeshRenderer meshRenderer; // MeshRenderer component of the object

    public bool ihaveChild = false;

    [System.Serializable]
    public struct ChildRenderers
    {
        public MeshRenderer childRenderer; // MeshRenderer component of the child object
        public Material childDefaultMaterial; // Default material of the child object
        public Material childHighlightMaterial; // Highlight material of the child object
    }
    public List<ChildRenderers> childRenderers = new List<ChildRenderers>();

    private static CodeHolder selectedObject;

    private ShowList_MRYM showList; // Reference to the ShowList_MRYM script

    private void Start()
    {
        code = gameObject.name.Replace("(Clone)", "");
        tag = gameObject.tag;
        Debug.Log("Object Tag: " + tag);

        meshRenderer = GetComponent<MeshRenderer>();
    }

    public string GetCode()
    {
        return code;
    }

    private bool isClicked = false; // Records if the object has been clicked

    private void OnMouseDown()
    {
        if (selectedObject != null)
        {
            if (selectedObject.ihaveChild)
            {
                foreach (ChildRenderers childRenderer in selectedObject.childRenderers)
                {
                    childRenderer.childRenderer.material = childRenderer.childDefaultMaterial;
                }
            }
            else
            {
                // Deselect the previously selected object
                selectedObject.meshRenderer.material = selectedObject.defaultMaterial;
            }
        }
        if (selectedObject != this)
            GetComponent<AudioSource>().Play();
        selectedObject = this;
        if (selectedObject = this)
        {
            if (tag == "Human")
            {
                showList = FindObjectOfType<ShowList_MRYM>();
                if (showList != null && !showList.listing)
                {
                    //
                    showList.listing = true;
                    showList.OpenList();
                }
            }
            else
            {
                showList = FindObjectOfType<ShowList_MRYM>();
                if (showList != null && showList.listing)
                {
                    //
                    showList.listing = false;
                    showList.OffList();
                }
            }
        }
        isClicked = true;

        if (ihaveChild)
        {
            foreach (ChildRenderers childRenderer in childRenderers)
            {
                childRenderer.childRenderer.material = childRenderer.childHighlightMaterial;
            }
        }
        else
        {
            // Change the material of the current object to the highlight material
            meshRenderer.material = highlightMaterial;
        }

        Debug.Log("Object Clicked");

        ItemsReading.Instance.ReceiveClickedObject(gameObject); // Send information about the clicked object to ItemsReading
        DisposeButtonScript.Instance.ReceiveClickedObject(gameObject); // Send information about the clicked object to DisposeButtonScript
        ItemsManager.Instance._isCanRotate = true; // Change _isCanRotate to true
    }

    private void Update()
    {
        if (isClicked)
        {
            isClicked = false;
        }
    }

    private void OnDestroy()
    {
        if (selectedObject == this)
        {
            selectedObject = null;
        }
    }
}
