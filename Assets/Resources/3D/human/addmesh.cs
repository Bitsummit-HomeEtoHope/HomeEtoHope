using UnityEngine;

public class addmesh : MonoBehaviour
{
    void Start()
    {
        // ��ȡ����������� MeshFilter ���
        MeshFilter[] childMeshFilters = GetComponentsInChildren<MeshFilter>();

        // �����ϲ��������
        Mesh combinedMesh = new Mesh();

        // �ϲ����������������
        CombineInstance[] combineInstances = new CombineInstance[childMeshFilters.Length];
        for (int i = 0; i < childMeshFilters.Length; i++)
        {
            combineInstances[i].mesh = childMeshFilters[i].sharedMesh;
            combineInstances[i].transform = childMeshFilters[i].transform.localToWorldMatrix;
        }
        combinedMesh.CombineMeshes(combineInstances, true);

        // ����һ���µ���Ϸ������Ϊ�ϲ����ģ��
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.SetParent(transform);
        combinedObject.transform.localPosition = Vector3.zero;

        // ��Ӻϲ�����������Ⱦ���
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // ���úϲ��������Ͳ���
        meshFilter.sharedMesh = combinedMesh;
        meshRenderer.sharedMaterial = childMeshFilters[0].GetComponent<Renderer>().sharedMaterial;

        // �����������������Ⱦ���
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
    }
}
