using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HideBehaviour : AIBehaviour
{
	GameObject guard;
	GameObject player;
	float moveSpeed = 3f;
	List<Vector3> hidePositions = new List<Vector3>(new Vector3[] { new Vector3(8, 1.5f, 3), new Vector3(-8, 1.5f, 3), new Vector3(0, 1.5f, -8) });
	List<Vector3> cornerPositions = new List<Vector3>(new Vector3[] { new Vector3(8, 1.5f, -8), new Vector3(-8, 1.5f, -8) });
	Vector3 hidePosition;
	
	private RaycastHit hit;
	LayerMask guardMask;

	bool foundPos = false;
	bool cornerReached = false;
	bool isHidden;

	public override void OnEnter()
	{
		Debug.Log("Enter Hide Behaviour");
		guard = GetComponent<Ninja>().guard;
		player = GetComponent<Ninja>().player;
		guardMask = GetComponent<Ninja>().guardMask;

		hidePositions = hidePositions.OrderBy(position => Vector3.Distance(transform.position, position)).ToList();
		cornerPositions = cornerPositions.OrderBy(position => Vector3.Distance(transform.position, position)).ToList();
	}

	public override void Execute()
	{
		Debug.Log("Execute Hide Behaviour");
		foreach (Vector3 position in hidePositions)
		{
			Debug.DrawRay(guard.transform.position, position - guard.transform.position, Color.grey);
		}

		isHidden = GetComponent<Ninja>().isHidden;
		if (!isHidden && Vector3.Distance(transform.position, hidePosition) < 1f)
		{
			foundPos = false;
			cornerReached = false;
		}

		if (!foundPos)
		{
			foreach (Vector3 position in hidePositions)
			{
				if (Physics.Raycast(guard.transform.position, position - guard.transform.position, out hit, 50f, guardMask) && !foundPos)
				{
					if (hit.transform != guard.transform)
					{
						hidePosition = position;
						foundPos = true;
						cornerPositions = cornerPositions.OrderBy(pos => Vector3.Distance(hidePosition, pos)).ToList();
					}
				}
			}
		}

		if (!cornerReached && Vector3.Distance(transform.position, cornerPositions[0]) > 1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, cornerPositions[0], Time.deltaTime * moveSpeed);
			if (Vector3.Distance(transform.position, cornerPositions[0]) <= 1f)
			{
				cornerReached = true;
			}
		}
		else if (cornerReached)
		{
			transform.position = Vector3.MoveTowards(transform.position, hidePosition, Time.deltaTime * moveSpeed);
		}
	}
}
