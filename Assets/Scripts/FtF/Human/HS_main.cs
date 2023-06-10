using UnityEngine;

public class HS_Main : MonoBehaviour
{
    // ��Ҫ��Inspector�����õĲ���
    [Header("�ƶ�����")]
    public float moveSpeed = 1.0f; // �ƶ��ٶ�
    public float maxDistance = 2.0f; // ����������
    public float maxMoveX = 0.5f; // ��x��������ƶ�����
    public float maxMoveY = 0.5f; // ��y��������ƶ�����
    public float moveFrequency = 3.0f; // �ƶ�Ƶ��

    // ��Ҫ��Inspector�����õ���Ϸ����
    [Header("Ŀ�����")]
    public Transform targetObject; // Ŀ�����

    // �ƶ�״̬
    private enum MoveState
    {
        RandomMove, // ����ƶ�״̬
        XAxisMove // ����X���ƶ�״̬
    }

    // ��ǰ�ƶ�״̬
    private MoveState currentMoveState = MoveState.RandomMove;

    // Ŀ��λ��
    private Vector3 targetPosition;

    // ����ƶ�ʱ�ļ�ʱ��
    private float randomMoveTimer = 0.0f;
    private float randomMoveInterval = 0.0f; // ����ƶ�״̬����ʱ��

    // Start is called before the first frame update
    void Start()
    {
        // ���ó�ʼ����ת�Ƕ�
        transform.rotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);

        // ��ʼ������ƶ�״̬����ʱ��
        randomMoveInterval = Random.Range(moveFrequency - 0.5f, moveFrequency + 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ƿ���Ŀ�����
        if (targetObject != null)
        {
            // ��ȡ��ǰλ��
            Vector3 currentPosition = transform.position;

            // �жϵ�ǰ�ƶ�״̬
            switch (currentMoveState)
            {
                case MoveState.RandomMove:
                    // �������ƶ���ʱ���Ƿ񳬹�����ʱ��
                    randomMoveTimer += Time.deltaTime;
                    if (randomMoveTimer >= randomMoveInterval)
                    {
                        // �л�������X���ƶ�״̬
                        currentMoveState = MoveState.XAxisMove;
                        targetPosition = new Vector3(
                            currentPosition.x + Random.Range(-maxMoveX, maxMoveX),
                            currentPosition.y,
                            currentPosition.z
                        );
                        randomMoveTimer = 0.0f;
                    }
                    else
                    {
                        // ����Ŀ������λ�ü����ƶ�Ŀ���
                        targetPosition = new Vector3(
                            Random.Range(targetObject.position.x - maxDistance, targetObject.position.x + maxDistance),
                            Random.Range(targetObject.position.y - maxDistance, targetObject.position.y + maxDistance),
                            Random.Range(targetObject.position.z - maxDistance, targetObject.position.z + maxDistance)
                        );
                    }
                    break;
                case MoveState.XAxisMove:
                    // �����ƶ��ķ���
                    Vector3 moveDirection = targetPosition - currentPosition;

                    // ������x���ϵ��ƶ�����
                    float moveX = Mathf.Clamp(moveDirection.x, -maxMoveX, maxMoveX);

                    // �����ƶ�����������ת�Ƕ�
                    if (moveX > 0)
                    {
                        transform.rotation = Quaternion.Euler(-45.0f, -180.0f, 0.0f);
                    }
                    else if (moveX < 0)
                    {
                        transform.rotation = Quaternion.Euler(45.0f, 0.0f, 0.0f);
                    }

                    // �������յ��ƶ�����
                    Vector3 moveVector = new Vector3(moveX, 0.0f, 0.0f) * moveSpeed * Time.deltaTime;

                    // ����λ��
                    transform.position += moveVector;

                    // ����Ƿ񵽴�Ŀ��λ��
                    if (Mathf.Abs(currentPosition.x - targetPosition.x) <= 0.01f)
                    {
                        // �л�������ƶ�״̬
                        currentMoveState = MoveState.RandomMove;

                        // ������������ƶ�״̬����ʱ��
                        randomMoveInterval = Random.Range(moveFrequency - 0.5f, moveFrequency + 0.5f);
                    }
                    break;
            }
        }
    }
}
