using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using UnityEngine;

public class GetTool_icon : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite=GetComponent<SpriteRenderer>();
        //player获得父物体
        player=transform.parent.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        sprite.sprite=player.GetComponent<GetItem_Human>().toolList_human[0].GetComponent<SpriteRenderer>().sprite;
        transform.localScale=new Vector3(2,2,1);
    }
}
