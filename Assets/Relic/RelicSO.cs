using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RelicSO : ScriptableObject
{
    [SerializeField] protected Stats playerStats;
    [SerializeField] protected string relicName;
    [SerializeField] protected string flavorTextDescription;
    [SerializeField] protected RelicRarity rarity;
    [SerializeField] protected Sprite spriteIcon;

    [SerializeField] protected bool hasCondition = false;

    [HideInInspector] public RelicCalculator calc;

    public string Name { get { return name; } }
    public string Description {  get { return flavorTextDescription; } }
    public RelicRarity Rarity {  get { return rarity; } }
    public Sprite Icon { get { return spriteIcon; } }  
    public bool HasCondition {  get { return hasCondition; } }

    public abstract void OnPickup();

    public abstract void ActivateBuffs();

    public abstract void DeactivateBuffs();

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