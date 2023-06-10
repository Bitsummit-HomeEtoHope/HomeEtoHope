using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class
//Don't modify!
public interface IState
{
	void Onenter();
	void OnUpdate();
	void OnExit();
}
