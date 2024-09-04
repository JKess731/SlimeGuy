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
    public List<RelicBuff> relicConditions = new List<RelicBuff>();
    [Space]

    public string relicName;
    public string flavorTextDescription;
    public Sprite spriteIcon;
    private bool condMet = true;


    public void ActivateBuff(Stats playerStats)
    {

        foreach (RelicBuff buff in relicConditions)
        {

            if(playerStats.GetStat(buff.statType) !<= (playerStats.GetStat(buff.statType) * (1-buff.buffPercentage)))
            {
                Debug.Log(buff.statType);
                condMet = false;
            }

        }
        Debug.Log(condMet);
        if(condMet == true)
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
}
