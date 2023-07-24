using UnityEngine;

public class DisposeButtonScript : MonoBehaviour
{
    private static DisposeButtonScript _instance;
    public static DisposeButtonScript Instance => _instance;

    private GameObject clickedObject; 
    
    [SerializeField]
    private float moveSpeed = 5f; 
    void Awake()
    {
        _instance = this;
    }

    public void ReceiveClickedObject(GameObject clickedObject)
    {
        this.clickedObject = clickedObject;
    }

    public void OnClick()
    {
        if (clickedObject != null)
        {
            GetComponent<AudioSource>().Play();

            clickedObject.tag = "Dispose";

            Vector3 newPosition = clickedObject.transform.position + (Vector3.up * moveSpeed * Time.deltaTime);
            clickedObject.transform.position = newPosition;
        }
    }
}
