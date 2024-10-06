using UnityEngine;
public class GuidedProjectile : BaseProjectile
{
    private Monster monster;

    public void SetMonster(Monster monster)
    {
        this.monster = monster;
        monster.Destroyed += OnDestroyedMonster;
    }
    private void OnDestroyedMonster()
    {
        DestroySelf();
    }
    private void OnDisable()
    {
        if (monster != null) monster.Destroyed -= OnDestroyedMonster;
    }
    private void Update()
    {
        if (monster == null)
        {
            DestroySelf();
            Debug.LogError("Movement target is enpty!");
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, monster.MiddlePosition, speed * Time.deltaTime);
    }
}
