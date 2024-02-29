using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Stats playerStats = new Stats(10, 10, 10, 10);

    /// <summary>
    /// Gets the value of player's stat based on Stats.statsName
    /// </summary>
    /// <param name="type"></param>
    /// <returns> Value of the stat </returns>
    public int returnStats(Stats.statsName type)
    {
        switch (type)
        {
            case Stats.statsName.MAXHEALTH:
                return playerStats.maxHealth;
            case Stats.statsName.ATTACK:
                return playerStats.attack;
            case Stats.statsName.DEFENSE:
                return playerStats.defense;
            case Stats.statsName.SPEED:
                return playerStats.speed;
        }
        return 0;
    }
    /// <summary>
    /// Increase the given stat based on Stats.statBoost
    /// </summary>
    /// <param name="amount"> The growth rate of the given stat</param>
    /// <param name="type"> The type of Stat Boost</param>
    public void increaseStats(int amount, Stats.statBoost type)
    {
        switch (type)
        {
            case Stats.statBoost.healthUP:
                playerStats.maxHealth += amount;
                break;
            case Stats.statBoost.attackUP:
                playerStats.attack += amount;
                break;
            case Stats.statBoost.defenseUP:
                playerStats.defense += amount;
                break;
            case Stats.statBoost.speedUP:
                playerStats.speed += amount;
                break;
        }
    }
}
