using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Health Buff", fileName = "Health Buff")]
public class HealthBuffRelic : RelicSO
{
    [SerializeField] private float buffPercentage = 10;

    public override void OnPickup()
    {
        ActivateBuffs();
    }

    public override void ActivateBuffs()
    {
        calc.BuffHealth(buffPercentage);
    }

    public override void DeactivateBuffs()
    {
        throw new System.NotImplementedException();
    }

    public override bool Condition()
    {
        throw new System.NotImplementedException();
    }
}
