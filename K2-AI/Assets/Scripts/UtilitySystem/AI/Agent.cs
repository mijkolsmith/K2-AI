using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour, IDamageable
{
    public AIBehaviourSelector AISelector { get; private set; }
    public BlackBoard BlackBoard { get; private set; }
	private FloatValue health;
	private FloatValue distance;
	protected FloatValue Health { private get => health; set => this.health = value; }
	protected FloatValue Distance { private get => distance; set => this.distance = value; }

	// Start is called before the first frame update
	private void Start()
    {
        OnInitialize();
    }

    public virtual void OnInitialize()
    {
        AISelector = GetComponent<AIBehaviourSelector>();
        BlackBoard = GetComponent<BlackBoard>();
        BlackBoard.OnInitialize();
        AISelector.OnInitialize(BlackBoard);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        AISelector.OnUpdate();
        
        distance.Value = transform.position.magnitude;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(10);
		}
	}

    public virtual void TakeDamage(float damage)
    {
        if (health)
        {
            health.Value -= damage;
        }

        AISelector.EvaluateBehaviours();
    }
}

public interface IDamageable
{
    void TakeDamage(float damage);
}