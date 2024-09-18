using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IDamageable interface for all damageable objects
public interface IDamageable
{
    void Damage(float damageAmount, Vector2 hitDirection, float hitForce, Vector2 constantForceDirection);
    void Die();
}
