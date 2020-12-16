using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : AIBehaviour
{
	Player player;
	float moveSpeed = 2f;
	float attackDistance = 2f;
	float attackTimer = 0f;

	public override void OnEnter()
	{
		Debug.Log("Enter Attack Behaviour");
		player = GetComponent<Guard>().player.GetComponent<Player>();
	}

	public override void Execute()
    {
		attackTimer += Time.deltaTime;

        Debug.Log("Execute Attack behaviour");
		if (Vector3.Distance(transform.position, player.transform.position) > attackDistance)
		{//if distance is bigger than the attackdistance
			//go towards the player
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
		}
		if (Vector3.Distance(transform.position, player.transform.position) < attackDistance && attackTimer >= 3f)
		{//if distance is smaller than the followdistance
			player.TakeDamage(10);
			attackTimer = 0;
		}
	}
}