using UnityEngine;

public class timewellout : MonoBehaviour
{
    public Transform targetPosition; // ָ����Ŀ��λ��
    public float movementTime = 90f; // �ƶ�ʱ�䣨�룩

    private float timer = 0f;
    private Vector3 startingPosition;
    private bool isMoving = false;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        if (isMoving)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / movementTime);

            // ��ָ��ʱ���ڽ�Ԥ�Ƽ��ƶ���Ŀ��λ��
            transform.position = Vector3.Lerp(startingPosition, targetPosition.position, t);

            if (t >= 1f)
            {
                // �ƶ���ɺ�ִ����ز���
                // ���磺���Ŷ����������¼���
                Debug.Log("�ƶ���ɣ�");
                isMoving = false;
            }
        }
    }

    public void StartMovement()
    {
        timer = 0f;
        isMoving = true;
    }
}
