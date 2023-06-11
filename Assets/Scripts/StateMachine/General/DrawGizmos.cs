using System;
using UnityEngine;

namespace StateMachine.General
{
    public class DrawGizmos : MonoBehaviour
    {
        public Parameter parameter;
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,parameter.patrolInnerRadius);
            Gizmos.DrawWireSphere(transform.position,parameter.patrolOuterRadius);
        }
    }
}
