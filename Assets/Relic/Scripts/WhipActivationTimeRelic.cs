using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "WhipActivationTime", menuName = "Relics/WhipActivationTime")]
public class WhipActivationTimeRelic : RelicSO
{

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
        _playerStats.AddStat(_changedStat, 100);
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
