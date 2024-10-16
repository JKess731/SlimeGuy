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
    private Dictionary<StatsEnum, float> _stats = new Dictionary<StatsEnum, float>(); 
    public List<StatsInfo> StatList { get => _statList; }
    /// <summary>
    /// Adds all stats to dictionary
    /// </summary>
    public void Initialize()
    {
        for(int i = 0; i < _statList.Count; i++) {
            if (_stats.ContainsKey(_statList[i].StatEnum))
            {
                //Debug.LogWarning("Duplicate Stat Found");
                continue;
            }
            _stats.Add(_statList[i].StatEnum, _statList[i].Value);
        }
    }

    public float GetStat(StatsEnum stat)
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
            _stats.Add(_statList[i].StatEnum, _statList[i].Value);
        }
    }

    public void SetStat(StatsEnum stat, float value)
    {
        _stats[stat] = value;
    }

    public void AddStat(StatsEnum stat, float value)
    {
        _stats[stat] += value;
    }

    public void AddStat(StatsEnum stat, float value, AbilitySOBase ability)
    {
        _stats[stat] += value;
    }

    public void SubtractStat(StatsEnum stat, float value)
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
    [SerializeField] private StatsEnum _statEnum;
    [SerializeField] private float _statValue;

    public StatsEnum StatEnum { get => _statEnum; }
    public float Value { get => _statValue;}
}

public enum StatsEnum
{
    //Add stats here
    //----------------------------------------------------------------

    //Base Stats
    ATTACK,
    DEFENSE,
    SPEED,
    HEALTH,
    MAXHEALTH,

    //Projectile Attack Stats
    BULLET_COUNT,
    ABSORPTION,
    RICHOCHET_COUNT,
    PIERCING_COUNT,
    PROJECTILE_KNOCKBACK,
    SPREAD_ANGLE,
    PROJECTILE_DAMAGE,
    PROJECTILE_SPEED,
    PROJECTILE_RANGE,


    //Ability Stats
    ACTIVATION_TIME,
    COOLDOWN_TIME,
    STATUS,

    //Physical Attack Stats
    DAMAGE,
    KNOCKBACK,
    ROTATION_SPEED,
}
