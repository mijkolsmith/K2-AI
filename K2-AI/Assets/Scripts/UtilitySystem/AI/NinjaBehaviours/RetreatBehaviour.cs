using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatBehaviour : AIBehaviour
{
	GameObject player;
	float moveSpeed = 3f;

	public override void OnEnter()
	{
		Debug.Log("Enter Retreat Behaviour");
		player = GetComponent<Guard>().player;
	}

	public override void Execute()
	{
		Debug.Log("execute Retreat behaviour");
		transform.position = Vector3.MoveTowards(transform.position, transform.position - player.transform.position, Time.deltaTime * moveSpeed);
	}
}
