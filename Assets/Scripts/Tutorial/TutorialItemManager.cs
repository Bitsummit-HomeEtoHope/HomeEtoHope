using System.Collections;
using UnityEngine;

public class TutorialItemManager : SingletonManager<TutorialItemManager>
{
    public string itemPath; // 生成するアイテムのパス（例: "3D/food/good/apple"）

    private float moveTime = 6f;
    private float dushSpeed = 2.0f;
    private float disposeDistance = 1.0f;
    private string disposeTag = "Dispose";

    void Start()
    {
        InitializeSpecificItem(itemPath);
    }

    public void InitializeSpecificItem(string itemPath)
    {
        GameObject parentObject = GameObject.Find("---items---");

        if (parentObject != null)
        {
            GameObject itemPrefab = Resources.Load(itemPath) as GameObject;

            if (itemPrefab != null)
            {
                GameObject item = GameObject.Instantiate(itemPrefab, parentObject.transform);
                item.transform.localScale = Vector3.Scale(item.transform.localScale, new Vector3(3f, 3f, 3f));

                float defaultHeight = item.transform.position.y;
                float pauseHeight = defaultHeight + 0.5f;

                item.transform.position = transform.position;
                item.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                StartCoroutine(MoveAndDisposeItem(item, defaultHeight, pauseHeight));
            }
            else
            {
                Debug.LogError("Failed to load item prefab at path: " + itemPath);
            }
        }
        else
        {
            Debug.LogError("Parent object '---items---' not found!");
        }
    }

    private IEnumerator MoveAndDisposeItem(GameObject item, float defaultHeight, float pauseHeight)
    {
        while (item != null)
        {
            if (item.transform.position.x < disposeDistance && !item.CompareTag(disposeTag))
            {
                float moveSpeed = (disposeDistance - transform.position.x) / moveTime;
                item.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }

            if (item.transform.position.x >= disposeDistance && item.layer != LayerMask.NameToLayer("Send"))
            {
                item.layer = LayerMask.NameToLayer("Send");
            }

            if (item.CompareTag(disposeTag))
            {
                DisposeItem(item, dushSpeed);
            }

            yield return null;
        }
    }

    private void DisposeItem(GameObject item, float dushSpeed)
    {
        Vector3 backDirection = -Vector3.forward;
        Vector3 targetPosition = item.transform.position + backDirection * disposeDistance;
        item.transform.position = Vector3.MoveTowards(item.transform.position, targetPosition, dushSpeed * Time.deltaTime);
    }
}
