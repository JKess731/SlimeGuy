using UnityEngine;

public class PlayerScript1 : MonoBehaviour
{
    private Stats1 playerStats = new Stats1(10, 10, 10, 10);

    /// <summary>
    /// Gets the value of player's stat based on Stats1.statsName
    /// </summary>
    /// <param name="type"></param>
    /// <returns> Value of the stat </returns>
    public int returnStats(Stats1.statsName type)
    {
        switch (type)
        {
            case Stats1.statsName.MAXHEALTH:
                return playerStats.maxHealth;
            case Stats1.statsName.ATTACK:
                return playerStats.attack;
            case Stats1.statsName.DEFENSE:
                return playerStats.defense;
            case Stats1.statsName.SPEED:
                return playerStats.speed;
        }
        return 0;
    }
    /// <summary>
    /// Increase the given stat based on Stats1.statBoost
    /// </summary>
    /// <param name="amount"> The growth rate of the given stat</param>
    /// <param name="type"> The type of Stat Boost</param>
    public void increaseStats(int amount, Stats1.statBoost type)
    {
        switch (type)
        {
            case Stats1.statBoost.healthUP:
                playerStats.maxHealth += amount;
                break;
            case Stats1.statBoost.attackUP:
                playerStats.attack += amount;
                break;
            case Stats1.statBoost.defenseUP:
                playerStats.defense += amount;
                break;
            case Stats1.statBoost.speedUP:
                playerStats.speed += amount;
                break;
        }
    }
}
