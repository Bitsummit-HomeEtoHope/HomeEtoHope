
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Manager : MonoBehaviour
{
    [Header("Second Camera")]
    public Camera camera2d;
    [SerializeField] public float moveSpeed = 2f; // Speed of FOV change
                                                  // [SerializeField] public float fieldOfViewSpeed = 2f; // Speed of FOV change
                                                  // [SerializeField] public int targetFieldOfView = 45; // Target FOV value
    [SerializeField] public float cameraMoveSpeed = 2f; // Camera movement speed
    [Header("Values")]
    [SerializeField] private float cameraMoveTimer = 0f; // Camera movement timer
    [SerializeField] public float cameraMoveDuration = 2f; // Total camera movement duration
    [SerializeField] public Transform targetPosition; // Target position game object
    [SerializeField] public Transform targetRotation; // Target rotation game object
    [Header("Change numbers")]
    [SerializeField] public int requiredBuildCount = 4; // Required number of objects with "Build_39" tag
    [SerializeField] private int buildCount;
    [Header("Second Set")]
    public GameObject enableGameObject; // Enabled game object
    private bool setOk = false;
    [Header("time?")]
    public bool needTimeMove = true; // if Move need time 


    private void Update()
    {

        if (!needTimeMove)
        {
            if (!setOk)
            {
                buildCount = CountBuildsWithTag("Build_39");
                if (buildCount >= requiredBuildCount)
                {
                    if (camera2d != null)
                    {
                        if (targetPosition != null)
                            camera2d.transform.position = targetPosition.position;
                        if (targetRotation != null)
                            camera2d.transform.rotation = targetRotation.rotation;

                        if (enableGameObject != null)
                        {
                            enableGameObject.SetActive(true);
                        }

                        setOk = true;
                    }
                }
            }

        }
        else
        {
            if (!setOk)
            {
                buildCount = CountBuildsWithTag("Build_39");
                if (buildCount >= requiredBuildCount)
                {
                    // Smoothly transition the camera's FOV to the target value
                    //  camera2d.fieldOfView = Mathf.Lerp(camera2d.fieldOfView, targetFieldOfView, Time.deltaTime * fieldOfViewSpeed);
                    // Use sine function to calculate the decay value of camera movement speed
                    //  float moveSpeed = Mathf.Lerp(cameraMoveSpeed, 0f, Mathf.Sin(cameraMoveTimer / cameraMoveDuration * Mathf.PI * 0.5f));

                    if (camera2d != null)   //set the new Camera
                    {
                        if (targetPosition != null)
                            camera2d.transform.position = Vector3.Lerp(camera2d.transform.position, targetPosition.position, Time.deltaTime * moveSpeed);
                        if (targetRotation != null)
                            camera2d.transform.rotation = Quaternion.Lerp(camera2d.transform.rotation, targetRotation.rotation, Time.deltaTime * moveSpeed);

                        if (enableGameObject != null)
                        {
                            enableGameObject.SetActive(true);

                        }

                        cameraMoveTimer += Time.deltaTime;
                        if (cameraMoveTimer >= cameraMoveDuration) setOk = true;
                    }
                }
            }
        }
    }


    // Currently not checking its own child objects.

    private int CountBuildsWithTag(string tag)
    {
        GameObject[] buildObjects = GameObject.FindGameObjectsWithTag(tag);
        return buildObjects.Length;
    }

}
