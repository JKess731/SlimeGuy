using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relics/StatBuff", menuName = "Relics/Multiplicative")]
public class StatPercentageBuffRelic : RelicSO
{
    [SerializeField] private float buffPercentage = 15;
    public override void Initialize(StatsSO playerstats)
    {
        _playerStats = playerstats;
    }

    public override void OnPickup()
    {
        ActivateBuffs();
    }

    public override void ActivateBuffs()
    {
        _playerStats.RegisterStat(_changedStat, "Multiply", buffPercentage / 100);
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
