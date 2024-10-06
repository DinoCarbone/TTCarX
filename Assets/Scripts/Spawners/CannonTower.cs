using UnityEngine;

public class CannonTower : BaseTower<CannonProjectile>
{
    [SerializeField] private WeaponRotator weaponRotator;
    [SerializeField] private Transform shootingWeapon;
    private Monster currentMonster;
    private bool canShoot = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        weaponRotator.FireReady += OnShoot;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        weaponRotator.FireReady -= OnShoot;
    }
    protected override void ShootReady(Monster monster)
    {
        canShoot = true;
    }
    private void OnShoot()
    {
        if (!canShoot) return;
        CannonProjectile projectile = GetAndSpawnObject();
        projectile.transform.rotation = shootingWeapon.rotation;
        projectile.Activate();
        canShoot = false;
    }
}
