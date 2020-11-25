using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowBehaviour : AIBehaviour
{
	public override void OnEnter()
	{
		Debug.Log("Enter Follow Behaviour");
	}

	public override void Execute()
	{
		Debug.Log("execute Follow Behaviour");
		//follow the player
	}
}
