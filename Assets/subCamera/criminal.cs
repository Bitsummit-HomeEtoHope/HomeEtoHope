using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class criminal : MonoBehaviour
{
    public Animator animator;//�A�j�}�[�^�[�R���|�[�l�b�g
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //�l��I�����i���͂Ȃ��̂ŁA�t���[�c�ő�p�j

    }
    public void MoveTextLeft()
    {
       // if (gameObject == "Fruit")
        //{
            animator.SetBool("List", true);
        //}
    }
    public void ResetTextPosition()
    {
        animator.SetBool("List", false);
    }
}
