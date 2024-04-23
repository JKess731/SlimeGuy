using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapRelic : Relic
{
    [SerializeField] private Enum_Stat buff;
    [SerializeField] private Enum_Stat debuff;
    [SerializeField] private float buffAmount;
    [SerializeField] private float debuffAmount;
    override public void Pickup()
    {
        base.Pickup();
        BuffStat();
        DebuffStat();
        Destroy(gameObject);
    }
    /// <summary>
    /// Buffs the player's stats
    /// </summary>
    private void BuffStat()
    {
        switch (buff)
        {
            case Enum_Stat.HEALTH:
                PlayerStats.instance.IncreaseMaxHealth(buffAmount);
                break;
            case Enum_Stat.ATTACK:
                PlayerStats.instance.IncreaseAttack(buffAmount);
                break;
            case Enum_Stat.ATTACKSPEED:
                PlayerStats.instance.IncreaseAttackSpeed(buffAmount);
                break;
            case Enum_Stat.DEFENSE:
                PlayerStats.instance.IncreaseDefense(buffAmount);
                break;
            case Enum_Stat.SPEED:
                PlayerStats.instance.IncreaseSpeed(buffAmount);
                break;
        }
    }
    /// <summary>
    /// Debuffs the player's stats
    /// </summary>
    private void DebuffStat()
    {
        switch (debuff)
        {
            case Enum_Stat.HEALTH:
                PlayerStats.instance.DecreaseMaxHealth(debuffAmount);
                break;
            case Enum_Stat.ATTACK:
                PlayerStats.instance.DecreaseAttack(debuffAmount);
                break;
            case Enum_Stat.ATTACKSPEED:
                PlayerStats.instance.DecreaseAttackSpeed(debuffAmount);
                break;
            case Enum_Stat.DEFENSE:
                PlayerStats.instance.DecreaseDefense(debuffAmount);
                break;
            case Enum_Stat.SPEED:
                PlayerStats.instance.DecreaseSpeed(debuffAmount);
                break;
        }
    }
}
