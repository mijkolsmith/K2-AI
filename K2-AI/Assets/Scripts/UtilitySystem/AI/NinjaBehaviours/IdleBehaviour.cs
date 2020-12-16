using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : AIBehaviour
{
	public override void OnEnter()
	{
		Debug.Log("Enter Idle Behaviour");
	}
	public override void Execute()
	{
		Debug.Log("Execute Idle Behaviour");
		//idling
	}
}
