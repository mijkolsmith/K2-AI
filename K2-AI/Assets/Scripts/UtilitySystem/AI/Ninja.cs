using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ninja : Agent
{
	[SerializeField] FloatValue NinjaDistance;
	[SerializeField] FloatValue GuardDistance;
	[SerializeField] FloatValue NinjaHidden;
	[SerializeField] public GameObject player;
	[SerializeField] public GameObject guard;
	public bool isHidden = false;

	public override void OnInitialize()
	{
		base.OnInitialize();
	}

	// Update is called once per frame
	protected override void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, guard.transform.position - transform.position, out hit))
		{
			if (hit.transform.gameObject == guard)
			{
				isHidden = false;
			}
			else isHidden = true;
		}

		NinjaDistance.Value = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		NinjaHidden.Value = Convert.ToSingle(isHidden);
		GuardDistance.Value = guard.GetComponent<BlackBoard>().GetFloatVariableValue(VariableType.GuardDistance).Value;
		
		base.Update();
	}
}
