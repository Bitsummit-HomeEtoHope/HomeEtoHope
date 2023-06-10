using UnityEngine;

public class HumanGaTei : MonoBehaviour
{
    private void Start()
    {
        MergeMeshes();
    }

    private void MergeMeshes()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            Material sharedMaterial = meshRenderer.sharedMaterial;
            // �����ϲ�����Ĵ���...
        }
        else
        {
            Debug.LogError("Missing MeshRenderer component on the 'human' game object.");
        }
    }
}
