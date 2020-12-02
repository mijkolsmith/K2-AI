using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeaponBehaviour : AIBehaviour
{
	GameObject weapon;
	float moveSpeed = 3f;
	float grabDistance = 2f;

	public override void OnEnter()
	{
		Debug.Log("Enter GetWeapon Behaviour");
		weapon = GetComponent<Guard>().weapon;
	}

	public override void Execute()
    {
        Debug.Log("execute GetWeapon behaviour");
		//go towards the weapon
		if (weapon != null)
		{
			transform.position = Vector3.MoveTowards(transform.position, weapon.transform.position, Time.deltaTime * moveSpeed);
			if (Vector3.Distance(transform.position, weapon.transform.position) < grabDistance)
			{//if distance is smaller than the grabdistance
				GetComponent<Guard>().hasWeapon = true;
				Destroy(weapon);
			}
		}
	}
}