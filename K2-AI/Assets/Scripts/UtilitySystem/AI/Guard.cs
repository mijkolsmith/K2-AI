using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Guard : Agent
{
	[SerializeField] FloatValue GuardDistance;
	[SerializeField] FloatValue GuardHealth;
	[SerializeField] FloatValue GuardWeapon;
	[SerializeField] public GameObject player;
	[SerializeField] public GameObject weapon;
	public bool hasWeapon = false;
	public bool canSeePlayer = false;

	public override void OnInitialize()
	{
		base.OnInitialize();
	}

	// Update is called once per frame
	protected override void Update()
	{
		/*RaycastHit hit;
		Debug.DrawRay(transform.position, new Vector3(player.transform.position.x - transform.position.x, transform.position.y, player.transform.position.z - transform.position.z), Color.red);
		if (Physics.Raycast(transform.position, new Vector3(player.transform.position.x - transform.position.x, transform.position.y, player.transform.position.z - transform.position.z), out hit))
		{
			if (hit.transform.gameObject == player)
			{
				canSeePlayer = true;
			}
			else canSeePlayer = false;
		}*/

		Debug.Log(canSeePlayer);
		GuardDistance.Value = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		GuardWeapon.Value = Convert.ToSingle(hasWeapon);

		base.Update();
	}

	public override void TakeDamage(float damage)
	{
		Health = BlackBoard.GetFloatVariableValue(VariableType.GuardHealth);
		base.TakeDamage(damage);
	}
}
