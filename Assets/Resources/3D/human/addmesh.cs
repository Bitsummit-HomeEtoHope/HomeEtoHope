using UnityEngine;

public class addmesh : MonoBehaviour
{
    void Start()
    {
        // 获取所有子物体的 MeshFilter 组件
        MeshFilter[] childMeshFilters = GetComponentsInChildren<MeshFilter>();

        // 创建合并后的网格
        Mesh combinedMesh = new Mesh();

        // 合并子物体的网格数据
        CombineInstance[] combineInstances = new CombineInstance[childMeshFilters.Length];
        for (int i = 0; i < childMeshFilters.Length; i++)
        {
            combineInstances[i].mesh = childMeshFilters[i].sharedMesh;
            combineInstances[i].transform = childMeshFilters[i].transform.localToWorldMatrix;
        }
        combinedMesh.CombineMeshes(combineInstances, true);

        // 创建一个新的游戏物体作为合并后的模型
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.SetParent(transform);
        combinedObject.transform.localPosition = Vector3.zero;

        // 添加合并后的网格和渲染组件
        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        // 设置合并后的网格和材质
        meshFilter.sharedMesh = combinedMesh;
        meshRenderer.sharedMaterial = childMeshFilters[0].GetComponent<Renderer>().sharedMaterial;

        // 禁用所有子物体的渲染组件
        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
    }
}
