using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Relics/StatBuff", menuName = "Relics/Stat Buff (NO HEALTH)")]
public class StatPercentageBuffRelic : RelicSO
{
    [SerializeField] private float buffPercentage = 10;
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
        float statVal = _playerStats.GetStat(_changedStat);
        float buffVal = buffPercentage / 100;

        float newVal = statVal + buffVal;

        _playerStats.AddStat(_changedStat, newVal);
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
