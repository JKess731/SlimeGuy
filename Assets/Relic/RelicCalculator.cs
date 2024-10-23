using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCalculator : MonoBehaviour
{
    [SerializeField] public StatsSO playerStats;

    private float originalDefense;

    private void Start()
    {
        originalDefense = playerStats.GetStat(Enum_Stats.DEFENSE);
    }

    #region Health Buff/Debuff
    public void BuffHealth(float percentage)
    {
        playerStats.AddStat(Enum_Stats.HEALTH, playerStats.GetStat(Enum_Stats.MAXHEALTH) * (percentage/100));
    }

    public void BuffHealth(int integer)
    {
        playerStats.AddStat(Enum_Stats.HEALTH, integer);
    }

    public void DebuffHealth(float percentage)
    {
        playerStats.SubtractStat(Enum_Stats.HEALTH, playerStats.GetStat(Enum_Stats.MAXHEALTH) * (percentage / 100));
    }

    public void DebuffHealth(int integer)
    {
        playerStats.SubtractStat(Enum_Stats.HEALTH, integer);
    }
    #endregion

    #region Defense Buff/Debuff

    public void BuffDefense(float percentage)
    {
        playerStats.AddStat(Enum_Stats.DEFENSE, originalDefense * (percentage / 100));
    }

    public void BuffDefense(int integer)
    {
        playerStats.AddStat(Enum_Stats.DEFENSE, integer);
    }

    public void DebuffDefense(float percentage)
    {
        playerStats.SubtractStat(Enum_Stats.HEALTH, originalDefense * (percentage / 100));
    }

    public void DebuffDefense(int integer)
    {
        playerStats.SubtractStat(Enum_Stats.DEFENSE, integer);
    }

    #endregion
}
