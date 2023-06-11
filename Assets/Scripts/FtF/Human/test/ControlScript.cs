using UnityEngine;

public class ControlScript : MonoBehaviour
{
    public HumanBuild targetScript; // Ŀ��ű�������

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetScript.currentBuildType = GoToBuild.Standby;
            Debug.Log("��ǰ״̬��Standby");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            targetScript.currentBuildType = GoToBuild.Building;
            Debug.Log("��ǰ״̬��Building");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetScript.currentBuildType = GoToBuild.Company;
            Debug.Log("��ǰ״̬��Company");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            targetScript.currentBuildType = GoToBuild.Farm;
            Debug.Log("��ǰ״̬��Farm");
        }
    }
}
