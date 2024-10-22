using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IEnemyMoveable interface for all moveable enemies
public interface IEnemyMoveable
{
    Rigidbody2D RigidBody2d { get; set; }
    void MoveEnemy(Vector2 velocity);
}
