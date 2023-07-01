using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Manager : MonoBehaviour
{
    public Camera camera2d;
    public List<GameObject> buildList = new List<GameObject>();
    public bool isAllBuild = false;
    public Transform targetPosition; // 目标位置游戏物体
    public Transform targetRotation; // 目标旋转角度游戏物体
    [SerializeField]public float fieldOfViewSpeed = 2f; // Field of View 变化速度
    [SerializeField]public int targetFieldOfView = 45; // Field of View 变化速度
    [SerializeField] public float cameraMoveSpeed = 2f; // 相机移动速度

    private float cameraMoveTimer = 0f; // 相机移动计时器
    public float cameraMoveDuration = 2f; // 相机移动总时间

    private void Update()
    {
        isAllBuild = IsAllBuild();
        if (isAllBuild)
        {
            // 平滑过渡相机的 Field of View 到目标值
            camera2d.fieldOfView = Mathf.Lerp(camera2d.fieldOfView, targetFieldOfView, Time.deltaTime * fieldOfViewSpeed);

            // 使用正弦函数计算相机移动速度的衰减值
            float moveSpeed = Mathf.Lerp(cameraMoveSpeed, 0f, Mathf.Sin(cameraMoveTimer / cameraMoveDuration * Mathf.PI * 0.5f));

            // 平滑过渡相机的 position 到目标位置
            camera2d.transform.position = Vector3.Lerp(camera2d.transform.position, targetPosition.position, Time.deltaTime * moveSpeed);

            // 平滑过渡相机的 rotation 到目标旋转角度
            camera2d.transform.rotation = Quaternion.Lerp(camera2d.transform.rotation, targetRotation.rotation, Time.deltaTime * moveSpeed);

            // 设置 buildList 中所有物体的 SetActive(true)
            foreach (var item in buildList)
            {
                item.SetActive(true);
            }

            // 更新相机移动计时器
            cameraMoveTimer += Time.deltaTime;
        }
    }

    public bool IsAllBuild()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            var childIsBuild = child.GetComponent<Check_HumanDistance>();
            if (childIsBuild != null && !childIsBuild.isBuild)
            {
                return false;
            }
        }
        return true;
    }
}
