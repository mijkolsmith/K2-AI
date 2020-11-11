using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid
{
	private Vector3 v1, v2, v3;
	private GameObject boid;
	private Vector3 position;
	private Vector3 velocity;
	private Vector3 percievedVelocity;
	private float centreOfMassWeight = .5f;
	private float distanceWeight = 1f;
	private float velocityWeight = 1f;
	private float speed = 10f;
    
	public Boid(GameObject _boidPrefab)
	{
		position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
		velocity = new Vector3(0, 0, 0);
		percievedVelocity = new Vector3(0, 0, 0);
		boid = _boidPrefab;
		boid = Object.Instantiate(boid, position, Quaternion.identity);
	}

	public void SaveBoid(Vector3 a, Vector3 b, Vector3 c)
	{
		//rule 1: centre of mass
		a -= position;
		Vector3 centreOfMass = a / (GameManager.Instance.boidCount - 1);

		v1 = centreOfMass - position;

		//rule 2: social distancing
		v2 = b;

		//rule 3: match velocity
		Vector3 newVelocity = c / (GameManager.Instance.boidCount - 1);
		v3 = newVelocity;
	}

	public void UpdateBoid()
	{
		//implement the rules
		velocity = centreOfMassWeight * v1 + distanceWeight * v2 + velocityWeight * v3;
		velocity = velocity.normalized;
		position += velocity * Time.deltaTime * speed;
		boid.transform.position = position;
		boid.transform.rotation = Quaternion.LookRotation(velocity);
	}

	public Vector3 GetPosition()
	{
		return position;
	}

	public Vector3 GetVelocity()
	{
		return velocity;
	}

	public Vector3 GetPercievedVelocity()
	{
		return percievedVelocity;
	}
}
