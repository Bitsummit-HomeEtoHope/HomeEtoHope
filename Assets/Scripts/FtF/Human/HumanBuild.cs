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
    public float moveSpeed = 5f; // 移动速度
    private bool isBuilding = false; // 是否正在建筑
    private float buildTimer = 0f; // 建筑计时器
    public float buildTime = 3f; // 建筑时间
    public float yOffset = 0.0f; // Y轴偏移量

    private Renderer characterRenderer; // 角色的渲染器组件
    private int defaultSortingOrder; // 角色默认的排序顺序

    private List<Vector3> buildPositions; // 建筑位置列表
    private int currentBuildIndex = 0; // 当前建筑位置的索引
    public GameObject[] replacementPrefabs;

    private GameObject currentBuildSetObject; // 当前正在接触的 Build_set 预制件

    public GoToBuild currentBuildType = GoToBuild.Standby; // 当前设定的建筑类型

    private bool isReadyToGo = false;

    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = GetComponent<Renderer>(); // 获取角色的渲染器组件
        defaultSortingOrder = characterRenderer.sortingOrder; // 获取角色默认的排序顺序
        ReadyToGo();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {
            buildTimer += Time.deltaTime; // 更新计时器

            if (buildTimer >= buildTime)
            {
                isBuilding = false; // 完成建筑，停止计时
                characterRenderer.sortingOrder = defaultSortingOrder; // 恢复角色的排序顺序

                BuildingReady(currentBuildSetObject); // 更改当前接触的预制件
                Debug.Log("建筑完成");

                // 移动到下一个建筑位置
                currentBuildIndex++;
                if (currentBuildIndex >= buildPositions.Count)
                {
                    currentBuildIndex = 0; // 重新开始循环建造位置
                }

                // 设置准备移动的标志
                isReadyToGo = true;
            }
        }

        // 如果准备移动的标志为 true，则调用 ReadyToGo 方法
        if (isReadyToGo)
        {
            isReadyToGo = false; // 重置标志
            ReadyToGo();
        }
    }

    private void ReadyToGo()
    {
        // 停止当前移动的协程
        StopAllCoroutines();

        // 查找场上所有带有"Build_set"标签的物体，并记录它们的位置
        buildPositions = new List<Vector3>();
        GameObject[] buildSetObjects = GameObject.FindGameObjectsWithTag("Build_set");
        foreach (GameObject buildSet in buildSetObjects)
        {
            Vector3 position = buildSet.transform.position;
            position.y += yOffset; // 添加Y轴偏移量
            buildPositions.Add(position);
        }

        // 根据规则对建筑位置进行排序
        buildPositions.Sort(SortBuildPositions);

        // 移动到第一个建筑位置
        MoveToBuildPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Build_set"))
        {
            isBuilding = true; // 进入建筑区域，开始建筑
            buildTimer = 0f; // 重置计时器

            Renderer buildRenderer = other.GetComponent<Renderer>(); // 获取Build_set物体的渲染器组件
            if (buildRenderer != null)
            {
                int buildSortingOrder = buildRenderer.sortingOrder; // 获取Build_set物体的排序顺序
                characterRenderer.sortingOrder = buildSortingOrder + 1; // 设置角色的排序顺序为Build_set物体的排序顺序加1，使其在Build_set之后渲染
                Debug.Log("角色排序顺序更新为: " + characterRenderer.sortingOrder);
            }

            currentBuildSetObject = other.gameObject; // 更新当前接触的预制件
        }
    }

    private void BuildingReady(GameObject buildSetObject)
    {
        // 获取当前要建造的 Build_set 预制件
        GameObject currentBuildSetObject = buildSetObject;

        // 根据当前设定的建筑类型来选择要替换为的预制件
        GameObject replacementPrefab = GetReplacementPrefab(currentBuildType);

        // 实例化新的预制件并替换当前的 Build_set 预制件
        GameObject newObject = Instantiate(replacementPrefab, currentBuildSetObject.transform.position, currentBuildSetObject.transform.rotation);
        Destroy(currentBuildSetObject);
    }

    private GameObject GetReplacementPrefab(GoToBuild buildType)
    {
        switch (buildType)
        {
            case GoToBuild.Standby:
                // 返回 Standby 类型的预制件
                return replacementPrefabs[0];
            case GoToBuild.Building:
                // 随机选择 Building 类型的预制件 (Element0-2)
                int randomIndexBuilding = Random.Range(0, 3);
                return replacementPrefabs[randomIndexBuilding];
            case GoToBuild.Company:
                // 随机选择 Company 类型的预制件 (Element3-5)
                int randomIndexCompany = Random.Range(3, 6);
                return replacementPrefabs[randomIndexCompany];
            case GoToBuild.Farm:
                // 随机选择 Farm 类型的预制件 (Element6-8)
                int randomIndexFarm = Random.Range(6, 9);
                return replacementPrefabs[randomIndexFarm];
            default:
                // 默认返回 Standby 类型的预制件
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
        // 按照规则对建筑位置进行排序
        float positionADistance = Vector3.Distance(Vector3.zero, positionA);
        float positionBDistance = Vector3.Distance(Vector3.zero, positionB);

        return positionADistance.CompareTo(positionBDistance);
    }
}
