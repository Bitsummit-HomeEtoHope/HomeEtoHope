using System.Collections.Generic;
using UnityEngine;

public class TreesGrou : MonoBehaviour
{
    public GameObject newPrefab; // ָ������Ԥ�Ƽ�
    public float delay = 10f; // �ӳ�ʱ��
    public LevelDataCurrent levelDataCurrent;
    public List<Transform> childrens = new List<Transform>();
    [SerializeField]
    private GameObject _food;

    private float timer;
    private bool prefabChanged;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            childrens.Add(child);
        }
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
        

       foreach (Transform child in childrens)
        {
            //���ɵ���������Ϊchild��������
            
            GameObject newObject = Instantiate(_food, child.position, child.rotation,child); // ʵ������Ԥ�Ƽ�
            newObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.5f);
            GameManager.Instance.treeFoodList.Add(newObject);
        }

        
        //Destroy(gameObject); // ���ٵ�ǰ��Ϸ����

        prefabChanged = true;
    }
}
