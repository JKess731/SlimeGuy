using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Relic", menuName = "Relic/New Relic")]
public class RelicSO : ScriptableObject
{
    [System.Serializable]
    public class RelicBuff
    {
        public StatsEnum statType;
        public float buffPercentage = 10f;
    }

    public List<RelicBuff> relicBuffs = new List<RelicBuff>();
    [Space]

    public string relicName;
    public string flavorTextDescription;
    public Sprite spriteIcon;

    public void ActivateBuff(Stats playerStats)
    {
        foreach (RelicBuff buff in relicBuffs)
        {
            float percentage = buff.buffPercentage / 100;
            percentage = percentage + 1;

            float newStat = playerStats.GetStat(buff.statType) * percentage;

            playerStats.SetStat(buff.statType, newStat);

            Debug.Log(playerStats.GetStat(buff.statType));
        }
    }
}
