using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
	[SerializeField] int health = 100;

	public void TakeDamage(float damage)
	{
		if (health >= 0)
		{
			health -= (int)damage;
			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}
