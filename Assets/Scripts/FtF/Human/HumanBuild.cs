using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoToBuild
{
    Standby,
    Building,
    Company,
    Farm
}

public class HumanBuild : MonoBehaviour
{
    public float moveSpeed = 5f; // �ƶ��ٶ�
    private bool isBuilding = false; // �Ƿ����ڽ���
    private float buildTimer = 0f; // ������ʱ��
    public float buildTime = 3f; // ����ʱ��
    public float yOffset = 0.0f; // Y��ƫ����

    private Renderer characterRenderer; // ��ɫ����Ⱦ�����
    private int defaultSortingOrder; // ��ɫĬ�ϵ�����˳��

    private List<Vector3> buildPositions; // ����λ���б�
    private int currentBuildIndex = 0; // ��ǰ����λ�õ�����
    public GameObject[] replacementPrefabs;

    private GameObject currentBuildSetObject; // ��ǰ���ڽӴ��� Build_set Ԥ�Ƽ�

    public GoToBuild currentBuildType = GoToBuild.Standby; // ��ǰ�趨�Ľ�������

    private bool isReadyToGo = false;

    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = GetComponent<Renderer>(); // ��ȡ��ɫ����Ⱦ�����
        defaultSortingOrder = characterRenderer.sortingOrder; // ��ȡ��ɫĬ�ϵ�����˳��
        ReadyToGo();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {
            buildTimer += Time.deltaTime; // ���¼�ʱ��

            if (buildTimer >= buildTime)
            {
                isBuilding = false; // ��ɽ�����ֹͣ��ʱ
                characterRenderer.sortingOrder = defaultSortingOrder; // �ָ���ɫ������˳��

                BuildingReady(currentBuildSetObject); // ���ĵ�ǰ�Ӵ���Ԥ�Ƽ�
                Debug.Log("�������");

                // �ƶ�����һ������λ��
                currentBuildIndex++;
                if (currentBuildIndex >= buildPositions.Count)
                {
                    currentBuildIndex = 0; // ���¿�ʼѭ������λ��
                }

                // ����׼���ƶ��ı�־
                isReadyToGo = true;
            }
        }

        // ���׼���ƶ��ı�־Ϊ true������� ReadyToGo ����
        if (isReadyToGo)
        {
            isReadyToGo = false; // ���ñ�־
            ReadyToGo();
        }
    }

    private void ReadyToGo()
    {
        // ֹͣ��ǰ�ƶ���Э��
        StopAllCoroutines();

        // ���ҳ������д���"Build_set"��ǩ�����壬����¼���ǵ�λ��
        buildPositions = new List<Vector3>();
        GameObject[] buildSetObjects = GameObject.FindGameObjectsWithTag("Build_set");
        foreach (GameObject buildSet in buildSetObjects)
        {
            Vector3 position = buildSet.transform.position;
            position.y += yOffset; // ���Y��ƫ����
            buildPositions.Add(position);
        }

        // ���ݹ���Խ���λ�ý�������
        buildPositions.Sort(SortBuildPositions);

        // �ƶ�����һ������λ��
        MoveToBuildPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Build_set"))
        {
            isBuilding = true; // ���뽨�����򣬿�ʼ����
            buildTimer = 0f; // ���ü�ʱ��

            Renderer buildRenderer = other.GetComponent<Renderer>(); // ��ȡBuild_set�������Ⱦ�����
            if (buildRenderer != null)
            {
                int buildSortingOrder = buildRenderer.sortingOrder; // ��ȡBuild_set���������˳��
                characterRenderer.sortingOrder = buildSortingOrder + 1; // ���ý�ɫ������˳��ΪBuild_set���������˳���1��ʹ����Build_set֮����Ⱦ
                Debug.Log("��ɫ����˳�����Ϊ: " + characterRenderer.sortingOrder);
            }

            currentBuildSetObject = other.gameObject; // ���µ�ǰ�Ӵ���Ԥ�Ƽ�
        }
    }

    private void BuildingReady(GameObject buildSetObject)
    {
        // ��ȡ��ǰҪ����� Build_set Ԥ�Ƽ�
        GameObject currentBuildSetObject = buildSetObject;

        // ���ݵ�ǰ�趨�Ľ���������ѡ��Ҫ�滻Ϊ��Ԥ�Ƽ�
        GameObject replacementPrefab = GetReplacementPrefab(currentBuildType);

        // ʵ�����µ�Ԥ�Ƽ����滻��ǰ�� Build_set Ԥ�Ƽ�
        GameObject newObject = Instantiate(replacementPrefab, currentBuildSetObject.transform.position, currentBuildSetObject.transform.rotation);
        Destroy(currentBuildSetObject);
    }

    private GameObject GetReplacementPrefab(GoToBuild buildType)
    {
        switch (buildType)
        {
            case GoToBuild.Standby:
                // ���� Standby ���͵�Ԥ�Ƽ�
                return replacementPrefabs[0];
            case GoToBuild.Building:
                // ���ѡ�� Building ���͵�Ԥ�Ƽ� (Element0-2)
                int randomIndexBuilding = Random.Range(0, 3);
                return replacementPrefabs[randomIndexBuilding];
            case GoToBuild.Company:
                // ���ѡ�� Company ���͵�Ԥ�Ƽ� (Element3-5)
                int randomIndexCompany = Random.Range(3, 6);
                return replacementPrefabs[randomIndexCompany];
            case GoToBuild.Farm:
                // ���ѡ�� Farm ���͵�Ԥ�Ƽ� (Element6-8)
                int randomIndexFarm = Random.Range(6, 9);
                return replacementPrefabs[randomIndexFarm];
            default:
                // Ĭ�Ϸ��� Standby ���͵�Ԥ�Ƽ�
                return replacementPrefabs[0];
        }
    }

    private void MoveToBuildPosition()
    {
        if (buildPositions.Count > 0)
        {
            Vector3 targetPosition = currentBuildType == GoToBuild.Standby ? new Vector3(0f, -8f, 0f) : buildPositions[currentBuildIndex];
            StartCoroutine(MoveToPosition(targetPosition));
        }
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private int SortBuildPositions(Vector3 positionA, Vector3 positionB)
    {
        // ���չ���Խ���λ�ý�������
        float positionADistance = Vector3.Distance(Vector3.zero, positionA);
        float positionBDistance = Vector3.Distance(Vector3.zero, positionB);

        return positionADistance.CompareTo(positionBDistance);
    }
}
