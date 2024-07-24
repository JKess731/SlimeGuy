using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The wave behavior allows for attacks to spawn child attacks
/// </summary>
[CreateAssetMenu(fileName = "Wave", menuName = "Attack/Wave")]
public class WaveBehavior : AttackBehavior
{
    [Header("Wave Attributes")]
    [SerializeField] GameObject wavePrefab;

    public override void ActivateAttack(Quaternion rotation, Vector2 attackPosition)
    {
        Instantiate(wavePrefab, attackPosition, rotation);
    }
}
