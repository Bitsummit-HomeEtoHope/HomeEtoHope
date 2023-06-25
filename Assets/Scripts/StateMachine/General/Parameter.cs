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
        public Transform[] patrolPoints;
        public List<Transform> workPoints;
        public Vector3 currentTarget;
        public float idleTime;
        public float idleTimer;
        public float workTimer;
        public float patrolOuterRadius;
        public float patrolInnerRadius;
        public float moveSpeed;
        public float hungrySpeed;
        public bool isHungry;
        public bool isTool;
        public bool isWork;
        public GameObject BuildAnim;
        public GameObject Build_FinsihAnim;
        public GameObject Food_Tran;
    }
}
