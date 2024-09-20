using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Relics/Health Debuff", fileName = "Health Debuff")]
public class HealthDebuffRelic : RelicSO
{
    [SerializeField] private float debuffPercentage = 10;

    public override void Initialize(StatsSO playerstats)
    {
        throw new System.NotImplementedException();
    }

    public override void OnPickup()
    {
        ActivateBuffs();
    }

    public override void ActivateBuffs()
    {
        calc.DebuffHealth(debuffPercentage);
    }

    public override void DeactivateBuffs()
    {
        throw new System.NotImplementedException();
    }

    public override bool Condition()
    {
        return false;
    }
}
