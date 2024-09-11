using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicCalculator : MonoBehaviour
{
    [SerializeField] private Stats playerStats;

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

}
