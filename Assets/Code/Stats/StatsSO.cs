using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Stats", menuName = "Stats/New Stats")]
public class StatsSO : ScriptableObject
{
    //list of stats since only lists can be serialized
    public List<StatInfo> statInfos =  new List<StatInfo>();

    //dictionary of stats
    public Dictionary<Enum_Stat, float> statDictionary = new Dictionary<Enum_Stat, float>();
    public Dictionary<Enum_Stat, float> dictionaryInstance = new Dictionary<Enum_Stat, float>();

    //add stats to dictionary
    public virtual void Initialize()
    {
        foreach (StatInfo statInfo in statInfos)
        {
            statDictionary.Add(statInfo.statType, statInfo.statValue);
        }

        //Clone the dictionary to prevent changes to the original
        dictionaryInstance = new Dictionary<Enum_Stat, float>(statDictionary);
    }

    /// <summary>
    /// Get the value of a stat
    /// </summary>
    /// <param name="stat">The type of stat you want to get</param>
    /// <returns>The value of that stat</returns>
    public float GetStat(Enum_Stat stat)
    {
        if(!statDictionary.TryGetValue(stat, out float value))
        {
            Debug.LogError("Stat not found: " + stat);
            return 0;
        }

        return value;
    }
}
