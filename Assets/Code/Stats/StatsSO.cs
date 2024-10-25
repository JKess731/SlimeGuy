using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// Scriptable object to store stats
/// </summary>
[CreateAssetMenu(fileName = "Stats", menuName = "Stats/Stats")]
public class StatsSO: ScriptableObject
{
    [SerializeField]private List<StatsInfo> _statList= new List<StatsInfo>();
    private Dictionary<Enum_Stats, float> _stats = new Dictionary<Enum_Stats, float>();
    private Dictionary<Enum_Stats, float> _statsAdditive = new Dictionary<Enum_Stats, float>();
    private Dictionary<Enum_Stats, float> _statsMultiplicative = new Dictionary<Enum_Stats, float>();

    public List<StatsInfo> StatList { get => _statList; }
    /// <summary>
    /// Adds all stats to dictionary
    /// </summary>
    public void Initialize()
    {
        for(int i = 0; i < _statList.Count; i++) {
            if (_stats.ContainsKey(_statList[i].StatType))
            {
                //Debug.LogWarning("Duplicate Stat Found");
                continue;
            }
            _stats.Add(_statList[i].StatType, _statList[i].Value);
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
            _stats.Add(_statList[i].StatType, _statList[i].Value);
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

    public void SubtractStat(Enum_Stats stat, float value)
    {
        _stats[stat] -= value;
    }






    public void SetStatAdditive(Enum_Stats stat, float value)
    {
        _statsAdditive[stat] = value;
    }

    public void AddStatAdditive(Enum_Stats stat, float value)
    {
        _statsAdditive[stat] += value;
    }





    public void SetStatMultiplicative(Enum_Stats stat, float value)
    {
        _statsMultiplicative[stat] = value;
    }

    public void AddStatMultiplicative(Enum_Stats stat, float value)
    {
        _statsMultiplicative[stat] += value;
        Debug.Log(stat + "'s value in _statsMultiplicative: " + _statsMultiplicative[stat]);
        Debug.Log("bboo");
    }




    public float ModifiedStatValue(Enum_Stats stat)
    {
        //(BaseState + StatIncrement) * (1+StatMultipler)

        //initialize all zeroes inside the dicts instead

        if (_statsMultiplicative.ContainsKey(stat) && !_statsAdditive.ContainsKey(stat))
        {
            Debug.Log("I got here ----");
            return _stats[stat] * (1 + _statsMultiplicative[stat]);
            
        }
        else if (!_statsMultiplicative.ContainsKey(stat) && _statsAdditive.ContainsKey(stat))
        {
            return (_stats[stat] + _statsAdditive[stat]);
        }
        else if (!_statsMultiplicative.ContainsKey(stat) && !_statsAdditive.ContainsKey(stat))
        {
            return (_stats[stat]);
        }
        else
        {
            return (_stats[stat] + _statsAdditive[stat]) * (1 + _statsMultiplicative[stat]);
        }
    }

    public void RegisterStat(Enum_Stats stat, string type, float value)
    {
        if(type == "Add")
        {
            if (_statsAdditive.ContainsKey(stat))
            {
                AddStatAdditive(stat, value);
            }
            else
            {
                _statsAdditive.Add(stat, value);
            }
        }
        if(type == "Multiply")
        {
            if (_statsMultiplicative.ContainsKey(stat))
            {
                AddStatMultiplicative(stat, value);
            }
            else
            {
                _statsMultiplicative.Add(stat, value);
            }
        }
    }




}
/// <summary>
/// Serializable class to store statInfo for list and dictionary
/// </summary>
[Serializable]
public class StatsInfo
{
    [SerializeField] private Enum_Stats _statType;
    [SerializeField] private float _statValue;

    public Enum_Stats StatType { get => _statType; }
    public float Value { get => _statValue;}
}
