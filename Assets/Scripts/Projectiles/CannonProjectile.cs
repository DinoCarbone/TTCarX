using UnityEngine;
public class CannonProjectile : BaseProjectile 
{
    [SerializeField] private float lifeTime;
    private float timeAwake;

    private void Awake()
    {
        transform.rotation = Quaternion.identity;
    }
    public void Activate()
    {
        timeAwake = Time.time;
    }
    private void Update()
    {
        if(timeAwake + lifeTime < Time.time)
        {
            DestroySelf();
            return;
        }
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
