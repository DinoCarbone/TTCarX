using System;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Monster : MonoBehaviour
{
	[Min(0)]
	[SerializeField] private float speed = 0.1f;
	[SerializeField] private int maxHealth = 30;
	private const float MinDistanceToTarget = 0.3f;
	private Vector3 movementTarget;
	private MonsterSpawner spawner;
	private int currentHealth;
	private CapsuleCollider capsuleCollider;
    public Vector3 MiddlePosition { get { return capsuleCollider.bounds.center; } }

	public Action Destroyed;

    private void Awake()
	{
		capsuleCollider = GetComponent<CapsuleCollider>();
    }
	private void OnEnable()
	{
        currentHealth = maxHealth;
    }
	private void Update () 
	{
		if (movementTarget == null)
		{
            Debug.LogError("Movement target is enpty!");
            return;
		}
		
		if (Vector3.Distance (transform.position, movementTarget) <= MinDistanceToTarget) {
			Kill();
            return;
		}
        transform.position = Vector3.MoveTowards(transform.position, movementTarget, speed * Time.deltaTime);
    }
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
            Kill();
        }
	}
	private void Kill()
	{
		if (spawner != null)
		{
            spawner.RecycleObject(this);
			Destroyed?.Invoke();
        }
		else
		{
			Debug.LogError("Spawner is Empty!");
		}
	}
    public void SetTarget(Vector3 movementTarget)
	{
		this.movementTarget = new Vector3(movementTarget.x,0,movementTarget.z);
	}
    public void SetSpawner(MonsterSpawner spawner)
    {
        this.spawner = spawner;
    }
}
