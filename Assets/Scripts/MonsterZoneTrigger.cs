using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MonsterZoneTrigger : MonoBehaviour
{
    public Action<Monster> MonsterZoneStay;
    private void OnTriggerStay(Collider trigger)
    {
        Monster monster = trigger.GetComponent<Monster>();
        if (monster != null)
        {
            MonsterZoneStay?.Invoke(monster);
        }
    }
}
