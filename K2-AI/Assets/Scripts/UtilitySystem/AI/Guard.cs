using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Guard : Agent
{
	[SerializeField] FloatValue GuardDistance;
	[SerializeField] FloatValue GuardHealth;
	[SerializeField] FloatValue GuardWeapon;
	[SerializeField] FloatValue GuardRaycast;
	[SerializeField] public GameObject player;
	[SerializeField] public GameObject weapon;
	[SerializeField] public GameObject ninja;
	[SerializeField] public LayerMask playerMask;
	public bool hasWeapon = false;
	public bool canSeePlayer = false;
	public bool isAttacking = false;

	public override void OnInitialize()
	{
		base.OnInitialize();
	}

	// Update is called once per frame
	protected override void Update()
	{
		RaycastHit hit;
		if (player != null && ninja.GetComponent<Ninja>().smokeThrown == false)
		{
			Debug.DrawRay(transform.position, new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z), Color.red);
			if (Physics.Raycast(transform.position, new Vector3(player.transform.position.x - transform.position.x, transform.position.y, player.transform.position.z - transform.position.z), out hit, 50, playerMask))
			{
				canSeePlayer = false;
			}
			else canSeePlayer = true;
		}
		else canSeePlayer = false;

		if (Input.GetButtonDown("Fire1"))
		{
			Debug.Log("oof!");
			TakeDamage(10);
		}

		GuardDistance.Value = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		GuardWeapon.Value = Convert.ToSingle(hasWeapon);
		GuardRaycast.Value = Convert.ToSingle(canSeePlayer);

		base.Update();
	}

	public override void TakeDamage(float damage)
	{
		Health = BlackBoard.GetFloatVariableValue(VariableType.GuardHealth);
		base.TakeDamage(damage);
	}
}
