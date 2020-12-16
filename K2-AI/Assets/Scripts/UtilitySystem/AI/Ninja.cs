using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ninja : Agent
{
	[SerializeField] FloatValue NinjaDistance;
	[SerializeField] FloatValue NinjaHidden;
	[SerializeField] FloatValue GuardDistance;
	[SerializeField] FloatValue GuardRaycast;
	[SerializeField] public GameObject player;
	[SerializeField] public GameObject guard;
	[SerializeField] public LayerMask guardMask;
	public bool isHidden = false;
	public bool smokeThrown = false;
	public float smokeTimer = 0;

	public override void OnInitialize()
	{
		base.OnInitialize();
	}

	// Update is called once per frame
	protected override void Update()
	{
		if (smokeThrown == true)
		{
			smokeTimer += Time.deltaTime;

			if (smokeTimer >= 5)
			{
				smokeThrown = false;
				smokeTimer = 0;
			}
		}

		RaycastHit hit;
		if (guard != null)
		{
			Debug.DrawRay(transform.position, guard.transform.position - transform.position, Color.green);
			if (Physics.Raycast(transform.position, guard.transform.position - transform.position, out hit, 50, guardMask))
			{
				if (hit.transform == guard.transform)
				{
					isHidden = false;
				}
				else isHidden = true;
			}
		}
		else isHidden = true;

		NinjaDistance.Value = Vector3.Distance(transform.localPosition, player.transform.localPosition);
		NinjaHidden.Value = Convert.ToSingle(isHidden);
		GuardDistance.Value = guard.GetComponent<BlackBoard>().GetFloatVariableValue(VariableType.GuardDistance).Value;
		
		base.Update();
	}
}
