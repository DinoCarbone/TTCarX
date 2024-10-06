
public class HomingTower : BaseTower<GuidedProjectile>
{
    protected override void ShootReady(Monster monster)
    {
        GuidedProjectile projectile = GetAndSpawnObject();
        projectile.SetMonster(monster);
    }
}
