using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolBehaviour : AIBehaviour
{
	List<Vector3> positions = new List<Vector3>(new Vector3[] { new Vector3(4, 0, 4), new Vector3(-4, 0, 4), new Vector3(4, 0, -4), new Vector3(-4, 0, -4) } );

	public override void OnEnter()
	{
		Debug.Log("Enter Patrol Behaviour");
		//sort the list by closest place first
		positions = positions.OrderBy(position => Vector3.Distance(transform.position, position)).ToList();
	}

	public override void Execute()
	{
		Debug.Log("execute Patrol Behaviour");
		
		if (transform.position != positions[0])
		{//walk to the first place
			transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime);
		}
		else
		{//when there, put the first place last and go to the next place
			positions.Insert(positions.Count - 1, positions[0]);
			positions.RemoveAt(0);
		}
	}
}
