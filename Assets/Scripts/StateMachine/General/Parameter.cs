using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace StateMachine.General
{
    [Serializable]
    public class Parameter
    {
        //It is a class that lists the parameter you need, I recommend you declare variables here!

        
        [Header("ifIdle")]
        public float idleTime;
        public float idleTimer;
        
        [Header("ifWork")]
        public List<Transform> workPoints;
        public Vector3 currentTarget;       
        public float workTimer;
        public GameObject BuildAnim;
        public GameObject Build_FinsihAnim;
        public GameObject Food_Tran;
        
        [Header("ifPatrol")]
        public Transform[] patrolPoints;
        public float patrolOuterRadius;
        public float patrolInnerRadius;
        public float moveSpeed;
        
        [Header("ifHungry")]
        public float hungrySpeed;

        [Header("ifDie")]
        public Texture2D did;
        public Texture2D flower;
        
        [Header("if Clean")]
        public GameObject CleanWanter;
        public Transform[] cleanTargetList;

        [Header("switch")]
        public bool isHungry;
        public bool isTool;
        public bool isWork;
        public bool isClean;
        public bool isDie;
    }
}
