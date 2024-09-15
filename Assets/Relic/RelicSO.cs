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
        public float percentage = 10f;
    }

    public List<RelicBuff> relicBuffs = new List<RelicBuff>();
    public List<RelicBuff> relicConditions = new List<RelicBuff>();
    [Space]

    public string relicName;
    public string flavorTextDescription;
    public Sprite spriteIcon;

    private bool condMet = false;

    public void ActivateBuff(StatsSO playerStats)
    {
        if (relicConditions.Count > 0) // If there is any conditions
        {
            foreach (RelicBuff cond in relicConditions)
            {
                float condPercentage = cond.percentage / 100;

                if (cond.statType == StatsEnum.MAXHEALTH)
                {
                    float maxHealth = playerStats.GetStat(StatsEnum.MAXHEALTH);
                    float currHealth = playerStats.GetStat(StatsEnum.HEALTH);

                    float percentageHealth = currHealth / maxHealth;

                    if (percentageHealth <= condPercentage)
                    {
                        condMet = true;
                    }

                }
            }

            if (condMet)
            {
                LoopBuffs(playerStats);
            }
        }
        else
        {
            LoopBuffs(playerStats);
        }

    }

    private void LoopBuffs(StatsSO playerStats)
    {
        foreach (RelicBuff buff in relicBuffs)
        {
            float percentage = buff.percentage / 100;
            percentage = percentage + 1;

            float newStat = playerStats.GetStat(buff.statType) * percentage;

            playerStats.SetStat(buff.statType, newStat);

            Debug.Log(playerStats.GetStat(buff.statType));
        }
    }
}
