using UnityEngine;

public class HumanRotation : MonoBehaviour
{
    [SerializeField] public int rotation;
    private Transform characterTransform;
    private Vector3 previousPosition;

    private void Start()
    {
        characterTransform = transform; // ��ȡ��ɫ��Transform���
        previousPosition = characterTransform.position; // �����ʼλ��
    }

    private void Update()
    {
        Vector3 currentPosition = characterTransform.position; // ��ȡ��ǰλ��

        if (currentPosition.x < previousPosition.x)
        {
            characterTransform.rotation = Quaternion.Euler(rotation, 0f, 0f); // �����ƶ�ʱ��Y����תΪ0��
        }
        else if (currentPosition.x > previousPosition.x)
        {
            characterTransform.rotation = Quaternion.Euler(-rotation, 180f, 0f); // �����ƶ�ʱ��Y����תΪ180��
        }

        previousPosition = currentPosition; // ����ǰһλ��
    }
}
