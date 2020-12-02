using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : AIBehaviour
{
	public override void OnEnter()
	{
		Debug.Log("Enter Hide Behaviour");
	}
	public override void Execute()
	{
		Debug.Log("Execute Hide Behaviour");
	}
}
