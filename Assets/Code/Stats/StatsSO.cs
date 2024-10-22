using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object to store stats
/// </summary>
[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Stats")]
public class StatsSO: ScriptableObject
{
    [SerializeField]private List<StatsInfo> _statList= new List<StatsInfo>();
    private Dictionary<Enum_Stats, float> _stats = new Dictionary<Enum_Stats, float>(); 
    public List<StatsInfo> StatList { get => _statList; }
    /// <summary>
    /// Adds all stats to dictionary
    /// </summary>
    public void Initialize()
    {
        for(int i = 0; i < _statList.Count; i++) {
            if (_stats.ContainsKey(_statList[i].Stat))
            {
                //Debug.LogWarning("Duplicate Stat Found");
                continue;
            }
            _stats.Add(_statList[i].Stat, _statList[i].Value);
        }
    }

    public float GetStat(Enum_Stats stat)
    {
        if (_stats.TryGetValue(stat,out float value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning("Stat not found: " + stat.ToString());
            return 0;
        }
    }

    public void Copy(List<StatsInfo> statList)
    {
        for (int i = 0; i < statList.Count; i++)
        {
            _stats.Add(_statList[i].Stat, _statList[i].Value);
        }
    }

    public void SetStat(Enum_Stats stat, float value)
    {
        _stats[stat] = value;
    }

    public void AddStat(Enum_Stats stat, float value)
    {
        _stats[stat] += value;
    }

    public void AddStat(Enum_Stats stat, float value, AbilitySOBase ability)
    {
        _stats[stat] += value;
    }

    public void SubtractStat(Enum_Stats stat, float value)
    {
        _stats[stat] -= value;
    }
}
/// <summary>
/// Serializable class to store statInfo for list and dictionary
/// </summary>
[Serializable]
public class StatsInfo
{
    [SerializeField] private Enum_Stats _stat;
    [SerializeField] private float _statValue;

    public Enum_Stats Stat { get => _stat; }
    public float Value { get => _statValue;}
}
