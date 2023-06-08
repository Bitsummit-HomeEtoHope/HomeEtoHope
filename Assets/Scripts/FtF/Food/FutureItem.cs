using System.Collections;
using UnityEngine;

public class FutureItem : MonoBehaviour
{
    [SerializeField] private float yOffset = 1f; // ��Ե�ǰλ�õ�y��ƫ����
    [SerializeField] private float moveDuration = 1f; // �ƶ������ʱ��

    // Start is called before the first frame update
    void Start()
    {
        MoveToOffsetPosition();
    }

    // �ƶ���ƫ��λ��
    private void MoveToOffsetPosition()
    {
        Vector3 targetPosition = transform.localPosition + new Vector3(0f, yOffset, 0f);

        // ʹ��Э��ʵ����ָ��ʱ����ƽ���ƶ���Ŀ��λ��
        StartCoroutine(MoveToPosition(targetPosition, moveDuration));
    }

    // ��ָ��ʱ����ƽ���ƶ���Ŀ��λ��
    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
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
    }
}
