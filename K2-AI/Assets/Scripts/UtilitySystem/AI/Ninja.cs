using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Agent
{
	[SerializeField] FloatValue NinjaDistance;
	[SerializeField] FloatValue NinjaHealth;
	[SerializeField] GameObject player;

	public override void OnInitialize()
	{
		base.OnInitialize();
	}

	// Update is called once per frame
	protected override void Update()
	{
		//NinjaDistance.Value = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		Distance = BlackBoard.GetFloatVariableValue(VariableType.NinjaDistance);
		base.Update();
	}

	public override void TakeDamage(float damage)
	{
		Health = BlackBoard.GetFloatVariableValue(VariableType.NinjaHealth);
		base.TakeDamage(damage);
	}
}
