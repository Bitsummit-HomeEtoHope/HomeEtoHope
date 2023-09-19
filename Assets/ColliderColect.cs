using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderColect : MonoBehaviour
{
    [SerializeField]private List<Collider> colliders = new List<Collider>();
    [SerializeField] private List<Rotate> rotateScripts = new List<Rotate>();

    private void OnEnable()
    {
        RefreshColliderList();
    }

    public void RefreshColliderList()
    {
        colliders.Clear();
        rotateScripts.Clear();

        Collider[] childColliders = GetComponentsInChildren<Collider>();
        Rotate[] rotates = FindObjectsOfType<Rotate>();


        foreach (Collider childCollider in childColliders)
        {
            colliders.Add(childCollider);
        }
        foreach(Rotate rotate in rotates)
        {
            rotateScripts.Add(rotate);
        }

    }

    private void OnTransformChildrenChanged()
    {
        RefreshColliderList();
    }

    public void DisableAllColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        foreach (Rotate rotate in rotateScripts)
        {
            rotate.enabled = false;
        }

    }
    public void EnableAllColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        foreach (Rotate rotate in rotateScripts)
        {
            rotate.enabled = true;
        }

    }
}
