using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotgunUpgrade", menuName = "Relics/ShotgunUpgrade")]
public class ShotgunUpgrade : RelicSO
{
    public override void Initialize(StatsSO playerstats)
    {
        _playerStats = playerstats;
    }
    public override void ActivateBuffs()
    {
        _playerStats.AddStat(_changedStat, 100);
    }

    public override bool Condition()
    {
        throw new System.NotImplementedException();
    }

    public override void DeactivateBuffs()
    {
        throw new System.NotImplementedException();
    }


    public override void OnPickup()
    {
        base.OnPickup();
    }
}
