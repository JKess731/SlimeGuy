using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCalculator : MonoBehaviour
{
    [SerializeField] public StatsSO playerStats;

    private float originalDefense;

    private void Start()
    {
        originalDefense = playerStats.GetStat(StatsEnum.DEFENSE);
    }

    #region Health Buff/Debuff
    public void BuffHealth(float percentage)
    {
        playerStats.AddStat(StatsEnum.HEALTH, playerStats.GetStat(StatsEnum.MAXHEALTH) * (percentage/100));
    }

    public void BuffHealth(int integer)
    {
        playerStats.AddStat(StatsEnum.HEALTH, integer);
    }

    public void DebuffHealth(float percentage)
    {
        playerStats.SubtractStat(StatsEnum.HEALTH, playerStats.GetStat(StatsEnum.MAXHEALTH) * (percentage / 100));
    }

    public void DebuffHealth(int integer)
    {
        playerStats.SubtractStat(StatsEnum.HEALTH, integer);
    }
    #endregion

    #region Defense Buff/Debuff

    public void BuffDefense(float percentage)
    {
        playerStats.AddStat(StatsEnum.DEFENSE, originalDefense * (percentage / 100));
    }

    public void BuffDefense(int integer)
    {
        playerStats.AddStat(StatsEnum.DEFENSE, integer);
    }

    public void DebuffDefense(float percentage)
    {
        playerStats.SubtractStat(StatsEnum.HEALTH, originalDefense * (percentage / 100));
    }

    public void DebuffDefense(int integer)
    {
        playerStats.SubtractStat(StatsEnum.DEFENSE, integer);
    }

    #endregion
}
