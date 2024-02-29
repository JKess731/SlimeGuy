using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Edison Li
public class Stats1
{
    public int maxHealth { get; set; }
    public int attack { get; set; }
    public int defense { get; set; }
    public int speed { get; set; }

    public enum statBoost
    {
        healthUP,
        attackUP,
        defenseUP,
        speedUP
    }
    public enum statsName
    {
        MAXHEALTH,
        ATTACK,
        DEFENSE,
        SPEED
    }

    public Stats1 (int maxHealth, int attack, int defense, int speed)
    {
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }
}
