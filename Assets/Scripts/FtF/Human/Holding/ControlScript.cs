using UnityEngine;

public class ControlScript : MonoBehaviour
{
    public HumanBuild targetScript; // 目标脚本的引用

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetScript.currentBuildType = GoToBuild.Standby;
            Debug.Log("当前状态：Standby");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            targetScript.currentBuildType = GoToBuild.Building;
            Debug.Log("当前状态：Building");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetScript.currentBuildType = GoToBuild.Company;
            Debug.Log("当前状态：Company");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            targetScript.currentBuildType = GoToBuild.Farm;
            Debug.Log("当前状态：Farm");
        }
    }
}
