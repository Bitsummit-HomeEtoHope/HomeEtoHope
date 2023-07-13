using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using UnityEngine;

public class Eat_Food : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject player;

    private void Start() {
        
        spriteRenderer=GetComponent<SpriteRenderer>();
    }
    private void Update() 
    {
        //change Scale
        spriteRenderer.sprite=player.GetComponent<GetItem_Human>().foodList_human[0].GetComponent<SpriteRenderer>().sprite;
        
        transform.localScale=new Vector3(2,2,1);
    }
        

    
}
