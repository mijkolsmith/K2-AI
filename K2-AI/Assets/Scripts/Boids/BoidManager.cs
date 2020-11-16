using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
	private static BoidManager instance;
	public static BoidManager Instance
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
	public GameObject cameraObject;
	public Material selectedMaterial;
	public Material deselectedMaterial;
	public int cameraBoid = 1;
	
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

		boids[cameraBoid].GetBoid().GetComponent<MeshRenderer>().material = selectedMaterial;
		boids[cameraBoid].GetBoid().transform.GetChild(0).GetComponent<MeshRenderer>().material = selectedMaterial;
	}

    void Update()
    {
		Vector3 a = new Vector3(0, 0, 0);
		Vector3 b = new Vector3(0, 0, 0);
		Vector3 c = new Vector3(0, 0, 0);

		CameraBehaviour();
		
		for (int i = 0; i < boidCount; i++)
		{
			for (int j = 0; j < boidCount; j++)
			{
				if (boids[i] != boids[j])
				{
					a = Rule1(a, i, j);

					b = Rule2(b, i ,j);

					c = Rule3(c, i, j);
				}
			}

			boids[i].SaveBoid(a, b, c);
		}

		for (int i = 0; i < boidCount; i++)
		{
			boids[i].UpdateBoid();
		}
	}

	private Vector3 Rule1(Vector3 a, int i, int j)
	{
		a += boids[j].GetPosition();
		a /= boidCount - 1;
		a -= boids[i].GetPosition();
		return a;
	}

	private Vector3 Rule2(Vector3 b, int i, int j)
	{
		//Vector3.Distance(boids[i].GetPosition(), boids[j].GetPosition())
		if ((boids[i].GetPosition().x - boids[j].GetPosition().x) * (boids[i].GetPosition().x - boids[j].GetPosition().x) + (boids[i].GetPosition().y - boids[j].GetPosition().y) * (boids[i].GetPosition().y - boids[j].GetPosition().y) + (boids[i].GetPosition().z - boids[j].GetPosition().z) * (boids[i].GetPosition().z - boids[j].GetPosition().z) < minDistance * minDistance)
		{
			b -= boids[j].GetPosition() - boids[i].GetPosition();
		}
		return b;
	}

	private Vector3 Rule3(Vector3 c, int i, int j)
	{
		//Vector3.Distance(boids[i].GetPosition(), boids[j].GetPosition())
		if ((boids[i].GetPosition().x - boids[j].GetPosition().x) * (boids[i].GetPosition().x - boids[j].GetPosition().x) + (boids[i].GetPosition().y - boids[j].GetPosition().y) * (boids[i].GetPosition().y - boids[j].GetPosition().y) + (boids[i].GetPosition().z - boids[j].GetPosition().z) * (boids[i].GetPosition().z - boids[j].GetPosition().z) < influenceRange * influenceRange)
		{
			c += boids[j].GetVelocity();
		}
		c /= boidCount - 1;
		//you don't have to implement this because I set the velocity in boid instead of adding it
		//c -= boids[i].GetVelocity();
		return c;
	}

	private void CameraBehaviour()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow) && cameraBoid > 1)
		{
			cameraBoid -= 1;
			boids[cameraBoid + 1].GetBoid().GetComponent<MeshRenderer>().material = deselectedMaterial;
			boids[cameraBoid + 1].GetBoid().transform.GetChild(0).GetComponent<MeshRenderer>().material = deselectedMaterial;
			boids[cameraBoid].GetBoid().GetComponent<MeshRenderer>().material = selectedMaterial;
			boids[cameraBoid].GetBoid().transform.GetChild(0).GetComponent<MeshRenderer>().material = selectedMaterial;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow) && cameraBoid <= boidCount)
		{
			cameraBoid += 1;
			boids[cameraBoid - 1].GetBoid().GetComponent<MeshRenderer>().material = deselectedMaterial;
			boids[cameraBoid - 1].GetBoid().transform.GetChild(0).GetComponent<MeshRenderer>().material = deselectedMaterial;
			boids[cameraBoid].GetBoid().GetComponent<MeshRenderer>().material = selectedMaterial;
			boids[cameraBoid].GetBoid().transform.GetChild(0).GetComponent<MeshRenderer>().material = selectedMaterial;
		}

		cameraObject.transform.position = new Vector3(0, boids[cameraBoid].GetPosition().z + 50, boids[cameraBoid].GetPosition().z - 100);
		cameraObject.transform.LookAt(boids[cameraBoid].GetBoid().transform);
	}
}
