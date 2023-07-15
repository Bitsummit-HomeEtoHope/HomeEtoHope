using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine.General;
using UnityEngine;
using Random = UnityEngine.Random;

public class KillPeople : MonoBehaviour
{
	public List<GameObject> peopleSet = new List<GameObject>();
	public Transform parent;
	public GameObject currentTarget;
	public Parameter parameter;
	public Vector3 targetPos;

	private void Awake()
	{
		targetPos = new Vector3(0, -175.9f, -203.296f);//-203.296
        parent = GameObject.Find("-----Human_Bag-----").transform;
	}

	private void MoveToTarget(GameObject target)
	{
		if (target == null)
			return;
		if (Vector3.Distance(target.transform.position, transform.position) < 20)
		{
			if (parameter.CDtimer >= parameter.CDtime)
			{
				Kill(target);
				return;
			}
		}
		else
		{
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
				Time.deltaTime*parameter.moveSpeed);
		}
	}

	private GameObject GetTarget()
	{
		GetChildren();
		if (peopleSet.Count == 0)
		{
			if (Vector3.Distance(targetPos, transform.position)>0.1)
			{
				transform.position = Vector3.MoveTowards(transform.position, targetPos,
					Time.deltaTime*parameter.moveSpeed);
			}
			else
			{
				SwithTarget();
			}
		}
		else if (currentTarget == null)
		{
			var a = Random.Range(0, parent.childCount);
			currentTarget = peopleSet[a];
		}
		return currentTarget;
	}

	private void Update()
	{
		parameter.CDtimer += Time.deltaTime;
		MoveToTarget(GetTarget());
	}

	private void GetChildren()
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			if (parent.GetChild(i).CompareTag("Player"))
			{
				if (!peopleSet.Contains(parent.GetChild(i).gameObject))
				{
					peopleSet.Add(parent.GetChild(i).gameObject);
				}
			}
		}
	}

	private void Kill(GameObject target)
	{
		var a = target.transform.position;
        GameObject obj = Instantiate(Resources.Load<GameObject>("2D_set/human/flower"));

        GameObject flowersObject = GameObject.Find("-----Garden-----");


        // Debug.Log("hi");
        peopleSet.Remove(target);
		Destroy(target);

        if (flowersObject != null)
        {
            obj.transform.position = a;
            obj.transform.SetParent(flowersObject.transform);
        }
        else
        {
            Debug.LogError("Parent object '-----Flowers-----' not found in the scene.");
        }		currentTarget = null;
		parameter.CDtimer = 0;
	}

	private void SwithTarget()
	{
		float x = Random.Range(-178, 202);
		float y = Random.Range(-118, -91);
		targetPos = new Vector3(x, y, -203.296f);
	}
}
