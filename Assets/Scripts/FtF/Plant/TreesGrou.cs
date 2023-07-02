using UnityEngine;

public class TreesGrou : MonoBehaviour
{
    public GameObject newPrefab; // 指定的新预制件
    public float delay = 10f; // 延迟时间

    private float timer;
    private bool prefabChanged;

    private void Start()
    {
        timer = 0f;
        prefabChanged = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!prefabChanged && timer >= delay)
        {
            ChangePrefab();
        }
    }

    private void ChangePrefab()
    {
        GameObject newPrefabInstance = Instantiate(newPrefab, transform.position, transform.rotation);
        newPrefabInstance.transform.SetParent(transform.parent); // 将新预制件设置为挂载脚本对象的父对象

        Destroy(gameObject); // 销毁当前游戏对象

        prefabChanged = true;
    }
}
