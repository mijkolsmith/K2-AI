using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : AIBehaviour
{
	GameObject player;
	float followSpeed = 3f;
	float followDistance = 2f;

	public override void OnEnter()
	{
		Debug.Log("Enter Attack Behaviour");
		player = GetComponent<Guard>().player;
	}

	public override void Execute()
    {
        Debug.Log("Execute Attack behaviour");
		if (Vector3.Distance(transform.position, player.transform.position) > followDistance)
		{//if distance is bigger than the followdistance
		 //follow the player
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * followSpeed);
		}
	}
}