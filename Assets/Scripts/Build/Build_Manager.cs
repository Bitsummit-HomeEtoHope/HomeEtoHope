using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Manager : MonoBehaviour
{
    [Header("Secend Camera")]
    public Camera camera2d;
    [SerializeField] public float moveSpeed = 2f; // Field of View 变化速度
   // [SerializeField] public float fieldOfViewSpeed = 2f; // Field of View 变化速度
   // [SerializeField] public int targetFieldOfView = 45; // Field of View 目标值
    [SerializeField] public float cameraMoveSpeed = 2f; // 相机移动速度
    [Header("Velus")]
    [SerializeField]private float cameraMoveTimer = 0f; // 相机移动计时器
    [SerializeField]public float cameraMoveDuration = 2f; // 相机移动总时间
    [SerializeField]public Transform targetPosition; // 目标位置游戏物体
    [SerializeField]public Transform targetRotation; // 目标旋转角度游戏物体
    [Header("Change numbers")]
    [SerializeField]public int requiredBuildCount = 4; // 需要的 Build_39 标签数量
    [Header("Secend Set")]
    public GameObject enableGameObject; // 启用的游戏物体



    private void Update()
    {
        int buildCount = CountBuildsWithTag("Build_39");
        if (buildCount >= requiredBuildCount)
        {
            // 平滑过渡相机的 Field of View 到目标值
       //     camera2d.fieldOfView = Mathf.Lerp(camera2d.fieldOfView, targetFieldOfView, Time.deltaTime * fieldOfViewSpeed);

            // 使用正弦函数计算相机移动速度的衰减值
          //  float moveSpeed = Mathf.Lerp(cameraMoveSpeed, 0f, Mathf.Sin(cameraMoveTimer / cameraMoveDuration * Mathf.PI * 0.5f));

            // 平滑过渡相机的 position 到目标位置
            camera2d.transform.position = Vector3.Lerp(camera2d.transform.position, targetPosition.position, Time.deltaTime * moveSpeed);

            // 平滑过渡相机的 rotation 到目标旋转角度
            camera2d.transform.rotation = Quaternion.Lerp(camera2d.transform.rotation, targetRotation.rotation, Time.deltaTime * moveSpeed);

            // 启用指定的游戏物体
            if (enableGameObject != null)
            {
                enableGameObject.SetActive(true);
            }

            // 更新相机移动计时器
            cameraMoveTimer += Time.deltaTime;
        }
    }

    private int CountBuildsWithTag(string tag)
    {
        GameObject[] buildObjects = GameObject.FindGameObjectsWithTag(tag);
        return buildObjects.Length;
    }
}
