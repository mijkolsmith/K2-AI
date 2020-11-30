using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Agent
{
	[SerializeField] FloatValue NinjaDistance;
	[SerializeField] FloatValue GuardDistance;
	[SerializeField] public GameObject player;
	[SerializeField] public GameObject guard;

	public override void OnInitialize()
	{
		NinjaDistance = ScriptableObject.CreateInstance<FloatValue>();
		base.OnInitialize();
	}

	// Update is called once per frame
	protected override void Update()
	{
		NinjaDistance.Value = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		GuardDistance = guard.GetComponent<BlackBoard>().GetFloatVariableValue(VariableType.GuardDistance);
		//Distance = BlackBoard.GetFloatVariableValue(VariableType.NinjaDistance);
		base.Update();
	}
}
