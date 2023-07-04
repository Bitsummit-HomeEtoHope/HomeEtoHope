using UnityEngine;

public class TreesGrou : MonoBehaviour
{
    public GameObject newPrefab; // ָ������Ԥ�Ƽ�
    public float delay = 10f; // �ӳ�ʱ��
    public LevelDataCurrent levelDataCurrent;

    private float timer;
    private bool prefabChanged;

    private void Start()
    {
        levelDataCurrent=GameObject.Find("LevelData").GetComponent<LevelDataCurrent>();
        timer = 0f;
        prefabChanged = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (!prefabChanged && timer >= levelDataCurrent._future_Data.tree_Time)
        {
            ChangePrefab();
        }
    }

    private void ChangePrefab()
    {
        GameObject newPrefabInstance = Instantiate(newPrefab, transform.position, transform.rotation);
        newPrefabInstance.transform.SetParent(transform.parent); // ����Ԥ�Ƽ�����Ϊ���ؽű�����ĸ�����

        Destroy(gameObject); // ���ٵ�ǰ��Ϸ����

        prefabChanged = true;
    }
}
