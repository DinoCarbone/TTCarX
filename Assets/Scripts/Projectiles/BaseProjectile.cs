using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class BaseProjectile: MonoBehaviour
{
    [SerializeField] protected float speed = 0.2f;
    [SerializeField] protected int damage = 10;
    protected ISpawner spawner;

    protected void DestroySelf()
    {
        spawner.RecycleObject(this);
    }
    private void OnTriggerEnter(Collider trigger)
    {
        Monster monster = trigger.gameObject.GetComponent<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
            DestroySelf();
        }
    }
    public void SetSpawner(ISpawner spawner)
    {
        this.spawner = spawner;
    }
}
