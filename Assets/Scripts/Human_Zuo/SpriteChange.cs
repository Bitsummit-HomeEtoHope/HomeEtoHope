using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine.General;

public class SpriteChange : MonoBehaviour
{
    public FSM manager;

    private float currentTime;
    private float targetTime =0;
    [SerializeField]
    private Sprite spriteHair;
    [SerializeField]
    private Sprite spriteFace;
    [SerializeField]
    private Sprite spriteBody;

    private Sprite defaultSpriteHair;
    private Sprite defaultSpriteFace;
    private Sprite defaultSpriteBody;
    private float defaultSpeed;

    private void Start() 
    {
        manager = GetComponent<FSM>();
        defaultSpeed=manager.parameter.hungrySpeed;
        targetTime=manager.parameter.levelDataCurrent._levelData.future_Data.itemBrokenHuman_time;

        foreach(Transform child in transform)
        {
            if(child.gameObject.tag=="Hair")
            {
                defaultSpriteHair=child.GetComponent<SpriteRenderer>().sprite;
            }
            if(child.gameObject.tag=="Face")
            {
                defaultSpriteFace=child.GetComponent<SpriteRenderer>().sprite;
            }
            if(child.gameObject.tag=="Body")
            {
                defaultSpriteBody=child.GetComponent<SpriteRenderer>().sprite;
            }
        }

    }
    private void Update() {
        if(manager.parameter.isBroken)
        {
           
            currentTime+=Time.deltaTime;
            manager.parameter.hungrySpeed=5;
            foreach(Transform child in transform)
                {
                    if(child.gameObject.tag=="Hair")
                    {
                        child.GetComponent<SpriteRenderer>().sprite=spriteHair;
                    }
                    if(child.gameObject.tag=="Face")
                    {
                        child.GetComponent<SpriteRenderer>().sprite=spriteFace;
                    }
                    if(child.gameObject.tag=="Body")
                    {
                        child.GetComponent<SpriteRenderer>().sprite=spriteBody;
                    }
                }
            if(currentTime>=targetTime)
            {
                foreach(Transform child in transform)
                {
                    if(child.gameObject.tag=="Hair")
                    {
                        child.GetComponent<SpriteRenderer>().sprite=defaultSpriteHair;
                    }
                    if(child.gameObject.tag=="Face")
                    {
                        child.GetComponent<SpriteRenderer>().sprite=defaultSpriteFace;
                    }
                    if(child.gameObject.tag=="Body")
                    {
                        child.GetComponent<SpriteRenderer>().sprite=defaultSpriteBody;
                    }
                }
                manager.parameter.hungrySpeed=defaultSpeed;
                manager.parameter.isBroken=false;
                currentTime=0;
            }
        }
    }
}

