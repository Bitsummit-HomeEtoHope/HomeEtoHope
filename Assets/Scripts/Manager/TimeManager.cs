using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("Dio")]
    [SerializeField] private bool isPaused = false; // ������ͣ״̬
    [SerializeField] public List<GameObject> targetObjects; // �����Ϸ����
    private Dictionary<GameObject, Vector3> initialObjectPositions;

    [Header("JoJo")]
    public Button[] buttonsToDisable; // ָ����Ҫ���õİ�ť����
    private List<MeshCollider> meshColliders;
    private List<BoxCollider> boxColliders;
 
    public GameObject[] objectsToDisable; // �洢��Ҫ���õ���Ϸ��������

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

       //SaveObjectStates();
       // SetTargetObjectsEnabled(true); // ��������˶�ֹͣ����������Transform

        DisableButtons();

        DisableObjects();

        SetCollidersEnabled(false);

        Time.timeScale = 0f;
        
        

    }

    public void Resume()
    {
        isPaused = false;

       // SetTargetObjectsEnabled(false); // ��������˶��ָ�����������Transform

        EnableButtons();

        EnableObjects();

        SetCollidersEnabled(true);

        Time.timeScale = 1f;

        
    }



    private void SetTargetObjectsEnabled(bool enabled)
    {
        foreach (GameObject obj in targetObjects)
        {
            SetObjectEnabledRecursively(obj, enabled);
        }
    }

    private void SetObjectEnabledRecursively(GameObject obj, bool enabled)
    {
        obj.transform.position = enabled ? initialObjectPositions[obj] : obj.transform.position;
        obj.transform.rotation = enabled ? Quaternion.identity : obj.transform.rotation;
        obj.transform.localScale = enabled ? Vector3.one : obj.transform.localScale;
        obj.transform.GetComponent<Rigidbody>().isKinematic = enabled;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            SetObjectEnabledRecursively(child, enabled);
        }
    }



    private void SaveObjectStates()
    {
        initialObjectPositions = new Dictionary<GameObject, Vector3>();
        foreach (GameObject obj in targetObjects)
        {
            SaveObjectStateRecursively(obj);
        }
    }

    private void SaveObjectStateRecursively(GameObject obj)
    {
        initialObjectPositions[obj] = obj.transform.position;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            SaveObjectStateRecursively(child);
        }
    }



    private void GetCollider()
    {
        // ��ȡ���������е� Mesh Collider ���
        MeshCollider[] allMeshColliders = FindObjectsOfType<MeshCollider>();
        meshColliders = new List<MeshCollider>();

        foreach (MeshCollider collider in allMeshColliders)
        {
            if (collider != null)
            {
                meshColliders.Add(collider);
            }
        }

        // ��ȡ���������е� Box Collider ���
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
        // ���� Mesh Collider ������״̬
        foreach (MeshCollider collider in meshColliders)
        {
            collider.enabled = enabled;
        }

        // ���� Box Collider ������״̬
        foreach (BoxCollider collider in boxColliders)
        {
            collider.enabled = enabled;
        }
    }


    public void DisableButtons()
    {
        foreach (Button button in buttonsToDisable)
        {
            button.interactable = false; // ���ð�ť�Ľ�������
        }
    }

    public void EnableButtons()
    {
        foreach (Button button in buttonsToDisable)
        {
            button.interactable = true; // ���ð�ť�Ľ�������
        }
    }

    public void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false); // ������Ϸ����
        }
    }

    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(true); // ������Ϸ����
        }
    }


}
