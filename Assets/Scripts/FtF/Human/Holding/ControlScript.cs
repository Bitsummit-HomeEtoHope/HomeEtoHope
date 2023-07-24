using UnityEngine;

public class ControlScript : MonoBehaviour
{
    public HumanBuild targetScript; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetScript.currentBuildType = GoToBuild.Standby;
            Debug.Log("Standby");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            targetScript.currentBuildType = GoToBuild.Building;
            Debug.Log("Building");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetScript.currentBuildType = GoToBuild.Company;
            Debug.Log("Company");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            targetScript.currentBuildType = GoToBuild.Farm;
            Debug.Log("Farm");
        }
    }
}
