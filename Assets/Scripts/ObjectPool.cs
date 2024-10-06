using System;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    private T prefab; 
    private int maxSize;
    private Transform transformSpawn;

    public Action<T> ObjectCreated;

    public void InitObjectPool(T prefab, int initialSize, int maxSize, Transform transformSpawn)
    {
        this.prefab = prefab;
        this.maxSize = maxSize;
        this.transformSpawn = transformSpawn;
        for (int i = 0; i < initialSize; i++)
        {
            T poolObject = UnityEngine.Object.Instantiate(prefab,transformSpawn);
            ObjectCreated?.Invoke(poolObject);
            poolObject.gameObject.SetActive(false);
            pool.Enqueue(poolObject);
        }
    }
    public T GetObject()
    {
        T poolObject = null;
        if (pool.Count > 0)
        {
            poolObject = pool.Dequeue();
        }
        else
        {
            poolObject = UnityEngine.Object.Instantiate(prefab, transformSpawn);
            ObjectCreated?.Invoke(poolObject);
        }
        poolObject.gameObject.SetActive(true);
        return poolObject;
    }
    public void ReturnObject(T poolObject)
    {
        poolObject.gameObject.SetActive(false);
        if (pool.Count < maxSize)
        {
            pool.Enqueue(poolObject);
        }
        else
        {
            UnityEngine.Object.Destroy(poolObject.gameObject);
        }
    }
}
