using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : AIBehaviour
{
	public override void OnEnter()
	{
		Debug.Log("Enter Throw Behaviour");
	}
	public override void Execute()
	{
		Debug.Log("execute Throw Behaviour");
		//throw smoke bomb
	}
}
