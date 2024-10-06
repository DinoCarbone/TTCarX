using System.Collections;
using UnityEngine;
public class MonsterSpawner : BaseSpawner<Monster>
{
	[SerializeField] private Transform movementTarget;

    private void Start()
    {
        StartCoroutine(SpawnMonstersCoroutine());
    }
    protected override void InitializeObject(Monster monster)
    {
        monster.SetSpawner(this);
    }

    private IEnumerator SpawnMonstersCoroutine()
    {
        while (true)
        {
            Monster monster = GetAndSpawnObject();
            monster.SetTarget(movementTarget.position);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
