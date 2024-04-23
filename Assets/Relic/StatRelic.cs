using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatRelic : Relic
{
    [SerializeField] private Enum_Stat statboost;
    [SerializeField] private float boostAmount;
    override public void Pickup()
    {
        base.Pickup();
        ApplyStatBoost();
        Destroy(gameObject);
    }

    private void ApplyStatBoost()
    {
        switch (statboost)
        {
            case Enum_Stat.HEALTH:
                PlayerStats.instance.IncreaseMaxHealth(boostAmount);
                break;
            case Enum_Stat.ATTACK:
                PlayerStats.instance.IncreaseAttack(boostAmount);
                break;
            case Enum_Stat.ATTACKSPEED:
                PlayerStats.instance.IncreaseAttackSpeed(boostAmount);
                break;
            case Enum_Stat.DEFENSE:
                PlayerStats.instance.IncreaseDefense(boostAmount);
                break;
            case Enum_Stat.SPEED:
                PlayerStats.instance.IncreaseSpeed(boostAmount);
                break;
        }
    }
}
