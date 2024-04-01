using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinionDamageable
{
    void Damage(float dmg);
    void Die();

    GameObject FindNearestEnemy(List<GameObject> enemyList);

    public float maxHealth { get; set; }
    public float health { get; set; }
}
