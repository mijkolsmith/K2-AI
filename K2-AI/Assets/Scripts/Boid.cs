using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid
{
	Vector3 v1, v2, v3;
	GameObject boid;
	Vector3 Position;
    
	public Boid(GameObject _boidPrefab)
	{
		Position = new Vector3(0, 0, 0);
		Object.Instantiate(_boidPrefab, Position, Quaternion.identity);
		boid = _boidPrefab;
	}

	public void UpdateBoid(Vector3 a, Vector3 b, Vector3 c)
	{
		//rule 1: centre of mass
		Position = boid.transform.localPosition;
		a -= Position;
		Vector3 centreOfMass = a / (GameManager.Instance.boidCount - 1);

		v1 = centreOfMass;

		//rule 2: social distancing
		v2 = b;

		//rule 3: match velocity
		v3 = c;
	}

	public Vector3 GetPosition()
	{
		return Position;
	}
}
