using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinionMoveable
{
    Rigidbody2D rb { get; set; }
    bool isMovingToEnemy { get; set; }
    float lookDistance { get; set; }
    
    void MoveMinion(Vector2 vector);
}
