using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackBoomerangProjectile", menuName = "EnemyLogic/AttackLogic/BoomerangProjectile")]

public class EnemyAttackBoomerangProjectile : EnemyAttackSOBase
{
    [SerializeField] private Rigidbody2D bulletPrefab;
}
