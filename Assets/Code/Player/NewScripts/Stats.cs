//Edison Li
public class Stats
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

    public Stats(int maxHealth, int attack, int defense, int speed)
    {
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }
}