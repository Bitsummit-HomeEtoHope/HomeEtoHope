using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class criminal : MonoBehaviour
{
    public Animator animator;//アニマーターコンポーネット
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //人を選択時（今はないので、フルーツで代用）

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
