using UnityEngine;

public class Title_human : MonoBehaviour
{
    public GameObject[] floatingObjects;
    public float floatSpeed = 1.0f;
    public float floatHeight = 0.5f;
    private Vector3[] originalPositions;

    private void Start()
    {
        originalPositions = new Vector3[floatingObjects.Length];

        for (int i = 0; i < floatingObjects.Length; i++)
        {
            originalPositions[i] = floatingObjects[i].transform.position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < floatingObjects.Length; i++)
        {
            float newY = originalPositions[i].y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
            floatingObjects[i].transform.position = new Vector3(originalPositions[i].x, newY, originalPositions[i].z);
        }
    }
}
