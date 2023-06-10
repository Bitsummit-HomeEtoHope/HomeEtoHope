using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface HumanBase
{
	public void Patrol();
	public void Eating();
	public void Walking();
	public void GettingSick();
	public void Hungry();
	public void GetTarget();
}
