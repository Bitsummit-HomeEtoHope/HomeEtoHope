using UnityEngine;

public class HumanRotation : MonoBehaviour
{
    [SerializeField] public int rotation;
    private Transform characterTransform;
    private Vector3 previousPosition;

    private void Start()
    {
        characterTransform = transform; 
        previousPosition = characterTransform.position; 
    }

    private void Update()
    {
        Vector3 currentPosition = characterTransform.position; 

        if (currentPosition.x < previousPosition.x)
        {
            characterTransform.rotation = Quaternion.Euler(rotation, 0f, 0f); 
        }
        else if (currentPosition.x > previousPosition.x)
        {
            characterTransform.rotation = Quaternion.Euler(-rotation, 180f, 0f);
        }

        previousPosition = currentPosition;
    }
}
