using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private stats playerStats = new stats(10, 10, 10, 10);

    public int returnStats(stats.statsName type)
    {
        switch (type)
        {
            case stats.statsName.MAXHEALTH:
                return playerStats.maxHealth;
            case stats.statsName.ATTACK:
                return playerStats.attack;
            case stats.statsName.DEFENSE:
                return playerStats.defense;
            case stats.statsName.SPEED:
                return playerStats.speed;
        }
        return 0;
    }

    public void increaseStats(int amount, stats.statBoost type)
    {
        switch (type)
        {
            case stats.statBoost.healthUP:
                playerStats.maxHealth += amount;
                break;
            case stats.statBoost.attackUP:
                playerStats.attack += amount;
                break;
            case stats.statBoost.defenseUP:
                playerStats.defense += amount;
                break;
            case stats.statBoost.speedUP:
                playerStats.speed += amount;
                break;
        }
    }
}
