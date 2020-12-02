using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolBehaviour : AIBehaviour
{
	List<Vector3> positions = new List<Vector3>(new Vector3[] { new Vector3(4, 1.5f, 4), new Vector3(-4, 1.5f, 4), new Vector3(4, 1.5f, -4), new Vector3(-4, 1.5f, -4) });
	float moveSpeed = 2f;

	public override void OnEnter()
	{
		Debug.Log("Enter Patrol Behaviour");
		//sort the list by closest place first
		positions = positions.OrderBy(position => Vector3.Distance(transform.position, position)).ToList();
		//switch the last and the one before that
		positions.Insert(positions.Count, positions[2]);
		positions.RemoveAt(2);
	}

	public override void Execute()
	{
		Debug.Log("execute Patrol Behaviour");
		if (Vector3.Distance(transform.position, positions[0]) > 0.1f)
		{//walk to the first place
			transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * moveSpeed);
		}
		else
		{//when there, put the first place last and go to the next place
			positions.Insert(positions.Count, positions[0]);
			positions.RemoveAt(0);
		}
	}
}
