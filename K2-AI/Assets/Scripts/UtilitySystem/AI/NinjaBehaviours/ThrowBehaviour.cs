using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : AIBehaviour
{
	Ninja ninja;

	public override void OnEnter()
	{
		Debug.Log("Enter Throw Behaviour");
		ninja = GetComponent<Ninja>();
	}

	public override void Execute()
	{
		Debug.Log("execute Throw Behaviour");
		//throw smoke bomb
		ninja.smokeThrown = true;
	}
}
