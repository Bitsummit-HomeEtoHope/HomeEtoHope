using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // 控制暂停状态
    [SerializeField] public List<GameObject> targetObjects; // 多个游戏对象
    [Header("JoJo")]
    public Button[] buttonsToDisable; // 指定需要禁用的按钮数组
    private List<MeshCollider> meshColliders;
    private List<BoxCollider> boxColliders;
 
    public GameObject[] objectsToDisable; // 存储需要禁用的游戏物体数组

    private void OnEnable()
    {
        GetCollider();
        Pause();
    }

    private void OnDisable()
    {
        Resume();
    }

    public void Pause()
    {
        isPaused = true;

        DisableButtons();

        DisableObjects();

        SetCollidersEnabled(false);

        Time.timeScale = 0f;
        
        

    }

    public void Resume()
    {
        isPaused = false;

        EnableButtons();

        EnableObjects();

        SetCollidersEnabled(true);

        Time.timeScale = 1f;

        
    }

    private void GetCollider()
    {
        // 获取场景中所有的 Mesh Collider 组件
        MeshCollider[] allMeshColliders = FindObjectsOfType<MeshCollider>();
        meshColliders = new List<MeshCollider>();

        foreach (MeshCollider collider in allMeshColliders)
        {
            if (collider != null)
            {
                meshColliders.Add(collider);
            }
        }

        // 获取场景中所有的 Box Collider 组件
        BoxCollider[] allBoxColliders = FindObjectsOfType<BoxCollider>();
        boxColliders = new List<BoxCollider>();

        foreach (BoxCollider collider in allBoxColliders)
        {
            if (collider != null)
            {
                boxColliders.Add(collider);
            }
        }
    }

    private void SetCollidersEnabled(bool enabled)
    {
        // 设置 Mesh Collider 的启用状态
        foreach (MeshCollider collider in meshColliders)
        {
            collider.enabled = enabled;
        }

        // 设置 Box Collider 的启用状态
        foreach (BoxCollider collider in boxColliders)
        {
            collider.enabled = enabled;
        }
    }


    public void DisableButtons()
    {
        foreach (Button button in buttonsToDisable)
        {
            button.interactable = false; // 禁用按钮的交互功能
        }
    }

    public void EnableButtons()
    {
        foreach (Button button in buttonsToDisable)
        {
            button.interactable = true; // 启用按钮的交互功能
        }
    }

    public void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false); // 禁用游戏物体
        }
    }

    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true); // 启用游戏物体
        }
    }


}
