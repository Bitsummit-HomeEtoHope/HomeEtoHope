using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Parameter
{
	//It is a class that lists the parameter you need, I recommend you declare variables here!
	public Transform[] patrolPoints;
	public Vector3 currentTarget;
	public float idleTime;
	public float idleTimer;
	public float patrolRadius;
	public float moveSpeed;
}
