using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_HumanDistance : MonoBehaviour
{
    public float check_Distance=1f;
    [SerializeField]
    public bool isBuild=false;

    private void Update() {
        if(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").gameObject.transform.position,transform.position)<check_Distance)
        {
            isBuild=true;
        }
        if(isBuild)
        {
            this.GetComponent<SpriteRenderer>().enabled=false;
        }
    }

}
