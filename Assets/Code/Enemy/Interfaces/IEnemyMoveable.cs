using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IEnemyMoveable interface for all moveable enemies
public interface IEnemyMoveable
{
    Rigidbody2D RB { get; set; }

    bool isFacingRight { get; set; }

    void MoveEnemy(Vector2 velocity);

    void CheckLeftOrRightFacing(Vector2 velocity);
}
