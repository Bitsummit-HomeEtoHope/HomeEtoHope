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
        public Vector3 currentTarget;
        public float idleTime;
        public float idleTimer;
        public float patrolOuterRadius;
        public float patrolInnerRadius;
        public float moveSpeed;
        public bool isHungry;
    }
}
