using UnityEngine;
using UnityEngine.Rendering;

public class HumanLayer_RE : MonoBehaviour
{
    public int defaultSortingOrder = 0;
    public float rotationAngleLeft = 0f;    // Y rotation angle when moving left
    public float rotationAngleRight = -180f;    // Y rotation angle when moving right

    public bool updateSortingLayer = true;    // Switch to enable/disable sorting layer update
    public bool updateRotationAngle = true;    // Switch to enable/disable rotation angle update

    private SortingGroup sortingGroup;
    private Quaternion initialRotation;
    private Vector3 lastPosition;

    private void Awake()
    {
        sortingGroup = GetComponent<SortingGroup>();
        initialRotation = transform.rotation;
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (updateSortingLayer && sortingGroup != null)
        {
            // Get the current object's Y position
            float currentYPosition = transform.position.y;

            // Set the sorting order based on the Y value
            int sortingOrder = Mathf.RoundToInt(currentYPosition * -100f);
            sortingGroup.sortingOrder = sortingOrder;
        }

        if (updateRotationAngle)
        {
            // Check the movement direction
            Vector3 currentPosition = transform.position;
            Vector3 direction = currentPosition - lastPosition;

            // Determine the rotation angle based on the movement direction
            float rotationAngle = (direction.x < 0) ? rotationAngleLeft : rotationAngleRight;

            // Set the Y rotation angle while preserving the X and Z rotation values
            Quaternion newRotation = Quaternion.Euler(initialRotation.eulerAngles.x, rotationAngle, initialRotation.eulerAngles.z);
            transform.rotation = newRotation;

            // Update the last frame's position
            lastPosition = currentPosition;
        }
    }
}
