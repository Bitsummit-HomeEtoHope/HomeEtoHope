using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using Unity.VisualScripting;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
	private Animator animator;
	public FSM manager;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		animator.SetBool("isHungry",manager.parameter.isHungry);
	}
}
