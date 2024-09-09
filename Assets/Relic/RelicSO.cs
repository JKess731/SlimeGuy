using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RelicSO : ScriptableObject
{
    [System.Serializable]
    public class RelicBuff
    {
        public StatsEnum statType;
        public Operator operate;
        public float num;
        public NumType numberType;
    }

    [System.Serializable]
    public class RelicCondition
    {
        public StatsEnum statType;
        public Operator operate;
        public float num;
        public NumType numberType;
    }

    [InspectorLabel("Buffs")]
    public List<RelicBuff> relicBuffs = new List<RelicBuff>();
    [InspectorLabel("Conditions")]
    public List<RelicCondition> relicConditions = new List<RelicCondition>();

    public Stats playerStats;
    public string relicName;
    public string flavorTextDescription;
    public RelicRarity rarity;
    public Sprite spriteIcon;

    public abstract void ActivateBuffs();

    public abstract bool Condition();
}

public enum Operator
{
    PLUS,
    MINUS,
    MULT,
    DIV,
    LESS_THAN,
    GREATER_THAN
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