using UnityEngine;
[SelectionBase]
public abstract class BaseTower<T> : BaseSpawner<T>, ISpawner where T : BaseProjectile
{
    [SerializeField] private MonsterZoneTrigger monsterTrigger;
    private float lastShootTime;

    protected virtual void OnEnable()
    {
        monsterTrigger.MonsterZoneStay += OnMonsterZoneStay;
    }
    protected virtual void OnDisable()
    {
        monsterTrigger.MonsterZoneStay -= OnMonsterZoneStay;
    }
    private void OnMonsterZoneStay(Monster monster)
    {
        if(Time.time - lastShootTime >= spawnInterval)
        {
            ShootReady(monster);
            lastShootTime = Time.time;
        }
    }
    protected abstract void ShootReady(Monster monster);
    public void RecycleObject(BaseProjectile projectile)
    {
        T typedProjectile = projectile as T;
        if (typedProjectile != null)
        {
            objectPool.ReturnObject(typedProjectile);
        }
        else
        {
            Debug.LogError($"{projectile} is not a {typeof(T).Name}!");
        }
    }

    protected override void InitializeObject(T projectile)
    {
        projectile.SetSpawner(this);
    }
}
