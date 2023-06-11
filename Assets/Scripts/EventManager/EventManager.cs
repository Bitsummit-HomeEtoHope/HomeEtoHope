using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EventManager : MonoBehaviour
{
	public static UnityAction<StateType> TransportToolEvent;
	public static UnityAction<StateType> TransportFoodEvent;
}
