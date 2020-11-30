using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Agent
{
	[SerializeField] FloatValue GuardDistance;
	[SerializeField] FloatValue GuardHealth;
	[SerializeField] public GameObject player;

	public override void OnInitialize()
	{
		GuardDistance = ScriptableObject.CreateInstance<FloatValue>();
		GuardHealth = ScriptableObject.CreateInstance<FloatValue>();
		base.OnInitialize();
	}

	// Update is called once per frame
	protected override void Update()
	{
		GuardDistance.Value = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		//Distance = BlackBoard.GetFloatVariableValue(VariableType.GuardDistance);
		base.Update();
	}

	public override void TakeDamage(float damage)
	{
		Health = BlackBoard.GetFloatVariableValue(VariableType.GuardHealth);
		base.TakeDamage(damage);
	}
}
