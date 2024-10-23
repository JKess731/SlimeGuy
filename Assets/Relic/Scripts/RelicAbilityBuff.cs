using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "RelicAbilityBuff", menuName = "Relics/RelicAbilityBuff")]
public class RelicAbilityBuff : RelicSO
{
    [SerializeField] float changeStatBy;

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
        _playerStats.RegisterStat(changedStat, "Add", changeStatBy);
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
