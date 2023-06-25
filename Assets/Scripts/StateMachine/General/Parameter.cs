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
        [Header("if Work")]
        public Transform[] patrolPoints;
        public List<Transform> workPoints;
        public Vector3 currentTarget;
        public float workTimer;
        public GameObject BuildAnim;
        public GameObject Build_FinsihAnim;
        [Header("if Idle")]
        public float idleTime;
        public float idleTimer;
        [Header("if Patrol")]
        public float patrolOuterRadius;
        public float patrolInnerRadius;
        public float moveSpeed;
        public float hungrySpeed;
        [Header("if Die")]
        public Texture2D did;
        public Texture2D flower;
        [Header("if Clear")]
        public GameObject CleanWanter;
        public Transform[] cleanTargetList;
        [Header("what you do")]
        public bool isHungry;
        public bool isTool;
        public bool isWork;
        public bool isClean;
        public bool isDie;
       
    }
}
