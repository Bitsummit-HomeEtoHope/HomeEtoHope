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
    public bool two = false;

    [System.Serializable]
    public struct ChildRenderers
    {
        public MeshRenderer childRenderer; // MeshRenderer component of the child object
        public Material childDefaultMaterial; // Default material of the child object
        public Material childHighlightMaterial; // Highlight material of the child object
    }
    public List<ChildRenderers> childRenderers = new List<ChildRenderers>();

    public static CodeHolder selectedObject;

    private ShowList_MRYM showList; // Reference to the ShowList_MRYM script
    private ItemSet itemSet;

    [SerializeField] private GameObject singleVer;


    private void Start()
    {

        singleVer = GameObject.Find("Play_single");

        code = gameObject.name.Replace("(Clone)", "");
        tag = gameObject.tag;
        Debug.Log("Object Tag: " + tag);

        meshRenderer = GetComponent<MeshRenderer>();
        itemSet = GetComponent<ItemSet>();
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
                //selectedObject.meshRenderer.material = selectedObject.defaultMaterial;

                // Deselect the previously selected object
                if (selectedObject.two)
                {
                    Material[] materials = selectedObject.meshRenderer.materials;
                    materials[1] = selectedObject.defaultMaterial;
                    selectedObject.meshRenderer.materials = materials;
                }
                else
                {

                    selectedObject.meshRenderer.material = selectedObject.defaultMaterial;
                }

            }
        }
        if (selectedObject != this)
            GetComponent<AudioSource>().Play();
        selectedObject = this;
        if (selectedObject = this)
        {
            if (singleVer == null)
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
                        showList.turnOff();
                    }
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
            if (two)
            {
                Material[] materials = selectedObject.meshRenderer.materials;
                materials[1] = selectedObject.highlightMaterial;
                selectedObject.meshRenderer.materials = materials;
            }
            else meshRenderer.material = highlightMaterial;

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
            itemSet.Clear();
        }
    }
}
