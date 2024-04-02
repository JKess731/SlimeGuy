using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount, Vector2 hitDirection, float hitForce, Vector2 constantForceDirection, float inputDirection);

    void Die();

    float maxHealth { get; set; }
    float currentHealth { get; set; }
}
