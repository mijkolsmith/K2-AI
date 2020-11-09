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

    void Update()
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
					if (Mathf.Abs(boids[i].GetPosition().x + boids[j].GetPosition().x) < 10 || Mathf.Abs(boids[i].GetPosition().y + boids[j].GetPosition().y) < 10 || Mathf.Abs(boids[i].GetPosition().z + boids[j].GetPosition().z) < 10)
					{
						b = b - (boids[i].GetPosition() + boids[j].GetPosition());
					}
				}
			}

			boids[i].UpdateBoid(a, b, c);
		}
	}
}
