using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicShotgun", menuName = "AttackPrefab/BasicShotgun")]
public class BasicShotgun : AttackBehavior
{
    public int bulletCount;
    public float spreadAngle;
    public GameObject projectile;

    public override void Activate(Quaternion rotation, Vector2 attackPosition)
    {
        float angleDiff = spreadAngle * 2 / (bulletCount - 1);
        for (int i = 0; i < bulletCount; i++){
            float addedOffset = -angleDiff * i;
            Quaternion newRot = rotation * Quaternion.Euler(0, 0, spreadAngle) * Quaternion.Euler(0, 0, addedOffset);
            Instantiate(projectile, attackPosition, newRot);
        }
    }   
}
