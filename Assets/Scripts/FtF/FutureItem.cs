using System.Collections;
using UnityEngine;

public class FutureItem : MonoBehaviour
{
    [SerializeField] private float yOffset = 1f; // ��Ե�ǰλ�õ�y��ƫ����
    [SerializeField] private float moveDuration = 1f; // �ƶ������ʱ��

    private bool isMoving = false; // �Ƿ������ƶ�

    // Start is called before the first frame update
    void Start()
    {
        MoveToOffsetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(AutoMove());
        }
    }

    // �ƶ���ƫ��λ��
    private void MoveToOffsetPosition()
    {
        Vector3 targetPosition = transform.localPosition;
        targetPosition.y += yOffset;

        // ʹ��Э��ʵ����ָ��ʱ����ƽ���ƶ���Ŀ��λ��
        StartCoroutine(MoveToPosition(targetPosition, moveDuration));
    }

    // ��ָ��ʱ����ƽ���ƶ���Ŀ��λ��
    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        isMoving = true; // ����Ϊ�����ƶ�״̬

        Vector3 startPosition = transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // ���㵱ǰʱ���Ĳ�ֵ����
            float t = elapsedTime / duration;

            // ʹ�ò�ֵ���㵱ǰλ��
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ƶ�����ʱȷ������Ŀ��λ��
        transform.localPosition = targetPosition;

        isMoving = false; // ����Ϊ���ƶ�״̬
    }

    // �Զ��ƶ�
    private IEnumerator AutoMove()
    {
        isMoving = true; // ����Ϊ�����ƶ�״̬

        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = startPosition + new Vector3(-1f, 0f, 0f); // Ŀ��λ��Ϊ��ǰλ���� x ��ĸ������ƶ�1f

        float elapsedTime = 0f;
        float duration = 1f; // �Զ��ƶ���ʱ��

        while (elapsedTime < duration)
        {
            // ���㵱ǰʱ���Ĳ�ֵ����
            float t = elapsedTime / duration;

            // ʹ�ò�ֵ���㵱ǰλ��
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ƶ�����ʱȷ������Ŀ��λ��
        transform.localPosition = targetPosition;

        isMoving = false; // ����Ϊ���ƶ�״̬
    }
}
