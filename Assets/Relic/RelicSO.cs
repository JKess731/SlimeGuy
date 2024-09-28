using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RelicSO : ScriptableObject
{
    [SerializeField] protected StatsSO _playerStats;
    [SerializeField] protected string relicName;
    [SerializeField] protected string flavorTextDescription;
    [SerializeField] protected RelicRarity rarity;
    [SerializeField] protected Sprite spriteIcon;
    [SerializeField] protected StatsEnum _changedStat;

    [SerializeField] protected bool hasCondition = false;

    [HideInInspector] public RelicCalculator calc;

    public string Name { get { return name; } }
    public string Description {  get { return flavorTextDescription; } }
    public RelicRarity Rarity {  get { return rarity; } }
    public Sprite Icon { get { return spriteIcon; } }  
    public bool HasCondition {  get { return hasCondition; } }
    public StatsEnum changedStat { get { return _changedStat; } }

    public virtual void Initialize(StatsSO playerstats) 
    { 
        _playerStats = playerstats;
    }

    public virtual void OnPickup()
    {
        ActivateBuffs();
    }

    public abstract void ActivateBuffs();

    public abstract void DeactivateBuffs();

    public abstract bool Condition();
}

public enum Operator
{
    PLUS,
    MINUS,
    MULT,
    DIV
}

public enum NumType
{
    PERCENTAGE,
    INTEGER
}

public enum RelicRarity
{
    COMMON,
    UNCOMMON,
    RARE,
    LEGENDARY
}