using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // Control pause state
    [SerializeField] public List<GameObject> targetObjects; // Multiple game objects
    private Dictionary<GameObject, Vector3> initialObjectPositions;

    [Header("JoJo")]
    public Button[] buttonsToDisable; // Specify an array of buttons to disable
    private List<MeshCollider> meshColliders;
    private List<BoxCollider> boxColliders;

    public GameObject[] objectsToDisable; // Store an array of game objects to disable

    private void OnEnable()
    {
        GetCollider();
        Pause();
    }

    private void OnDisable()
    {
        Resume();
    }

    public void Pause()
    {
        isPaused = true;

        //SaveObjectStates();
        // SetTargetObjectsEnabled(true); // Stop the movement of objects and lock their Transform

        DisableButtons();

        //  DisableObjects();

        SetCollidersEnabled(false);

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Time.timeScale = 1f;

        isPaused = false;

        // SetTargetObjectsEnabled(false); // Resume the movement of objects and unlock their Transform

        EnableButtons();

        EnableObjects();

        SetCollidersEnabled(true);
    }

    private void SetTargetObjectsEnabled(bool enabled)
    {
        foreach (GameObject obj in targetObjects)
        {
            SetObjectEnabledRecursively(obj, enabled);
        }
    }

    private void SetObjectEnabledRecursively(GameObject obj, bool enabled)
    {
        obj.transform.position = enabled ? initialObjectPositions[obj] : obj.transform.position;
        obj.transform.rotation = enabled ? Quaternion.identity : obj.transform.rotation;
        obj.transform.localScale = enabled ? Vector3.one : obj.transform.localScale;
        obj.transform.GetComponent<Rigidbody>().isKinematic = enabled;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            SetObjectEnabledRecursively(child, enabled);
        }
    }

    private void SaveObjectStates()
    {
        initialObjectPositions = new Dictionary<GameObject, Vector3>();
        foreach (GameObject obj in targetObjects)
        {
            SaveObjectStateRecursively(obj);
        }
    }

    private void SaveObjectStateRecursively(GameObject obj)
    {
        initialObjectPositions[obj] = obj.transform.position;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            SaveObjectStateRecursively(child);
        }
    }

    private void GetCollider()
    {
        // Get all Mesh Collider components in the scene
        MeshCollider[] allMeshColliders = FindObjectsOfType<MeshCollider>();
        meshColliders = new List<MeshCollider>();

        foreach (MeshCollider collider in allMeshColliders)
        {
            if (collider != null && collider.gameObject != null) // Check if the collider and gameObject are not null
            {
                meshColliders.Add(collider);
            }
        }

        // Get all Box Collider components in the scene
        BoxCollider[] allBoxColliders = FindObjectsOfType<BoxCollider>();
        boxColliders = new List<BoxCollider>();

        foreach (BoxCollider collider in allBoxColliders)
        {
            if (collider != null && collider.gameObject != null) // Check if the collider and gameObject are not null
            {
                boxColliders.Add(collider);
            }
        }
    }

    private void SetCollidersEnabled(bool enabled)
    {
        // Set the enabled state of Mesh Colliders
        foreach (MeshCollider collider in meshColliders)
        {
            collider.enabled = enabled;
        }

        // Set the enabled state of Box Colliders
        foreach (BoxCollider collider in boxColliders)
        {
            collider.enabled = enabled;
        }
    }

    public void DisableButtons()
    {
        foreach (Button button in buttonsToDisable)
        {
            button.interactable = false; // Disable button interactions
        }
    }

    public void EnableButtons()
    {
        foreach (Button button in buttonsToDisable)
        {
            button.interactable = true; // Enable button interactions
        }
    }

    public void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false); // Disable game objects
        }
    }

    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true); // Enable game objects
        }
    }
}
