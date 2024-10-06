using UnityEngine;
public abstract class BaseSpawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected Transform spawnPoint;
    [Min(0)]
    [SerializeField] protected float spawnInterval;
    [SerializeField] private T prefab;
    [Min(1)]
    [SerializeField] private int initialSize = 5;
    [Min(1)]
    [SerializeField] private int maxSize = 10;
    [SerializeField] private Transform objectParent;//для удобства спавна в конкретном объекте

    protected ObjectPool<T> objectPool = new ObjectPool<T>();

    private void Awake()
    {
        objectPool.ObjectCreated += InitializeObject;
        objectPool.InitObjectPool(prefab, initialSize, maxSize, objectParent);
    }

    private void OnDisable()
    {
        objectPool.ObjectCreated -= InitializeObject;
    }
    protected T GetAndSpawnObject()
    {
        T obj = objectPool.GetObject();
        obj.transform.position = spawnPoint.position;
        return obj;
    }
    protected abstract void InitializeObject(T obj);
    public void RecycleObject(T obj)
    {
        objectPool.ReturnObject(obj);
    }
}
