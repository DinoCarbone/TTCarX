using System;
using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField] private float minAngleX = 90;
    [SerializeField] private float maxAngleX = -90;
    [SerializeField] private float readyToShootThreshold = 1.0f;
    [SerializeField] private Transform rotationToolX;
    [SerializeField] private Transform rotationToolY;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private MonsterZoneTrigger monsterTrigger;

    public Action FireReady;

    private void OnEnable()
    {
        monsterTrigger.MonsterZoneStay += OnMonsterZoneStay;
    }
    private void OnDisable()
    {
        monsterTrigger.MonsterZoneStay -= OnMonsterZoneStay;
    }
    private void OnMonsterZoneStay(Monster monster)
    {
        Vector3 direction = monster.transform.position - rotationToolX.position;

        float targetAngleX = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        float currentAngleX = Mathf.LerpAngle(rotationToolX.eulerAngles.x, -targetAngleX, Time.deltaTime * rotationSpeed);
        float clampedAngleY = Mathf.Clamp(currentAngleX, minAngleX, maxAngleX);
        rotationToolX.rotation = Quaternion.Euler(clampedAngleY, rotationToolX.eulerAngles.y, rotationToolX.eulerAngles.z);

        float targetAngleY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float currentAngleY = Mathf.LerpAngle(rotationToolY.eulerAngles.y, targetAngleY, Time.deltaTime * rotationSpeed);
        rotationToolY.rotation = Quaternion.Euler(rotationToolY.eulerAngles.x, currentAngleY, rotationToolY.eulerAngles.z);

        bool isXAligned = Mathf.Abs(rotationToolX.eulerAngles.x - (-targetAngleX)) <= readyToShootThreshold;
        bool isYAligned = Mathf.Abs(rotationToolY.eulerAngles.y - targetAngleY) <= readyToShootThreshold;

        if (isXAligned && isYAligned)
        {
            FireReady?.Invoke();
        }
    }
}
