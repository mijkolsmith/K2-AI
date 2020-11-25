using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : AIBehaviour
{
	public override void OnEnter()
	{
		Debug.Log("Enter Attack Behaviour");
	}
	public override void Execute()
    {
        Debug.Log("Execute Attack behaviour");
    }
}