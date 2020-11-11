using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;
	public static GameManager Instance
	{
		get
		{
			return instance;
		}
	}

	Dictionary<int, Boid> boids;
	public GameObject boidPrefab;
	public int boidCount = 20;
	public float minDistance = 10f;
	public float influenceRange = 10f;
	
    void Start()
    {
		if (instance != null && instance != this)
		{
			Destroy(this);
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this);

		boids = new Dictionary<int, Boid>();
		for (int i = 0; i < boidCount; i++)
		{
			boids.Add(i, new Boid(boidPrefab));
		}
	}

    void FixedUpdate()
    {
		Vector3 a = new Vector3(0, 0, 0);
		Vector3 b = new Vector3(0, 0, 0);
		Vector3 c = new Vector3(0, 0, 0);

		for (int i = 0; i < boidCount; i++)
		{
			a += boids[i].GetPosition();
		}

		for (int i = 0; i < boidCount; i++)
		{
			for (int j = 0; j < boidCount; j++)
			{
				if (boids[i] != boids[j])
				{
					//Vector3.Distance(boids[i].GetPosition(), boids[j].GetPosition())
					if ((boids[i].GetPosition().x - boids[j].GetPosition().x) * (boids[i].GetPosition().x - boids[j].GetPosition().x) + (boids[i].GetPosition().y - boids[j].GetPosition().y) * (boids[i].GetPosition().y - boids[j].GetPosition().y) + (boids[i].GetPosition().z - boids[j].GetPosition().z) * (boids[i].GetPosition().z - boids[j].GetPosition().z) < minDistance * minDistance)
					{
						b -= boids[j].GetPosition() - boids[i].GetPosition();
					}
					if ((boids[i].GetPosition().x - boids[j].GetPosition().x) * (boids[i].GetPosition().x - boids[j].GetPosition().x) + (boids[i].GetPosition().y - boids[j].GetPosition().y) * (boids[i].GetPosition().y - boids[j].GetPosition().y) + (boids[i].GetPosition().z - boids[j].GetPosition().z) * (boids[i].GetPosition().z - boids[j].GetPosition().z) < influenceRange * influenceRange)
					{
						c += boids[j].GetVelocity();
					}
				}
			}

			boids[i].SaveBoid(a, b, c);
		}

		for (int i = 0; i < boidCount; i++)
		{
			boids[i].UpdateBoid();
		}
	}
}
